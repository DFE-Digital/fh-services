using FamilyHubs.SharedKernel.Extensions;
using Serilog;

namespace FamilyHubs.ServiceDirectory.Web;

public class Program
{
    public static IServiceProvider ServiceProvider { get; private set; } = default!;

    private Program()
    {
    }
    
    public static void Main(string[] args)
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

            builder.Services.ConfigureServices(builder.Configuration);

            var app = builder.Build();

            ServiceProvider = app.ConfigureWebApplication();

            app.Run();
        }
        catch (Exception e)
        {
            Log.Fatal(e, "An unhandled exception occurred during bootstrapping");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}