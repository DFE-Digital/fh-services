using Microsoft.Extensions.Configuration;

namespace FamilyHubs.ServiceDirectory.Api.AcceptanceTests.Configuration;

public static class ConfigAccessor
{
    private static IConfigurationRoot GetIConfigurationRoot()
    {
        var root = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

        return root;
    }

    public static ConfigModel GetApplicationConfiguration()
    {
        var configuration = new ConfigModel();

        var iConfig = GetIConfigurationRoot();

        iConfig.Bind(configuration);

        return configuration;
    }
}