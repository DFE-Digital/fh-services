﻿using AutoMapper.EquivalencyExpression;
using AutoMapper;
using FamilyHubs.Notification.Api.Contracts;
using FamilyHubs.Notification.Api.Endpoints;
using FamilyHubs.SharedKernel.GovLogin.AppStart;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using FamilyHubs.Notification.Core;
using FamilyHubs.Notification.Data.Repository;
using FamilyHubs.Notification.Api.Middleware;
using FamilyHubs.Notification.Data.Interceptors;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using FamilyHubs.Notification.Core.Commands.CreateNotification;
using FamilyHubs.Notification.Data.NotificationServices;
using FamilyHubs.SharedKernel.Razor.Health;
using Notify.Client;
using Notify.Interfaces;
using FamilyHubs.SharedKernel.Security;

namespace FamilyHubs.Notification.Api;

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

    public static void RegisterApplicationComponents(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IKeyProvider, KeyProvider>();

        services.AddBearerAuthentication(configuration);
        
        services.AddFamilyHubsHealthChecks(configuration);

        services.RegisterAppDbContext(configuration);

        services.RegisterMinimalEndPoints();

        services.RegisterAdditionalInterfaces(configuration);

        services.RegisterAutoMapper();

        services.RegisterMediator();
    }

    private static void RegisterMinimalEndPoints(this IServiceCollection services)
    {
        services.AddTransient<MinimalNotifyEndPoints>();
    }

    private static void RegisterAdditionalInterfaces(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IGovNotifySender, GovNotifySender>();
        string? connectNotifyApiKey = configuration["GovNotifySetting:ConnectAPIKey"];
        if (connectNotifyApiKey == null)
        {
            //todo: use config exception
            throw new InvalidOperationException("Connect API Key is not configured");
        }
        services.AddSingleton<IServiceNotificationClient>(s => new ServiceNotificationClient(ApiKeyType.ConnectKey, connectNotifyApiKey));
        
        string? manageNotifyApiKey = configuration["GovNotifySetting:ManageAPIKey"];
        if (manageNotifyApiKey == null)
        {
            throw new InvalidOperationException("Manage API Key is not configured");
        }
        services.AddSingleton<IServiceNotificationClient>(s => new ServiceNotificationClient(ApiKeyType.ManageKey, manageNotifyApiKey));
    }

    private static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper((serviceProvider, cfg) =>
        {
            var auditProperties = new[] { "Created", "CreatedBy", "LastModified", "LastModifiedBy" };
            cfg.AddProfile<AutoMappingProfiles>();
            cfg.AddCollectionMappers();
            cfg.UseEntityFrameworkCoreModel<ApplicationDbContext>(serviceProvider);
            cfg.ShouldMapProperty = pi => !auditProperties.Contains(pi.Name);
        }, typeof(AutoMappingProfiles));
    }

    private static void RegisterAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<AuditableEntitySaveChangesInterceptor>();
        services.AddTransient<ApplicationDbContextInitialiser>();

        var connectionString = configuration.GetConnectionString("NotificationConnection");
        ArgumentException.ThrowIfNullOrEmpty(connectionString);

        var useSqlite = configuration.GetValue<bool?>("UseSqlite");
        ArgumentNullException.ThrowIfNull(useSqlite);

        //DO not remove, This will prevent Application from starting if wrong type of connection string is provided
        var connection = (useSqlite == true)
            ? new SqliteConnectionStringBuilder(connectionString).ToString()
            : new SqlConnectionStringBuilder(connectionString).ToString();

        // Register Entity Framework
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (useSqlite == true)
            {
                options.UseSqlite(connection, mg =>
                    mg.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.ToString())
                        .MigrationsHistoryTable("NotificationMigrationHistory"));
            }
            else
            {
                options.UseSqlServer(connection, mg =>
                    mg.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.ToString())
                        .MigrationsHistoryTable("NotificationMigrationHistory"));
            }
        });
    }

    public static void RegisterMediator(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            typeof(CreateNotificationCommand).Assembly
        };

        services.AddMediatR(config =>
        {
            config.Lifetime = ServiceLifetime.Transient;
            config.RegisterServicesFromAssemblies(assemblies);
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddTransient<CorrelationMiddleware>();
        services.AddTransient<ExceptionHandlingMiddleware>();
    }

    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration, bool isProduction)
    {
        services.AddApplicationInsightsTelemetry();

        // Add services to the container.
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "FamilyHubs.Notification.Api", Version = "v1" });
            c.EnableAnnotations();
        });
    }

    public static async Task ConfigureWebApplication(this WebApplication webApplication)
    {
        webApplication.UseSerilogRequestLogging();

        webApplication.UseMiddleware<CorrelationMiddleware>();
        webApplication.UseMiddleware<ExceptionHandlingMiddleware>();

        // Configure the HTTP request pipeline.
        webApplication.UseSwagger();
        webApplication.UseSwaggerUI();

        webApplication.UseHttpsRedirection();

        webApplication.MapControllers();

        webApplication.MapFamilyHubsHealthChecks(typeof(StartupExtensions).Assembly);

        await RegisterEndPoints(webApplication);
    }

    private static async Task RegisterEndPoints(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var notifyApi = scope.ServiceProvider.GetService<MinimalNotifyEndPoints>();
        notifyApi?.RegisterMinimalNotifyEndPoints(app);

        try
        {
            if (!app.Environment.IsProduction())
            {
                // Seed Database
                var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
                var shouldRestDatabaseOnRestart = app.Configuration.GetValue<bool>("ShouldRestDatabaseOnRestart");
                await initialiser.InitialiseAsync(app.Environment.IsProduction(), shouldRestDatabaseOnRestart);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
        }
    }
}
