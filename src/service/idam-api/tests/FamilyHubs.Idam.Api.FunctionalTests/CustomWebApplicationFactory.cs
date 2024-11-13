using FamilyHubs.Idam.Data.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idam.Api.FunctionalTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _serviceDirectoryConnection;
    private readonly Action<IConfigurationBuilder> _conf;

    public CustomWebApplicationFactory(Action<IConfigurationBuilder> conf)
    {
        _conf = conf;
        _serviceDirectoryConnection = $"Data Source=idam-{Random.Shared.Next().ToString()}.db;Mode=ReadWriteCreate;Cache=Shared;Foreign Keys=True;Recursive Triggers=True;Default Timeout=30;Pooling=True";
    }
    
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureHostConfiguration(_conf);

        return base.CreateHost(builder);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var efCoreServices = services.Where(s => s.ServiceType.FullName?.Contains("EntityFrameworkCore") == true).ToList();

            efCoreServices.ForEach(s => services.Remove(s));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(_serviceDirectoryConnection, mg => mg.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.ToString()));
            });

        });

        builder.UseEnvironment("Development");
    }

    public void SetupTestData(bool many = false)
    {
        using var scope = Services.CreateScope();

        var scopedServices = scope.ServiceProvider;
        var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory>>();

        try
        {
            var context = scopedServices.GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureCreated();
            context.Accounts.Add(TestDataProvider.GetTestAccount(many));

            context.SaveChanges();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred seeding the database with test messages");
        }
    }
}