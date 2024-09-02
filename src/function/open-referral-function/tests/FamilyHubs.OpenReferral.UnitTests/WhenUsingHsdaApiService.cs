using System.Net;
using System.Text.Json;
using FamilyHubs.OpenReferral.Function.ClientServices;
using FamilyHubs.OpenReferral.Function.Entities;
using FamilyHubs.OpenReferral.UnitTests.Helpers;
using Microsoft.Extensions.Logging;
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

        (HttpStatusCode httpStatusCode, JsonElement.ArrayEnumerator? services) = await _hsdaApiService.GetServices();

        Assert.Equal(HttpStatusCode.OK, httpStatusCode);
        Assert.NotNull(services);
        Assert.Equal(expectedCount, services.Value.Count());
    }

    [Fact]
    public async Task Then_GetServices_Returns_500_InternalServerError_When_EndpointIsDown()
    {
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.InternalServerError;
        _mockHttpMessageHandler.Content = "";

        (HttpStatusCode httpStatusCode, JsonElement.ArrayEnumerator? services) = await _hsdaApiService.GetServices();

        Assert.Equal(HttpStatusCode.InternalServerError, httpStatusCode);
        Assert.Null(services);
    }

    [Fact]
    public async Task Then_GetServices_Returns_NotFound_When_ResponseIsEmpty()
    {
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.NotFound;
        _mockHttpMessageHandler.Content = "{\"contents\":[]}";

        (HttpStatusCode httpStatusCode, JsonElement.ArrayEnumerator? services) = await _hsdaApiService.GetServices();

        Assert.Equal(HttpStatusCode.NotFound, httpStatusCode);
        Assert.Null(services);
    }

    [Fact]
    public async Task Then_GetServicesById_Returns_CorrectData()
    {
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.OK;
        _mockHttpMessageHandler.Content = "{\"contents\":[{\"id\":\"ABC\"}]}";

        (HttpStatusCode _, JsonElement.ArrayEnumerator? services) = await _hsdaApiService.GetServices();

        Assert.NotNull(services);

        _mockHttpMessageHandler.Content = "{\"id\":\"ABC\",\"name\":\"Test\"}";

        (HttpStatusCode httpStatusCode, List<ServiceJson> servicesById) = await _hsdaApiService.GetServicesById(services.Value);

        Assert.Equal(HttpStatusCode.OK, httpStatusCode);

        Assert.Single(servicesById);
        Assert.Equal("ABC", servicesById[0].Id);

        string serviceName = JsonDocument.Parse(servicesById[0].Json).RootElement.GetProperty("name").ToString();

        Assert.Equal("Test", serviceName);
    }

    [Fact]
    public async Task Then_GetServicesById_Continues_When_AResultDidNotComeBack()
    {
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.OK;
        _mockHttpMessageHandler.Content = "{\"contents\":[{\"id\":\"ABC\"}]}";

        (HttpStatusCode _, JsonElement.ArrayEnumerator? services) = await _hsdaApiService.GetServices();

        Assert.NotNull(services);

        _mockHttpMessageHandler.StatusCode = HttpStatusCode.NotFound;
        _mockHttpMessageHandler.Content = "";

        (HttpStatusCode httpStatusCode, List<ServiceJson> servicesById) = await _hsdaApiService.GetServicesById(services.Value);

        Assert.Equal(HttpStatusCode.NoContent, httpStatusCode);
        Assert.Empty(servicesById);
    }
}