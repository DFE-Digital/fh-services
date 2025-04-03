using FamilyHubs.ServiceDirectory.Infrastructure.Services.ServiceDirectory.Extensions;
using FamilyHubs.ServiceDirectory.Web.Pages.ServiceFilter;
using FamilyHubs.SharedKernel.Health;
using FamilyHubs.SharedKernel.Services.PostcodesIo;
using FamilyHubs.SharedKernel.Services.PostcodesIo.Extensions;
using FamilyHubs.SharedKernel.Telemetry;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Events;

namespace FamilyHubs.ServiceDirectory.Web;

public static class StartupExtensions
{
    public static void ConfigureHost(this WebApplicationBuilder builder)
    {
        // ApplicationInsights
        builder.Host.UseSerilog((_, services, loggerConfiguration) =>
        {
            var logLevelString = builder.Configuration["LogLevel"];

            var parsed = Enum.TryParse<LogEventLevel>(logLevelString, out var logLevel);

            loggerConfiguration.WriteTo.ApplicationInsights(
                services.GetRequiredService<TelemetryConfiguration>(),
                TelemetryConverter.Traces,
                parsed ? logLevel : LogEventLevel.Warning);

            loggerConfiguration.WriteTo.Console(
                parsed ? logLevel : LogEventLevel.Warning);
        });
    }

    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ITelemetryInitializer, TelemetryPiiRedactor>();
        services.AddApplicationInsightsTelemetry();

        // Add services to the container.
        services.AddRazorPages();

        services.AddSingleton<IPageFilterFactory, PageFilterFactory>();
        services.AddPostcodesIoClient(configuration);
        services.AddServiceDirectoryClient(configuration);
        services.AddFamilyHubs(configuration);
        services.AddFamilyHubsHealthChecks(configuration)
            .AddUrlGroup(PostcodesIoLookup.HealthUrl(configuration), "PostcodesIo", HealthStatus.Degraded, new[] {"ExternalAPI"});
    }

    public static IServiceProvider ConfigureWebApplication(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        app.UseFamilyHubs();
        
        app.UseHsts();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.MapFamilyHubsHealthChecks(typeof(StartupExtensions).Assembly);

        return app.Services;
    }
}
