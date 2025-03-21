using AutoMapper;
using AutoMapper.QueryableExtensions;
using FamilyHubs.ServiceDirectory.Data.Repository;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.SharedKernel.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FamilyHubs.ServiceDirectory.Api;

public class Program
{
    protected Program() { }
    public static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        Log.Information("Starting up");

        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.ConfigureAzureKeyVault();
            
            builder.ConfigureHost();

            builder.Services.RegisterApplicationComponents(builder.Configuration);

            builder.Services.ConfigureServices(builder.Configuration);
            
            var webApplication = builder.Build();

            if (builder.Configuration.GetValue<bool>("EnableRuntimeMigrations"))
            {
                var db = webApplication.Services.GetRequiredService<ApplicationDbContext>();
                await db.Database.MigrateAsync();
            }

            await WarmupAsync(webApplication.Services);
            
            webApplication.ConfigureWebApplication();

            await webApplication.RunAsync();
        }
        catch (Exception e)
        {
            if (e.GetType().Name.Equals("HostAbortedException", StringComparison.Ordinal))
            {
                //this error only occurs when DB migration is running on its own
                throw;
            }
            
            Log.Fatal(e, "An unhandled exception occurred during bootstrapping");
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
    
    private static async Task WarmupAsync(IServiceProvider serviceProvider)
    {
        // We are having performance issues on first postcode search and the line:
        // cfg.UseEntityFrameworkCoreModel<ApplicationDbContext>(serviceProvider);
        // in the ServiceExtensions.RegisterAutoMapper is taking somewhere between 5-6 seconds
        // Below warms up the EF/AutoMapping so the first call doesn't take the hit. The calling UI code has
        // a 10sec policy so quite often this will get triggered and abandon the call.
        
        try
        {
            using var scope = serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<ApplicationDbContext>();
            var mapper = scopedServices.GetRequiredService<IMapper>();
            _ = await context.Services.ProjectTo<ServiceDto>(mapper.ConfigurationProvider).FirstAsync();
        }
        catch (Exception error)
        {
            Log.Error(error, "Failed to warmup Entity Framework and Auto Mapper configuration.");
        }
    }
}