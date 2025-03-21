﻿using FamilyHubs.Referral.Core.ApiClients;
using FamilyHubs.Referral.Core.DistributedCache;
using FamilyHubs.Referral.Infrastructure.DistributedCache;
using FamilyHubs.SharedKernel.GovLogin.AppStart;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;
using System.Diagnostics.CodeAnalysis;
using FamilyHubs.Notification.Api.Client.Extensions;
using FamilyHubs.Notification.Api.Client.Templates;
using FamilyHubs.Referral.Core;
using FamilyHubs.Referral.Infrastructure.Health;
using FamilyHubs.Referral.Infrastructure.Notifications;
using FamilyHubs.SharedKernel.DataProtection;
using FamilyHubs.SharedKernel.Health;
using FamilyHubs.SharedKernel.Telemetry;
using FamilyHubs.SharedKernel.Services.PostcodesIo.Extensions;

namespace FamilyHubs.Referral.Web;

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

        builder.Services.AddAndConfigureGovUkAuthentication(builder.Configuration);
    }

    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ITelemetryInitializer, ConnectTelemetryPiiRedactor>();
        services.AddApplicationInsightsTelemetry();

        // Add services to the container.
        services.AddHttpClients(configuration);

        services.AddWebUiServices(configuration);

        // Add services to the container.
        services.AddRazorPages()
            .AddViewOptions(options =>
            {
                options.HtmlHelperOptions.ClientValidationEnabled = false;
            });

        services.AddSiteHealthChecks(configuration);

        services.AddFamilyHubs(configuration);
    }

    private static void AddWebUiServices(this IServiceCollection services, IConfiguration configuration)
    {
        const string dataProtectionAppName = "Connect";
        services.AddFamilyHubsDataProtection(configuration, dataProtectionAppName);

        services.AddHttpContextAccessor();

        services.AddNotificationsApiClient(configuration);
        services.AddSingleton<INotificationTemplates<NotificationType>, NotificationTemplates<NotificationType>>();
        services.AddTransient<IReferralNotificationService, ReferralNotificationService>();
        services.AddPostcodesIoClient(configuration);

        services.AddIdamsClient(configuration);

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        string? connectionString = configuration["SqlServerCache:Connection"];
        string? schemaName = configuration["SqlServerCache:SchemaName"];
        string? tableName = configuration["SqlServerCache:TableName"];

        if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(schemaName) ||
            string.IsNullOrEmpty(tableName))
        {
            //todo: config exception?
            throw new InvalidOperationException("Missing config in SqlServerCache section");
        }

        services.AddSqlServerDistributedCache(
            connectionString,
            int.Parse(configuration["SqlServerCache:SlidingExpirationInMinutes"] ?? "240"),
            schemaName, tableName);
        services.AddTransient<IConnectionRequestDistributedCache, ConnectionRequestDistributedCache>();
    }

    private static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IOrganisationClientService, OrganisationClientService>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetValue<string>("ServiceDirectoryUrl")!);
        });

        services.AddSecuredTypedHttpClient<IReferralClientService, ReferralClientService>((serviceProvider, httpClient) =>
        {
            httpClient.BaseAddress = new Uri(configuration.GetValue<string>("ReferralApiUrl")!);
        });
        
        services.AddSecuredTypedHttpClient<IReferralDashboardClientService, ReferralClientService>((serviceProvider, httpClient) =>
        {
            httpClient.BaseAddress = new Uri(configuration.GetValue<string>("ReferralApiUrl")!);
        });
    }

    public static IServiceCollection AddSecuredTypedHttpClient<TClient, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(
            this IServiceCollection services, Action<IServiceProvider, HttpClient> configureClient)
            where TClient : class
            where TImplementation : class, TClient
    {
        services.AddHttpClient<TClient, TImplementation>((serviceProvider, httpClient) =>
        {
            configureClient(serviceProvider, httpClient);
            var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            if (httpContextAccessor == null)
                throw new ArgumentException($"IHttpContextAccessor required for {nameof(AddSecuredTypedHttpClient)}");

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {httpContextAccessor.HttpContext!.GetBearerToken()}");

        });

        return services;
    }

    public static IServiceProvider ConfigureWebApplication(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        app.UseFamilyHubsDataProtection();

        app.UseFamilyHubs();
        
        app.UseHsts();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseGovLoginAuthentication();

        app.MapRazorPages();

        app.MapFamilyHubsHealthChecks(typeof(StartupExtensions).Assembly);

        app.RedirectFindRequests();
        
        return app.Services;
    }

    private static void RedirectFindRequests(this WebApplication app)
    {
        app.MapGet("/PostcodeSearch", context =>
        {
            context.Response.Redirect("/ProfessionalReferral/Search", true);
            return Task.CompletedTask;
        });
            
        app.MapGet("/ServiceDetail", context =>
        {
            var serviceId = context.Request.Query.ContainsKey("serviceId") ? context.Request.Query["serviceId"][0] : null;

            // Redirect to the search page if we cannot resolve the service Id
            context.Response.Redirect(
                serviceId is not null
                    ? $"/ProfessionalReferral/LocalOfferDetail/?serviceid={serviceId}"
                    : "/ProfessionalReferral/Search", true);

            return Task.CompletedTask;
        });

        app.MapGet("/ServiceFilter", context =>
        {
            var postcode = context.Request.Query.ContainsKey("postcode") ? context.Request.Query["postcode"][0] : null;

            // Redirect to the search page if we cannot resolve the postcode to search
            context.Response.Redirect(
                postcode is not null
                    ? $"/ProfessionalReferral/LocalOfferResults/?postcode={postcode}&currentPage=1"
                    : "/ProfessionalReferral/Search", true);

            return Task.CompletedTask;
        });
    }
}
