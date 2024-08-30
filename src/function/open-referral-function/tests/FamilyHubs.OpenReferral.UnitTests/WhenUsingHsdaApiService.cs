using System.Net;
using FamilyHubs.OpenReferral.Function.ClientServices;
using FamilyHubs.OpenReferral.Function.Entities;
using FamilyHubs.OpenReferral.UnitTests.Helpers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NSubstitute;

namespace FamilyHubs.OpenReferral.UnitTests;

public class WhenUsingHsdaApiService
{
    private readonly IHsdaApiService _hsdaApiService;
    private readonly MockHttpMessageHandler _mockHttpMessageHandler;

    public WhenUsingHsdaApiService()
    {
        ILogger<HsdaApiService> loggerApiReceiverMock = Substitute.For<ILogger<HsdaApiService>>();
        _mockHttpMessageHandler = new MockHttpMessageHandler();

        HttpClient httpClient = Substitute.For<HttpClient>(_mockHttpMessageHandler);
        httpClient.BaseAddress = new Uri("http://localhost:16384");

        _hsdaApiService = new HsdaApiService(loggerApiReceiverMock, httpClient);
    }

    [Theory]
    [InlineData("{\"contents\":[{\"id\":\"ABC\"}]}", 1)] // 1 Service
    [InlineData("{\"contents\":[{\"id\":\"ABC\"},{\"id\":\"DEF\"}]}", 2)] // 2 Services
    public async Task Then_GetServices_Returns_CorrectData(string content, int expectedCount)
    {
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.OK;
        _mockHttpMessageHandler.Content = content;

        (HttpStatusCode httpStatusCode, JArray? services) = await _hsdaApiService.GetServices();

        Assert.Equal(HttpStatusCode.OK, httpStatusCode);
        Assert.NotNull(services);
        Assert.Equal(expectedCount, services.Count);
    }

    [Fact]
    public async Task Then_GetServices_Returns_500_InternalServerError_When_EndpointIsDown()
    {
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.InternalServerError;
        _mockHttpMessageHandler.Content = "";

        (HttpStatusCode httpStatusCode, JArray? services) = await _hsdaApiService.GetServices();

        Assert.Equal(HttpStatusCode.InternalServerError, httpStatusCode);
        Assert.Null(services);
    }

    [Fact]
    public async Task Then_GetServices_Returns_NotFound_When_ResponseIsEmpty()
    {
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.NotFound;
        _mockHttpMessageHandler.Content = "{\"contents\":[]}";

        (HttpStatusCode httpStatusCode, JArray? services) = await _hsdaApiService.GetServices();

        Assert.Equal(HttpStatusCode.NotFound, httpStatusCode);
        Assert.Null(services);
    }

    [Fact]
    public async Task Then_GetServicesById_Returns_CorrectData()
    {
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.OK;
        _mockHttpMessageHandler.Content = "{\"contents\":[{\"id\":\"ABC\"}]}";

        (HttpStatusCode _, JArray? services) = await _hsdaApiService.GetServices();

        Assert.NotNull(services);

        _mockHttpMessageHandler.Content = "{\"id\":\"ABC\",\"name\":\"Test\"}";

        List<ServiceJson> servicesById = await  _hsdaApiService.GetServicesById(services);

        Assert.Single(servicesById);
        Assert.Equal("ABC", servicesById[0].Id);

        string? serviceName = JObject.Parse(servicesById[0].Json)["name"]?.ToString();

        Assert.NotNull(serviceName);
        Assert.Equal("Test", serviceName);
    }

    [Fact]
    public async Task Then_GetServicesById_Continues_When_AResultDidNotComeBack()
    {
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.OK;
        _mockHttpMessageHandler.Content = "{\"contents\":[{\"id\":\"ABC\"}]}";

        (HttpStatusCode _, JArray? services) = await _hsdaApiService.GetServices();

        Assert.NotNull(services);

        _mockHttpMessageHandler.StatusCode = HttpStatusCode.NotFound;
        _mockHttpMessageHandler.Content = "";

        List<ServiceJson> servicesById = await  _hsdaApiService.GetServicesById(services);

        Assert.Empty(servicesById);
    }
}