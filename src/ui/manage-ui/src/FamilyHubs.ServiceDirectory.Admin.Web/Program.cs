using FamilyHubs.SharedKernel.Extensions;
using Microsoft.FeatureManagement;
using Serilog;

namespace FamilyHubs.ServiceDirectory.Admin.Web;

public class Program
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
            
            builder.Services.AddFeatureManagement(builder.Configuration.GetSection("FeatureManagement"));

            builder.Services.ConfigureServices(builder.Configuration);

            var app = builder.Build();

            app.ConfigureWebApplication();

            await app.RunAsync();
        }
        catch (Exception e)
        {
            Log.Fatal(e, "An unhandled exception occurred during bootstrapping");
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }

    protected Program()
    {
    }
}