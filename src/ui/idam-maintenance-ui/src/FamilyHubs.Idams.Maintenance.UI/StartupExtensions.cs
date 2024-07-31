using FamilyHubs.Idams.Maintenance.Core.ApiClient;
using FamilyHubs.Idams.Maintenance.Core.Commands.Add;
using FamilyHubs.Idams.Maintenance.Core.DistributedCache;
using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.Idams.Maintenance.Data.Interceptors;
using FamilyHubs.Idams.Maintenance.Data.Repository;
using FamilyHubs.Idams.Maintenance.UI.Middleware;
using FamilyHubs.SharedKernel.DataProtection;
using FamilyHubs.SharedKernel.GovLogin.AppStart;
using FamilyHubs.SharedKernel.GovLogin.Configuration;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Razor.FamilyHubsUi.Extensions;
using FamilyHubs.SharedKernel.Security;
using FluentValidation;
using MediatR;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.Extensions.Caching.Distributed;
using Serilog;
using Serilog.Events;

namespace FamilyHubs.Idams.Maintenance.UI;

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
#if MABUSE_DistributedCache
        services.AddSingleton<ICacheService, CacheService>();
#endif
        services.AddScoped<ICorrelationService, CorrelationService>();
        services.AddTransient<IIdamService, IdamService>();

        services.AddSingleton((serviceProvider) =>
        {
            IKeyProvider keyProvider = serviceProvider.GetRequiredService<IKeyProvider>();

            string? encryptionKey = keyProvider.GetDbEncryptionKey().Result;
            if (string.IsNullOrEmpty(encryptionKey))
                throw new ArgumentException("EncryptionKey is missing");

            byte[]? byteEncryptionKey = ApplicationDbContext.ConvertStringToByteArray(encryptionKey);

            string? encryptionIV = keyProvider.GetDbEncryptionIVKey().Result;
            if (string.IsNullOrEmpty(encryptionIV))
                throw new ArgumentException("EncryptionIV is missing");

            byte[]? byteEncryptionIV = ApplicationDbContext.ConvertStringToByteArray(encryptionIV);

            return new AesProvider(byteEncryptionKey, byteEncryptionIV);
        });

        services.AddClientServices(configuration);
#if MABUSE_DistributedCache
        services.AddWebUiServices(configuration);
#endif

#if USE_Authorization
        services.AddAndConfigureGovUkAuthentication(configuration);
#endif


        services.AddRazorPages(options =>
        {

        });

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

#if MABUSE_DistributedCache
        services.AddDistributedCache(configuration);
#endif

        services.AddFamilyHubs(configuration);
        services.AddFamilyHubsUi(configuration);
    }

    public static void RegisterApplicationComponents(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IKeyProvider, KeyProvider>();
        services.AddSingleton<ICrypto, Crypto>();
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

#if MABUSE_DistributedCache
    public static IServiceCollection AddDistributedCache(this IServiceCollection services, ConfigurationManager configuration)
    {
        var cacheConnection = configuration.GetValue<string>("CacheConnection");

        if (string.IsNullOrWhiteSpace(cacheConnection))
        {
            services.AddDistributedMemoryCache();
        }
        else
        {
            var tableName = "IdamMaintenanceCache";
            CheckCreateCacheTable(tableName, cacheConnection);
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = cacheConnection;
                options.TableName = tableName;
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

    private static void CheckCreateCacheTable(string tableNam, string cacheConnectionString)
    {
        try
        {
            using var sqlConnection = new SqlConnection(cacheConnectionString);
            sqlConnection.Open();

            var checkTableExistsCommandText = $"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='{tableNam}') SELECT 1 ELSE SELECT 0";
            var checkCmd = new SqlCommand(checkTableExistsCommandText, sqlConnection);

            // IF EXISTS returns the SELECT 1 if the table exists or SELECT 0 if not
            var tableExists = Convert.ToInt32(checkCmd.ExecuteScalar());
            if (tableExists == 1) return;

            var createTableExistsCommandText = @$"
            CREATE TABLE [dbo].[{tableNam}](
                [Id] [nvarchar](449) NOT NULL,
                [Value] [varbinary](max) NOT NULL,
                [ExpiresAtTime] [datetimeoffset] NOT NULL,
                [SlidingExpirationInSeconds] [bigint] NULL,
                [AbsoluteExpiration] [datetimeoffset] NULL,
                INDEX Ix_{tableNam}_ExpiresAtTime NONCLUSTERED ([ExpiresAtTime]),
                CONSTRAINT Pk_{tableNam}_Id PRIMARY KEY CLUSTERED ([Id] ASC) WITH 
                    (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
                     IGNORE_DUP_KEY = OFF,
                     ALLOW_ROW_LOCKS = ON,
                     ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];";

            var createCmd = new SqlCommand(createTableExistsCommandText, sqlConnection);
            createCmd.ExecuteNonQuery();
            sqlConnection.Close();
        }
        catch (Exception e)
        {
            Log.Fatal(e, "An unhandled exception occurred during setting up Sql Cache");
            throw;
        }
    }
#endif
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

#if MABUSE_DistributedCache
    private static void AddWebUiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

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
#endif

    public static IServiceCollection AddClientServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddClient<IServiceDirectoryClient>(configuration, "ServiceDirectoryApiBaseUrl", (httpClient, serviceProvider) =>
        {
            var cacheService = serviceProvider.GetService<ICacheService>();
            var logger = serviceProvider.GetService<ILogger<ServiceDirectoryClient>>();

            return new ServiceDirectoryClient(httpClient, cacheService!, logger!);
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
            var correlationService = s.GetService<ICorrelationService>();

            var httpClient = clientFactory?.CreateClient(name);

            ArgumentNullException.ThrowIfNull(httpClient);
            ArgumentNullException.ThrowIfNull(correlationService);

            httpClient.DefaultRequestHeaders.Add("X-Correlation-ID", correlationService.CorrelationId);
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
