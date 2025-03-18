using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FamilyHubs.Referral.Data.Repository;
using Microsoft.Extensions.Logging;
using FamilyHubs.Referral.Api;
using FamilyHubs.Referral.Core.ClientServices;
using Microsoft.Extensions.Configuration;

namespace FamilyHubs.Referral.FunctionalTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _referralConnection;
    private readonly Action<IConfigurationBuilder> _conf;
    private readonly HttpClient _sdClient;
    public CustomWebApplicationFactory(Action<IConfigurationBuilder> conf, HttpClient sdClient)
    {
        _conf = conf;
        _sdClient = sdClient;
        _referralConnection = $"Data Source=REFERRAL-{Guid.NewGuid()}.db;Mode=ReadWriteCreate;Cache=Shared;Foreign Keys=True;Recursive Triggers=True;Default Timeout=30;Pooling=True";
    }

    /// <summary>
    /// Overriding CreateHost to avoid creating a separate ServiceProvider per this thread:
    /// https://github.com/dotnet-architecture/eShopOnWeb/issues/465
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureHostConfiguration(_conf);

        var host = base.CreateHost(builder);

        // Get service provider.
        var serviceProvider = host.Services;

        // Create a scope to obtain a reference to the database
        // context (AppDbContext).
        using (var scope = serviceProvider.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ApplicationDbContext>();

            // Ensure the database is created.
            db.Database.EnsureCreated();
        }

        return host;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var efCoreServices = services.Where(s =>
                s.ServiceType.FullName?.Contains("EntityFrameworkCore") == true).ToList();

            efCoreServices.ForEach(s => services.Remove(s));

            services.AddSingleton<IServiceDirectoryService>(new ServiceDirectoryService(_sdClient));
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(_referralConnection, mg =>
                    mg.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.ToString()));
            });
        });

        builder.UseEnvironment("Development");
    }

    public void SetupTestDatabaseAndSeedData()
    {
        using var scope = Services.CreateScope();

        var scopedServices = scope.ServiceProvider;
        var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory>>();

        try
        {
            var context = scopedServices.GetRequiredService<ApplicationDbContext>();


            var statuses = ReferralSeedData.SeedStatuses();

            if (!context.Statuses.Any())
            {
                context.Statuses.AddRange(statuses);
                context.SaveChanges();
            }

            if (!context.Referrals.Any())
            {
                IReadOnlyCollection<Data.Entities.Referral> referrals = ReferralSeedData.SeedReferral();

                foreach (Data.Entities.Referral referral in referrals)
                {
                    var status = context.Statuses.SingleOrDefault(x => x.Name == referral.Status.Name);
                    if (status != null)
                    {
                        referral.Status = status;
                    }
                }

                context.Referrals.AddRange(referrals);
                context.SaveChanges();
            }

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred seeding the database with test messages. Error: {exceptionMessage}", ex.Message);
        }
    }
}

