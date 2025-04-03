using System.Net;
using FamilyHubs.ServiceDirectory.Core.ServiceDirectory.Models;
using FamilyHubs.ServiceDirectory.Infrastructure.Services.ServiceDirectory;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Models;
using Newtonsoft.Json;
using NSubstitute;

namespace FamilyHubs.ServiceDirectory.UnitTests.Services.ServiceDirectory;

public class ServiceDirectoryClientServices
{
    private readonly IHttpClientFactory _httpClientFactory = Substitute.For<IHttpClientFactory>();

    [Fact]
    public async Task GetServicesUsesApi()
    {
        var serviceList = new PaginatedList<ServiceDto>(
            TestData.ExampleServices,
            2, 1, 10
        );

        var messageHandler = new MockHttpMessageHandler(JsonConvert.SerializeObject(serviceList), HttpStatusCode.OK);
        var httpClient = new HttpClient(messageHandler);
        httpClient.BaseAddress = new Uri("http://example.domain");

        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        var sdApi = new ServiceDirectoryClient(_httpClientFactory, new NotACache(_ => (false, null)));
        var (services, _) = await sdApi.GetServices(new ServicesParams("A", 0, 0));

        Assert.Equal(2, services.TotalCount);
    }

    [Fact]
    public async Task GetServicesHandlesApiErrors()
    {
        var messageHandler = new MockHttpMessageHandler("", HttpStatusCode.BadGateway);
        var httpClient = new HttpClient(messageHandler);
        httpClient.BaseAddress = new Uri("http://example.domain");

        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        var sdApi = new ServiceDirectoryClient(_httpClientFactory, new NotACache(_ => (false, null)));
        await Assert.ThrowsAsync<ServiceDirectoryClientException>(async () => await sdApi.GetServices(new ServicesParams("A", 0, 0)));
    }

    [Fact]
    public async Task GetServicesHandlesNullResponse()
    {
        var messageHandler = new MockHttpMessageHandler("null", HttpStatusCode.OK);
        var httpClient = new HttpClient(messageHandler);
        httpClient.BaseAddress = new Uri("http://example.domain");

        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        var sdApi = new ServiceDirectoryClient(_httpClientFactory, new NotACache(_ => (false, null)));
        await Assert.ThrowsAsync<ServiceDirectoryClientException>(async () => await sdApi.GetServices(new ServicesParams("A", 0, 0)));
    }
}
