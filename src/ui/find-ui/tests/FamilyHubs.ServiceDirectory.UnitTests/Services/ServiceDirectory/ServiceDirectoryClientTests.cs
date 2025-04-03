using FamilyHubs.ServiceDirectory.Infrastructure.Services.ServiceDirectory;
using Microsoft.Extensions.Configuration;

namespace FamilyHubs.ServiceDirectory.UnitTests.Services.ServiceDirectory;

public class ServiceDirectoryClientTests
{
    [Fact]
    public void HealthUrl_ReturnsCorrectUrl()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(
                new Dictionary<string, string?> {
                    {"ServiceDirectoryAPI:Endpoint", "https://example.domain"},
                }
            )
            .Build();
        var uri = ServiceDirectoryClient.HealthUrl(config);

        Assert.Equal("https://example.domain/api/info", uri.ToString());
    }
}
