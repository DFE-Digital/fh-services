using FamilyHubs.SharedKernel.GovLogin.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;

namespace FamilyHubs.SharedKernel.UnitTests.Identity.TestHelpers;

internal static class FakeConfiguration
{
    internal static IConfiguration GetConfiguration()
    {
        var configSource = new MemoryConfigurationSource
        {
            InitialData = new List<KeyValuePair<string, string?>>
            {
                new("GovUkOidcConfiguration:Oidc:BaseUrl", "https://test.com"),
                new("GovUkOidcConfiguration:Oidc:ClientId", "1234567"),
                new("GovUkOidcConfiguration:Oidc:KeyVaultIdentifier", "https://test.com/"),
                new("GovUkOidcConfiguration:Urls:AccountSuspendedRedirect", "https://familyhubs-test.com/service/account-unavailable"),
                new("GovUkOidcConfiguration:IdamsApiBaseUrl", "https://test.com/"),
                new("GovUkOidcConfiguration:CookieName", "UnitTestCookieName"),
                new("ResourceEnvironmentName", "AT")
            }
        };

        var provider = new MemoryConfigurationProvider(configSource);

        return new ConfigurationRoot(new List<IConfigurationProvider> { provider });
    }

    internal static GovUkOidcConfiguration GetOidcConfiguration()
    {
        var config = new GovUkOidcConfiguration
        {
            Oidc = new Oidc
            {
                BaseUrl = "https://test.com",
                ClientId = "1234567",
                KeyVaultIdentifier = "https://test.com/"
            },
            Urls = new Urls
            {
                AccountSuspendedRedirect = "https://familyhubs-test.com/service/account-unavailable"
            },
            IdamsApiBaseUrl = "https://test.com/",
            CookieName = "UnitTestCookieName"
        };

        return config;
    }
}