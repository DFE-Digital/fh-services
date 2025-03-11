using FamilyHubs.SharedKernel.Extensions;
using Microsoft.FeatureManagement;
using Serilog;

namespace FamilyHubs.Referral.Web;

public static class Program
{
    public static IServiceProvider ServiceProvider { get; private set; } = default!;

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

            ServiceProvider = app.ConfigureWebApplication();

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
}