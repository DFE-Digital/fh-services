using FamilyHubs.ServiceDirectory.Data.Repository;
using FamilyHubs.ServiceDirectory.Api;
using FamilyHubs.ServiceDirectory.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FamilyHubs.ServiceDirectory.Shared.Enums;

namespace FamilyHubs.Referral.FunctionalTests;

public class ServiceDirectoryFactory : WebApplicationFactory<Program>
{
    private readonly string _sdConnection = $"Data Source=SERVICE_DIRECTORY-{Guid.NewGuid()}.db;Mode=ReadWriteCreate;Cache=Shared;Foreign Keys=True;Recursive Triggers=True;Default Timeout=30;Pooling=True";

    protected override IHost CreateHost(IHostBuilder builder)
    {
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

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(_sdConnection, mg =>
                    mg.UseNetTopologySuite().MigrationsAssembly(typeof(ApplicationDbContext).Assembly.ToString()));
            });
        });

        builder.UseEnvironment("Development");
    }

    public void SetupTestDatabaseAndSeedData()
    {
        using var scope = Services.CreateScope();

        var scopedServices = scope.ServiceProvider;
        var context = scopedServices.GetRequiredService<ApplicationDbContext>();

        SeedOrganisations(context);
        SeedServices(context);
    }

    private static void SeedOrganisations(ApplicationDbContext context)
    {
        const OrganisationType organisationType = OrganisationType.LA;
        var organisations = new List<Organisation>
        {
            new()
            {
                Id = 1,
                OrganisationType =      organisationType,
                Name =                  "Bristol County Council",
                Description =           "Bristol County Council",
                Uri =                   new Uri("https://www.bristol.gov.uk/").ToString(),
                Url =                   "https://www.bristol.gov.uk/",
                AdminAreaCode =         "E06000023",
            },
            new()
            {
                Id = 2,
                OrganisationType =      organisationType,
                Name =                  "Lancashire County Council",
                Description =           "Lancashire County Council",
                Uri =                   new Uri("https://www.lancashire.gov.uk/").ToString(),
                Url =                   "https://www.lancashire.gov.uk/",
                AdminAreaCode =         "E10000017",
            },
            new()
            {
                Id = 3,
                OrganisationType =      organisationType,
                Name =                  "London Borough of Redbridge",
                Description =           "London Borough of Redbridge",
                Uri =                   new Uri("https://www.redbridge.gov.uk/").ToString(),
                Url =                   "https://www.redbridge.gov.uk/",
                AdminAreaCode =         "E09000026",
            }
        };

        context.Organisations.AddRange(organisations);
        context.SaveChanges();
    }

    private static void SeedServices(ApplicationDbContext context)
    {
        var services = new List<Service>
        {
            new()
            {
                Id = 1,
                ServiceType =           ServiceType.InformationSharing,
                Name =                  "Elop Mentoring",
                Description =           "Elop Mentoring",
                OrganisationId =        1
            },
            new()
            {
                Id = 2,
                ServiceType =           ServiceType.InformationSharing,
                Name =                  "Collingwood Youth Centre",
                Description =           "Collingwood Youth Centre",
                OrganisationId =        2
            },
            new()
            {
                Id = 3,
                ServiceType =           ServiceType.InformationSharing,
                Name =                  "Newark Youth London",
                Description =           "Newark Youth London",
                OrganisationId =        3
            }
        };

        context.Services.AddRange(services);
        context.SaveChanges();
    }
}