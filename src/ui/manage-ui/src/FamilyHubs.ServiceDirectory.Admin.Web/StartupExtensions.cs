﻿using FamilyHubs.Notification.Api.Client.Extensions;
using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.DistributedCache;
using FamilyHubs.ServiceDirectory.Admin.Core.Health;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.SharedKernel.GovLogin.AppStart;
using FamilyHubs.SharedKernel.Health;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Services.PostcodesIo.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Serilog;
using Serilog.Events;

namespace FamilyHubs.ServiceDirectory.Admin.Web;

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

            loggerConfiguration.WriteTo.Console(parsed ? logLevel : LogEventLevel.Warning);
        }, preserveStaticLogger: true);
    }

    public static void ConfigureServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton<ITelemetryInitializer, TelemetryPiiRedactor>();
        services.AddApplicationInsightsTelemetry();

        // Add services to the container.
        services
            .AddClientServices(configuration)
            .AddWebUiServices();

        services.AddNotificationsApiClient(configuration);

        services.AddAndConfigureGovUkAuthentication(configuration);

        services.AddScoped<IEmailService, EmailService>();
        services.AddSingleton<ICacheService, CacheService>();
        services.AddTransient<IRequestDistributedCache, RequestDistributedCache>();

        // Add services to the container.
        services.AddRazorPages(options =>
        {
            options.Conventions.AuthorizeAreaFolder("AccountAdmin", "/", "DfeAdminAndLaManager");
            options.Conventions.AuthorizeAreaFolder("VcsAdmin", "/", "DfeAdminAndLaManager");
            options.Conventions.AuthorizeAreaFolder("MyAccount", "/");
        })
        .AddViewOptions(options =>
        {
            options.HtmlHelperOptions.ClientValidationEnabled = false;
        });

        services.AddAuthorization(options => options.AddPolicy("DfeAdmin", policy =>
            policy.RequireAssertion(context =>
                context.User.HasClaim(claim => claim.Value == RoleTypes.DfeAdmin)
            )));

        services.AddAuthorization(options => options.AddPolicy("DfeAdminAndLaManager", policy =>
            policy.RequireAssertion(context =>
                context.User.HasClaim(claim => claim.Value == RoleTypes.DfeAdmin
                || claim.Value == RoleTypes.LaManager
                || claim.Value == RoleTypes.LaDualRole)
            )));

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(configuration.GetValue<int>("SessionTimeOutMinutes"));
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        // Add Session middleware
        services.AddDistributedCache(configuration);

        services.AddSiteHealthChecks(configuration);

        services.AddPostcodesIoClient(configuration);

        services.AddFamilyHubs(configuration);
    }

    //todo: components use distributed cache
    public static IServiceCollection AddDistributedCache(this IServiceCollection services, ConfigurationManager configuration)
    {
        var cacheConnection = configuration.GetValue<string>("CacheConnection");

        if (string.IsNullOrWhiteSpace(cacheConnection))
        {
            services.AddDistributedMemoryCache();
        }
        else
        {
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = cacheConnection;
                options.TableName = "AdminUiCache";
                options.SchemaName = "dbo";
            });
        }

        services.AddTransient<ICacheService, CacheService>();

        services.AddTransient<ICacheKeys, CacheKeys>();

        // there's currently only one, so this should be fine
        services.AddSingleton(new DistributedCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(configuration.GetValue<int>("SessionTimeOutMinutes"))
        });

        return services;
    }

    private static void AddWebUiServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
    }

    public static IServiceCollection AddClientServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddClient<IServiceDirectoryClient>(configuration, "ServiceDirectoryApiBaseUrl", (httpClient, serviceProvider) =>
        {
            var cacheService = serviceProvider.GetService<ICacheService>();
            var logger = serviceProvider.GetService<ILogger<ServiceDirectoryClient>>();
            ArgumentNullException.ThrowIfNull(cacheService);
            return new ServiceDirectoryClient(httpClient, cacheService, logger!);
        });
        serviceCollection.AddClient<ITaxonomyService>(configuration, "ServiceDirectoryApiBaseUrl", (c, sp) => new TaxonomyService(c, sp.GetService<ILogger<TaxonomyService>>()!));

        serviceCollection.AddClient<IReferralService>(configuration, "ReferralApiBaseUrl", (c, sp) => new Core.ApiClient.ReferralService(c, sp.GetService<ILogger<Core.ApiClient.ReferralService>>()!));


        serviceCollection.AddClient<IIdamClient>(configuration, "IdamApi", (c, serviceProvider) => new IdamClient(c, serviceProvider.GetService<ILogger<IdamClient>>()!));

        serviceCollection.AddClient<IReportingClient>(configuration, "ReportingApiBaseUrl", (c, _) => new ReportingClient(c));

        return serviceCollection;
    }

    private static void AddClient<T>(this IServiceCollection services, IConfiguration config, string baseUrlKey, Func<HttpClient, IServiceProvider, T> instance) where T : class
    {
        var name = typeof(T).Name;

        services.AddSecureHttpClient(name, (_, httpClient) =>
        {
            var baseUrl = config.GetValue<string?>(baseUrlKey);
#pragma warning disable S3236
            ArgumentNullException.ThrowIfNull(baseUrl, $"appsettings.{baseUrlKey}");
#pragma warning restore S3236

            httpClient.BaseAddress = new Uri(baseUrl);
        });

        services.AddScoped<T>(s =>
        {
            var clientFactory = s.GetService<IHttpClientFactory>();

            var httpClient = clientFactory?.CreateClient(name);

            ArgumentNullException.ThrowIfNull(httpClient);

            return instance.Invoke(httpClient, s);
        });
    }

    public static void ConfigureWebApplication(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        app.UseFamilyHubs();
        
        app.UseHsts();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseGovLoginAuthentication();

        app.UseSession();

        app.MapRazorPages();

        app.MapFamilyHubsHealthChecks(typeof(StartupExtensions).Assembly);
    }
}