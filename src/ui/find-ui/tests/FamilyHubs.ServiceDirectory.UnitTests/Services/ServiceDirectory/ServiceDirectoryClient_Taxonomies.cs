using System.Net;
using FamilyHubs.ServiceDirectory.Infrastructure.Services.ServiceDirectory;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.ServiceDirectory.Shared.Models;
using Newtonsoft.Json;
using NSubstitute;

namespace FamilyHubs.ServiceDirectory.UnitTests.Services.ServiceDirectory;

public class ServiceDirectoryClientTaxonomies
{
    private readonly IHttpClientFactory _httpClientFactory = Substitute.For<IHttpClientFactory>();

    private readonly PaginatedList<TaxonomyDto> _taxonomies = new(
        [
            new TaxonomyDto { Id = 1, Name = "Activities, clubs and groups", TaxonomyType = TaxonomyType.ServiceCategory },
            new TaxonomyDto { Id = 2, Name = "Activities", TaxonomyType = TaxonomyType.ServiceCategory, ParentId = 1 }
        ],
        2, 1, 10
    );

    [Fact]
    public async Task GetTaxonomiesUsesCache()
    {
        var sdApi = new ServiceDirectoryClient(_httpClientFactory, new NotACache(_ => (true, _taxonomies)));
        var taxonomies = await sdApi.GetTaxonomies();

        Assert.Equal(2, taxonomies.TotalCount);
    }

    [Fact]
    public async Task GetTaxonomiesFallsBackToApi()
    {
        var messageHandler = new MockHttpMessageHandler(JsonConvert.SerializeObject(_taxonomies), HttpStatusCode.OK);
        var httpClient = new HttpClient(messageHandler);
        httpClient.BaseAddress = new Uri("http://example.domain");

        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        var sdApi = new ServiceDirectoryClient(_httpClientFactory, new NotACache(_ => (false, null)));
        var taxonomies = await sdApi.GetTaxonomies();

        Assert.Equal(2, taxonomies.TotalCount);
    }

    [Fact]
    public async Task GetTaxonomiesHandlesApiErrors()
    {
        var messageHandler = new MockHttpMessageHandler("", HttpStatusCode.BadGateway);
        var httpClient = new HttpClient(messageHandler);
        httpClient.BaseAddress = new Uri("http://example.domain");

        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        var sdApi = new ServiceDirectoryClient(_httpClientFactory, new NotACache(_ => (false, null)));
        await Assert.ThrowsAsync<ServiceDirectoryClientException>(async () => await sdApi.GetTaxonomies());
    }
    
    [Fact]
    public async Task GetTaxonomiesHandlesNullResponse()
    {
        var messageHandler = new MockHttpMessageHandler("null", HttpStatusCode.OK);
        var httpClient = new HttpClient(messageHandler);
        httpClient.BaseAddress = new Uri("http://example.domain");

        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        var sdApi = new ServiceDirectoryClient(_httpClientFactory, new NotACache(_ => (false, null)));
        await Assert.ThrowsAsync<ServiceDirectoryClientException>(async () => await sdApi.GetTaxonomies());
    }
}
