using FamilyHubs.OpenReferral.Function.ClientServices;
using FamilyHubs.OpenReferral.Function.Repository;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// TODO: 2. Unit Test Update
// TODO: 3. Fix: Plurals for DEDS Db Table Names (Shared Kernel 2.8.1)

IHost host = new HostBuilder()
    .ConfigureHostConfiguration(config =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
        config.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: false);
    })
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        IConfiguration config = services.BuildServiceProvider().GetService<IConfiguration>()!;

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddHttpClient<IHsdaApiService, HsdaApiService>(httpClient =>
            httpClient.BaseAddress = new Uri(config["ApiConnection"]!));

        services.AddDbContext<IFunctionDbContext, FunctionDbContext>(options =>
        {
            options.UseSqlServer(config["ServiceDirectoryConnection"])
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
        });
    })
    .Build();

await host.RunAsync();