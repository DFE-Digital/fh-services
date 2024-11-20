using EFCoreSecondLevelCacheInterceptor;
using FamilyHubs.Report.Api.Endpoints;
using FamilyHubs.Report.Api.Middleware;
using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts;
using FamilyHubs.Report.Core.Queries.ServiceSearchFacts;
using FamilyHubs.Report.Core.Queries.ServiceSearchFacts.Validators;
using FamilyHubs.Report.Data;
using FamilyHubs.Report.Data.Repository;
using FamilyHubs.SharedKernel.Extensions;
using FamilyHubs.SharedKernel.GovLogin.AppStart;
using FamilyHubs.SharedKernel.Health;
using FluentValidation;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

namespace FamilyHubs.Report.Api;

public class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Configuration.ConfigureAzureKeyVault();
        AddServices(builder.Services, builder.Configuration);
        ConfigureAppInsights(builder);

        WebApplication app = builder.Build();

        IServiceScope serviceScope = app.Services.CreateScope();
        RegisterMinimalEndPoints(serviceScope, app);

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseSerilogRequestLogging();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapFamilyHubsHealthChecks(typeof(Program).Assembly);

        app.Run();
    }

    private static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddBearerAuthentication(configuration);

        services.AddSingleton<ITelemetryInitializer, ReportTelemetryPiiRedactor>();
        services.AddApplicationInsightsTelemetry();
        services.AddTransient<ExceptionHandlingMiddleware>();

        services.AddFamilyHubsHealthChecks(configuration);

        services.AddEFSecondLevelCache(options =>
        {
            options
                .UseMemoryCacheProvider()
                .CacheAllQueries(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(5))
                .UseCacheKeyPrefix("EF_")
                .UseDbCallsIfCachingProviderIsDown(TimeSpan.FromSeconds(30));
        });

        services.AddDbContext<IReportDbContext, ReportDbContext>((serviceProvider, options) =>
        {
            options
                .UseSqlServer(configuration.GetConnectionString("ReportConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
                .AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>());
        });

        services.AddTransient<IGetServiceSearchFactQuery, GetServiceSearchFactQuery>();
        services.AddTransient<IGetFourWeekBreakdownQuery, GetFourWeekBreakdownQuery>();

        services.AddTransient<IGetConnectionRequestsSentFactQuery, GetConnectionRequestsSentFactQuery>();
        services.AddTransient<IGetConnectionRequestsSentFactFourWeekBreakdownQuery, GetConnectionRequestsSentFactFourWeekBreakdownQuery>();

        services.AddValidatorsFromAssembly(typeof(LaRequestValidator).Assembly);

        services.AddSingleton<MinimalAdminReportEndPoints>();
        services.AddSingleton<MinimalOrgReportEndPoints>();
    }
    

    private static void RegisterMinimalEndPoints(IServiceScope scope, WebApplication app)
    {
        scope.ServiceProvider.GetService<MinimalAdminReportEndPoints>()?.RegisterAdminReportEndPoints(app);
        scope.ServiceProvider.GetService<MinimalOrgReportEndPoints>()?.RegisterOrgReportEndPoints(app);
    }

    private static void ConfigureAppInsights(WebApplicationBuilder builder)
    {
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
}
