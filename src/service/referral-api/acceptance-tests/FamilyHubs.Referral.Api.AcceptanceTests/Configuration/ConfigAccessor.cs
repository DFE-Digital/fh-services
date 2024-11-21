using FamilyHubs.SharedKernel.Extensions;
using Microsoft.Extensions.Configuration;

namespace FamilyHubs.Referral.Api.AcceptanceTests.Configuration;

public class ConfigAccessor
{
    private static IConfigurationRoot? _root;

    private static IConfigurationRoot GetIConfigurationRoot()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddUserSecrets<ConfigAccessor>()
            .AddEnvironmentVariables();
        
        builder.ConfigureAzureKeyVault();

        return _root ??= builder.Build();
    }

    public static ConfigModel GetApplicationConfiguration()
    {
        var configuration = new ConfigModel();

        var iConfig = GetIConfigurationRoot();

        iConfig.Bind(configuration);

        return configuration;
    }
}