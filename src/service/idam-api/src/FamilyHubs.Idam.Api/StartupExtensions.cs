using FamilyHubs.Idam.Api.Middleware;
using FamilyHubs.Idam.Core.Commands.Add;
using FamilyHubs.Idam.Core.Mapper;
using FamilyHubs.Idam.Core.Services;
using FamilyHubs.Idam.Data.Interceptors;
using FamilyHubs.Idam.Data.Repository;
using FamilyHubs.SharedKernel.GovLogin.AppStart;
using FamilyHubs.SharedKernel.Razor.Health;
using FamilyHubs.SharedKernel.Security;
using FluentValidation;
using MediatR;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

namespace FamilyHubs.Idam.Api;

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
        services.AddSingleton<ICrypto, Crypto>();
        services.RegisterAppDbContext(configuration);

        var serviceDirectoryApiBaseUrl = configuration["ServiceDirectoryApiBaseUrl"];
        if(!string.IsNullOrWhiteSpace(serviceDirectoryApiBaseUrl))
        {
            services.AddHttpClient<IServiceDirectoryService, ServiceDirectoryService>(client =>
            {
                client.BaseAddress = new Uri(serviceDirectoryApiBaseUrl);
            });
        }

        services.RegisterMediator();
    }

    private static void RegisterAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<AuditableEntitySaveChangesInterceptor>();

        var connectionString = configuration.GetConnectionString("IdamConnection");
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
                        .MigrationsHistoryTable("IdamMigrationHistory"));
            }
            else
            {
                options.UseSqlServer(connection, mg =>
                    mg.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.ToString()));
            }
        });
    }

    private static void RegisterMediator(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            typeof(AddClaimCommand).Assembly
        };

        services.AddMediatR(config =>
        {
            config.Lifetime = ServiceLifetime.Transient;
            config.RegisterServicesFromAssemblies(assemblies);
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssemblyContaining<AddClaimCommandValidator>();

        services.AddTransient<CorrelationMiddleware>();
        services.AddTransient<ExceptionHandlingMiddleware>();
    }

    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration, bool isProduction)
    {
        services.AddSingleton<ITelemetryInitializer, IdamsTelemetryPiiRedactor>();
        services.AddApplicationInsightsTelemetry();

        // Add services to the container.
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddAutoMapper(typeof(AutoMappingProfile));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "FamilyHubs.IDAM.Api", Version = "v1" });
            c.EnableAnnotations();
        });

        services.AddBearerAuthentication(configuration);

        services.AddFamilyHubsHealthChecks(configuration);
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

        await webApplication.InitialiseDatabase();
    }

    private static async Task InitialiseDatabase(this WebApplication webApplication)
    {
        try
        {
            using var scope = webApplication.Services.CreateScope();

            // Seed Database
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var shouldRestDatabaseOnRestart = webApplication.Configuration.GetValue<bool>("ShouldRestDatabaseOnRestart");
            
            if (!webApplication.Environment.IsProduction())
            {

                if (shouldRestDatabaseOnRestart) 
                    await dbContext.Database.EnsureDeletedAsync();

                if(dbContext.Database.IsSqlServer())
                    await dbContext.Database.MigrateAsync();
                else
                    await dbContext.Database.EnsureCreatedAsync();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while initialising the database");
            
            throw;
        }
    }
}
