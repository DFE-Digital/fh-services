using System.Net;
using FamilyHubs.OpenReferral.Function.ClientServices;
using FamilyHubs.OpenReferral.Function.Functions;
using FamilyHubs.OpenReferral.Function.Models;
using FamilyHubs.OpenReferral.Function.Services;
using FamilyHubs.OpenReferral.UnitTests.Helpers;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace FamilyHubs.OpenReferral.UnitTests;

public class WhenUsingTriggerPullServicesWebhook
{
    private readonly TriggerPullServicesWebhook _triggerPullServicesWebhook;

    private readonly IHsdaApiService _hsdaApiServiceMock;
    private readonly HttpRequestData _reqMock;

    public WhenUsingTriggerPullServicesWebhook()
    {
        var loggerApiReceiverMock = Substitute.For<ILogger<TriggerPullServicesWebhook>>();

        _hsdaApiServiceMock = Substitute.For<IHsdaApiService>();

        var dedsServiceMock = Substitute.For<IDedsService>();

        _reqMock = Substitute.For<HttpRequestData>(Substitute.For<FunctionContext>());
        _reqMock.CreateResponse().Returns(Substitute.For<HttpResponseData>(Substitute.For<FunctionContext>()));

        _triggerPullServicesWebhook = new TriggerPullServicesWebhook(loggerApiReceiverMock, _hsdaApiServiceMock, dedsServiceMock);

    }

    [Fact]
    public async Task Then_NormalOperation_BeingReturned_ShouldResultIn_200_OK()
    {
        List<ServiceDto> servicesById = [ MockService.Service ];

        _hsdaApiServiceMock.GetServices().Returns((HttpStatusCode.OK, []));
        _hsdaApiServiceMock.GetServicesById(default).Returns((HttpStatusCode.OK, servicesById));

        HttpResponseData response = await _triggerPullServicesWebhook.Run(_reqMock);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Then_FailedGet_Should_ResultIn_500_InternalServerError()
    {
        _hsdaApiServiceMock.GetServices().Returns((HttpStatusCode.InternalServerError, null));

        var response = await _triggerPullServicesWebhook.Run(_reqMock);

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}