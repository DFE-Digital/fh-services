using System.Net;
using System.Text.Json;
using FamilyHubs.ServiceDirectory.Infrastructure.Services.ServiceDirectory;
using FamilyHubs.ServiceDirectory.Shared.Dto.Metrics;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using NSubstitute;

namespace FamilyHubs.ServiceDirectory.UnitTests.Services.ServiceDirectory;

public class ServiceDirectoryClientReport
{
    private readonly IHttpClientFactory _httpClientFactory = Substitute.For<IHttpClientFactory>();

    [Fact]
    public async Task GetServicesUsesApi()
    {
        var messageHandler = new MockHttpMessageHandler("", HttpStatusCode.OK);
        var httpClient = new HttpClient(messageHandler);
        httpClient.BaseAddress = new Uri("http://example.domain");

        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        var sdApi = new ServiceDirectoryClient(_httpClientFactory, new NotACache(_ => (false, null)));
        await sdApi.RecordServiceSearch(
            ServiceDirectorySearchEventType.ServiceDirectoryInitialSearch,
            "W1A 1AA",
            "DC0001",
            10,
            [],
            DateTime.UtcNow,
            DateTime.UtcNow,
            HttpStatusCode.OK,
            Guid.NewGuid()
        );

        Assert.Equal(1, messageHandler.NumberOfCalls);
        var ssDto = JsonSerializer.Deserialize<ServiceSearchDto>(messageHandler.Input, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        Assert.Equal("W1A 1AA", ssDto.SearchPostcode);
    }
}
