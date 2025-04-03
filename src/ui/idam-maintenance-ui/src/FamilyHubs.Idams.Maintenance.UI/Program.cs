using System.Diagnostics.CodeAnalysis;
using FamilyHubs.SharedKernel.Extensions;
using Serilog;

namespace FamilyHubs.Idams.Maintenance.UI;

[ExcludeFromCodeCoverage]
public class Program
{
    private Program()
    {
    }
    
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
}

