using System.Net;
using FamilyHubs.OpenReferral.Function.ClientServices;
using FamilyHubs.OpenReferral.Function.Entities;
using FamilyHubs.OpenReferral.Function.Functions;
using FamilyHubs.OpenReferral.Function.Repository;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace FamilyHubs.OpenReferral.UnitTests;

public class WhenUsingTriggerPullServicesWebhook
{
    private readonly TriggerPullServicesWebhook _triggerPullServicesWebhook;

    private readonly IHsdaApiService _hsdaApiServiceMock;
    private readonly IFunctionDbContext _functionDbContextMock;
    private readonly HttpRequestData _reqMock;

    public WhenUsingTriggerPullServicesWebhook()
    {
        ILogger<TriggerPullServicesWebhook> loggerApiReceiverMock = Substitute.For<ILogger<TriggerPullServicesWebhook>>();

        _hsdaApiServiceMock = Substitute.For<IHsdaApiService>();

        _functionDbContextMock = Substitute.For<IFunctionDbContext>();

        _reqMock = Substitute.For<HttpRequestData>(Substitute.For<FunctionContext>());
        _reqMock.CreateResponse().Returns(Substitute.For<HttpResponseData>(Substitute.For<FunctionContext>()));

        _triggerPullServicesWebhook = new TriggerPullServicesWebhook(loggerApiReceiverMock, _hsdaApiServiceMock, _functionDbContextMock);
    }

    [Fact]
    public async Task Then_NormalOperation_BeingReturned_ShouldResultIn_200_OK()
    {
        List<ServiceJson> serviceJsonList = [new ServiceJson { Id = Guid.NewGuid().ToString(), Json = "OK" }];

        _hsdaApiServiceMock.GetServices().Returns((HttpStatusCode.OK, serviceJsonList));

        HttpResponseData response = await _triggerPullServicesWebhook.Run(_reqMock);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // If GetServices() returns a 500, then the response of the call to the function should also be 500.
    [Fact]
    public async Task Then_FailedGet_Should_ResultIn_500_InternalServerError()
    {
        _hsdaApiServiceMock.GetServices().Returns((HttpStatusCode.InternalServerError, null));

        HttpResponseData response = await _triggerPullServicesWebhook.Run(_reqMock);

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Fact]
    public async Task Then_DatabaseFailingToUpdate_Should_ResultIn_500_InternalServerError()
    {
        List<ServiceJson> serviceJsonList = [new ServiceJson { Id = Guid.NewGuid().ToString(), Json = "OK" }];

        _hsdaApiServiceMock.GetServices().Returns((HttpStatusCode.OK, serviceJsonList));

        _functionDbContextMock.When(dbContext => dbContext.SaveChangesAsync())
            .Do(_ => throw new DbUpdateException());

        HttpResponseData response = await _triggerPullServicesWebhook.Run(_reqMock);

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}