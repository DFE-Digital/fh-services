using System.Net;
using FamilyHubs.ServiceDirectory.Infrastructure.Services.ServiceDirectory;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using Newtonsoft.Json;
using NSubstitute;

namespace FamilyHubs.ServiceDirectory.UnitTests.Services.ServiceDirectory;

public class ServiceDirectoryClientOrganisations
{
    private readonly IHttpClientFactory _httpClientFactory = Substitute.For<IHttpClientFactory>();

    private readonly OrganisationDto _exampleOrg = new()
    {
        OrganisationType = OrganisationType.LA,
        Name = "Test LA",
        Description = "Test LA Description",
        AdminAreaCode = "AAC0001"
    };

    [Fact]
    public async Task GetOrganisationsUsesCache()
    {
        var sdApi = new ServiceDirectoryClient(_httpClientFactory, new NotACache(_ => (true, _exampleOrg)));
        var organisation = await sdApi.GetOrganisation(1);

        Assert.NotNull(organisation);
    }

    [Fact]
    public async Task GetOrganisationsFallsBackToApi()
    {
        var messageHandler = new MockHttpMessageHandler(JsonConvert.SerializeObject(_exampleOrg), HttpStatusCode.OK);
        var httpClient = new HttpClient(messageHandler);
        httpClient.BaseAddress = new Uri("http://example.domain");

        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        var sdApi = new ServiceDirectoryClient(_httpClientFactory, new NotACache(_ => (false, null)));
        var organisation = await sdApi.GetOrganisation(1);

        Assert.NotNull(organisation);
    }

    [Fact]
    public async Task GetOrganisations0NotAllowed()
    {
        var sdApi = new ServiceDirectoryClient(_httpClientFactory, new NotACache(_ => (false, null)));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await sdApi.GetOrganisation(0));
    }
    
    [Fact]
    public async Task GetOrganisationsHandlesApiErrors()
    {
        var messageHandler = new MockHttpMessageHandler("", HttpStatusCode.BadGateway);
        var httpClient = new HttpClient(messageHandler);
        httpClient.BaseAddress = new Uri("http://example.domain");

        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        var sdApi = new ServiceDirectoryClient(_httpClientFactory, new NotACache(_ => (false, null)));
        await Assert.ThrowsAsync<ServiceDirectoryClientException>(async () => await sdApi.GetOrganisation(1));
    }

    [Fact]
    public async Task GetOrganisationsHandlesNullResponse()
    {
        var messageHandler = new MockHttpMessageHandler("null", HttpStatusCode.OK);
        var httpClient = new HttpClient(messageHandler);
        httpClient.BaseAddress = new Uri("http://example.domain");

        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        var sdApi = new ServiceDirectoryClient(_httpClientFactory, new NotACache(_ => (false, null)));
        await Assert.ThrowsAsync<ServiceDirectoryClientException>(async () => await sdApi.GetOrganisation(1));
    }
}
