using System.Diagnostics.CodeAnalysis;
using FamilyHubs.Idams.Maintenance.Core.ApiClient;
using FamilyHubs.Idams.Maintenance.Core.Commands.Add;
using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.Idams.Maintenance.Data.Interceptors;
using FamilyHubs.Idams.Maintenance.Data.Repository;
using FamilyHubs.Idams.Maintenance.UI.Middleware;
using FamilyHubs.SharedKernel.GovLogin.AppStart;
using FamilyHubs.SharedKernel.Razor.FamilyHubsUi.Extensions;
using FamilyHubs.SharedKernel.Security;
using FluentValidation;
using MediatR;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Serilog;
using Serilog.Events;

namespace FamilyHubs.Idams.Maintenance.UI;

[ExcludeFromCodeCoverage]
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
        });
    }

    public static void ConfigureServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddApplicationInsightsTelemetry();
        services.AddTransient<IIdamService, IdamService>();

        services.AddSingleton((serviceProvider) =>
        {
            IKeyProvider keyProvider = serviceProvider.GetRequiredService<IKeyProvider>();
            var byteEncryptionKey = ApplicationDbContext.ConvertStringToByteArray(keyProvider.GetDbEncryptionKey());
            var byteEncryptionIv = ApplicationDbContext.ConvertStringToByteArray(keyProvider.GetDbEncryptionIvKey());
            return new AesProvider(byteEncryptionKey, byteEncryptionIv);
        });

        services.AddClientServices(configuration);

#if USE_Authorization
        services.AddAndConfigureGovUkAuthentication(configuration);
#endif


        services.AddRazorPages();

#if USE_Authorization

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
#endif
        
        services.AddFamilyHubs(configuration);
        services.AddFamilyHubsUi(configuration);
    }

    public static void RegisterApplicationComponents(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IKeyProvider, KeyProvider>();
        services.RegisterAppDbContext(configuration);
        services.RegisterMediator();
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
    
    private static void RegisterAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<AuditableEntitySaveChangesInterceptor>();
        services.AddTransient<IRepository, ApplicationRepository>();
        
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

    public static IServiceCollection AddClientServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddClient<IServiceDirectoryClient>(configuration, "ServiceDirectoryApiBaseUrl", (httpClient, serviceProvider) =>
        {
            var logger = serviceProvider.GetService<ILogger<ServiceDirectoryClient>>();

            return new ServiceDirectoryClient(httpClient, logger!);
        });

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

            httpClient.DefaultRequestHeaders.Add("X-Correlation-ID", Guid.NewGuid().ToString());
            return instance.Invoke(httpClient, s);
        });
    }

    public static void ConfigureWebApplication(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        app.UseMiddleware<CorrelationMiddleware>();

        app.UseFamilyHubs();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();
#if USE_Authorization
        app.UseGovLoginAuthentication();
#endif

        app.MapRazorPages();
    }
}
