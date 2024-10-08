﻿using AutoMapper;
using AutoMapper.EquivalencyExpression;
using FamilyHubs.ServiceDirectory.Api.Endpoints;
using FamilyHubs.ServiceDirectory.Api.Middleware;
using FamilyHubs.ServiceDirectory.Core;
using FamilyHubs.ServiceDirectory.Core.Commands.Locations.CreateLocation;
using FamilyHubs.ServiceDirectory.Data;
using FamilyHubs.ServiceDirectory.Data.Interceptors;
using FamilyHubs.ServiceDirectory.Data.Repository;
using FamilyHubs.SharedKernel.GovLogin.AppStart;
using FamilyHubs.SharedKernel.Razor.Health;
using FluentValidation;
using MediatR;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

namespace FamilyHubs.ServiceDirectory.Api;

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
        services.RegisterAppDbContext(configuration);

        services.RegisterMinimalEndPoints();

        services.RegisterAutoMapper();

        services.RegisterMediator();
    }

    private static void RegisterMinimalEndPoints(this IServiceCollection services)
    {
        services.AddTransient<MinimalMetricsEndPoints>();
        services.AddTransient<MinimalTaxonomyEndPoints>();
        services.AddTransient<MinimalOrganisationEndPoints>();
        services.AddTransient<MinimalServiceEndPoints>();
        services.AddTransient<MinimalLocationEndPoints>();
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

        var connectionString = configuration.GetConnectionString("ServiceDirectoryConnection");
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
                    mg.UseNetTopologySuite().MigrationsAssembly(typeof(ApplicationDbContext).Assembly.ToString()));
            }
            else
            {
                options.UseSqlServer(connection, mg =>
                    mg.UseNetTopologySuite().MigrationsAssembly(typeof(ApplicationDbContext).Assembly.ToString()));
            }
        });
    }

    public static void RegisterMediator(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            typeof(CreateLocationCommand).Assembly
        };

        services.AddMediatR(config =>
        {
            config.Lifetime = ServiceLifetime.Transient;
            config.RegisterServicesFromAssemblies(assemblies);
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssemblyContaining<CreateLocationCommandValidator>();

        services.AddTransient<ExceptionHandlingMiddleware>();
    }

    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration, bool isProduction)
    {
        services.AddSingleton<ITelemetryInitializer, ConnectTelemetryPiiRedactor>();
        services.AddApplicationInsightsTelemetry();

        // Add services to the container.
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "FamilyHubs.ServiceDirectory.Api", Version = "v1" });
            c.EnableAnnotations();
        });

        services.AddBearerAuthentication(configuration);

        services.AddFamilyHubsHealthChecks(configuration);
    }

    public static async Task ConfigureWebApplication(this WebApplication webApplication)
    {
        webApplication.UseSerilogRequestLogging();

        webApplication.UseMiddleware<ExceptionHandlingMiddleware>();

        // Configure the HTTP request pipeline.
        webApplication.UseSwagger();
        webApplication.UseSwaggerUI();

        webApplication.UseHttpsRedirection();

        webApplication.MapControllers();
        webApplication.MapFamilyHubsHealthChecks(typeof(StartupExtensions).Assembly);

        await RegisterEndPoints(webApplication);
    }

    private static async Task RegisterEndPoints(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();

        var taxonomyEndPoints = scope.ServiceProvider.GetService<MinimalTaxonomyEndPoints>();
        taxonomyEndPoints?.RegisterTaxonomyEndPoints(webApplication);

        var organisationEndPoints = scope.ServiceProvider.GetService<MinimalOrganisationEndPoints>();
        organisationEndPoints?.RegisterOrganisationEndPoints(webApplication);

        var serviceEndPoints = scope.ServiceProvider.GetService<MinimalServiceEndPoints>();
        serviceEndPoints?.RegisterServiceEndPoints(webApplication);

        var locationEndPoints = scope.ServiceProvider.GetService<MinimalLocationEndPoints>();
        locationEndPoints?.RegisterLocationEndPoints(webApplication);

        var metricsEndPoints = scope.ServiceProvider.GetService<MinimalMetricsEndPoints>();
        metricsEndPoints?.RegisterMetricsEndPoints(webApplication);

        try
        {
            // Seed Database
            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
            var shouldRestDatabaseOnRestart = webApplication.Configuration.GetValue<bool>("ShouldRestDatabaseOnRestart");
            await initialiser.InitialiseAsync(webApplication.Environment.IsProduction(), shouldRestDatabaseOnRestart);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred seeding the DB. {ExceptionMessage}", ex.Message);
        }
    }
}
