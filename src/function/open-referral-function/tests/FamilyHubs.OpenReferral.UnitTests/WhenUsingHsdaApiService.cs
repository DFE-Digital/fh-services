using System.Net;
using System.Text.Json;
using FamilyHubs.OpenReferral.Function.ClientServices;
using FamilyHubs.OpenReferral.UnitTests.Helpers;
using FamilyHubs.SharedKernel.OpenReferral.Entities;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace FamilyHubs.OpenReferral.UnitTests;

public class WhenUsingHsdaApiService
{
    private readonly IHsdaApiService _hsdaApiService;
    private readonly MockHttpMessageHandler _mockHttpMessageHandler;

    private readonly Service _service;
    private readonly string _serviceJson;

    public WhenUsingHsdaApiService()
    {
        ILogger<HsdaApiService> loggerApiReceiverMock = Substitute.For<ILogger<HsdaApiService>>();
        _mockHttpMessageHandler = new MockHttpMessageHandler();

        HttpClient httpClient = Substitute.For<HttpClient>(_mockHttpMessageHandler);
        httpClient.BaseAddress = new Uri("http://localhost:16384");

        _hsdaApiService = new HsdaApiService(loggerApiReceiverMock, httpClient);

        _service = MockService.Service;
        _serviceJson = MockService.ServiceJson;
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
    public async Task Then_GetServices_Returns_NoContent_When_QueryIsOK_But_NoServicesWereReturned()
    {
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.OK;
        _mockHttpMessageHandler.Content = "{\"contents\":[]}";

        (HttpStatusCode httpStatusCode, JsonElement.ArrayEnumerator? services) = await _hsdaApiService.GetServices();

        Assert.Equal(HttpStatusCode.NoContent, httpStatusCode);
        Assert.Null(services);
    }

    [Fact]
    public async Task Then_GetServicesById_Returns_CorrectData()
    {
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.OK;
        _mockHttpMessageHandler.Content = "{\"contents\":[{\"id\":\"00000000-0000-0000-0000-000000000000\"}]}";

        (HttpStatusCode _, JsonElement.ArrayEnumerator? services) = await _hsdaApiService.GetServices();

        Assert.NotNull(services);

        _mockHttpMessageHandler.Content = _serviceJson;

        (HttpStatusCode httpStatusCode, List<Service> servicesById) =
            await _hsdaApiService.GetServicesById(services.Value);

        Assert.Equal(HttpStatusCode.OK, httpStatusCode);
        Assert.Single(servicesById);
        Assert.Equivalent(_service, servicesById[0]);
    }

    [Fact]
    public async Task Then_GetServicesById_Continues_When_AResultDidNotComeBack()
    {
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.OK;
        _mockHttpMessageHandler.Content = "{\"contents\":[{\"id\":\"00000000-0000-0000-0000-000000000000\"}]}";

        (HttpStatusCode _, JsonElement.ArrayEnumerator? services) = await _hsdaApiService.GetServices();

        Assert.NotNull(services);

        _mockHttpMessageHandler.Content = "";

        (HttpStatusCode httpStatusCode, List<Service> servicesById) = await _hsdaApiService.GetServicesById(services.Value);

        Assert.Equal(HttpStatusCode.NoContent, httpStatusCode);
        Assert.Empty(servicesById);
    }

    [Fact]
    public async Task Then_GetServicesById_Continues_When_IncomingJson_IsNot_A_Valid_Service()
    {
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.OK;
        _mockHttpMessageHandler.Content = "{\"contents\":[{\"id\":\"00000000-0000-0000-0000-000000000000\"}]}";

        (HttpStatusCode _, JsonElement.ArrayEnumerator? services) = await _hsdaApiService.GetServices();

        Assert.NotNull(services);

        _mockHttpMessageHandler.Content = "{\"incorrect\":\"schema\"}";

        (HttpStatusCode httpStatusCode, List<Service> servicesById) =
            await _hsdaApiService.GetServicesById(services.Value);

        Assert.Equal(HttpStatusCode.NoContent, httpStatusCode);
        Assert.Empty(servicesById);
    }

    [Fact]
    public async Task Then_GetServicesById_Continues_When_IncomingJson_Is_A_Null_JsonObject()
    {
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.OK;
        _mockHttpMessageHandler.Content = "{\"contents\":[{\"id\":\"00000000-0000-0000-0000-000000000000\"}]}";

        (HttpStatusCode _, JsonElement.ArrayEnumerator? services) = await _hsdaApiService.GetServices();

        Assert.NotNull(services);

        _mockHttpMessageHandler.Content = "null";

        (HttpStatusCode httpStatusCode, List<Service> servicesById) =
            await _hsdaApiService.GetServicesById(services.Value);

        Assert.Equal(HttpStatusCode.NoContent, httpStatusCode);
        Assert.Empty(servicesById);
    }
}