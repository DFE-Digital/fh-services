
using FamilyHubs.Idam.Data.Repository;
using FamilyHubs.SharedKernel.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FamilyHubs.Idam.Api;

#pragma warning disable S1118
public class Program
#pragma warning disable S1118
{
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
}