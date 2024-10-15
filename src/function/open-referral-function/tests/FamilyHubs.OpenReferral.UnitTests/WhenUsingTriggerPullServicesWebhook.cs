using System.Net;
using FamilyHubs.OpenReferral.Function.ClientServices;
using FamilyHubs.OpenReferral.Function.Functions;
using FamilyHubs.OpenReferral.Function.Repository;
using FamilyHubs.OpenReferral.UnitTests.Helpers;
using FamilyHubs.SharedKernel.OpenReferral.Entities;
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

    private readonly Service _service;

    public WhenUsingTriggerPullServicesWebhook()
    {
        ILogger<TriggerPullServicesWebhook> loggerApiReceiverMock = Substitute.For<ILogger<TriggerPullServicesWebhook>>();

        _hsdaApiServiceMock = Substitute.For<IHsdaApiService>();

        _functionDbContextMock = Substitute.For<IFunctionDbContext>();

        _reqMock = Substitute.For<HttpRequestData>(Substitute.For<FunctionContext>());
        _reqMock.CreateResponse().Returns(Substitute.For<HttpResponseData>(Substitute.For<FunctionContext>()));

        _triggerPullServicesWebhook = new TriggerPullServicesWebhook(loggerApiReceiverMock, _hsdaApiServiceMock, _functionDbContextMock);

        _service = MockService.Service;
    }

    [Fact]
    public async Task Then_NormalOperation_BeingReturned_ShouldResultIn_200_OK()
    {
        List<Service> servicesById = [ _service ];

        _hsdaApiServiceMock.GetServices().Returns((HttpStatusCode.OK, []));
        _hsdaApiServiceMock.GetServicesById(default).Returns((HttpStatusCode.OK, servicesById));

        _functionDbContextMock.ToListAsync(Arg.Any<IQueryable<Service>>()).Returns([_service]);
        _functionDbContextMock.SaveChangesAsync().Returns(Task.FromResult(1));

        HttpResponseData response = await _triggerPullServicesWebhook.Run(_reqMock);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Then_FailedGet_Should_ResultIn_500_InternalServerError()
    {
        _hsdaApiServiceMock.GetServices().Returns((HttpStatusCode.InternalServerError, null));

        HttpResponseData response = await _triggerPullServicesWebhook.Run(_reqMock);

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Fact]
    public async Task Then_NoServicesByIdReturned_Should_ResultIn_204_NoContent()
    {
        List<Service> servicesById = [];

        _hsdaApiServiceMock.GetServices().Returns((HttpStatusCode.OK, []));
        _hsdaApiServiceMock.GetServicesById(default).Returns((HttpStatusCode.NoContent, servicesById));

        HttpResponseData response = await _triggerPullServicesWebhook.Run(_reqMock);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Then_DatabaseFailingToUpdate_Should_ResultIn_500_InternalServerError()
    {
        List<Service> servicesById = [ _service ];

        _hsdaApiServiceMock.GetServices().Returns((HttpStatusCode.OK, []));
        _hsdaApiServiceMock.GetServicesById(default).Returns((HttpStatusCode.OK, servicesById));

        _functionDbContextMock.ToListAsync(Arg.Any<IQueryable<Service>>()).Returns([_service]);

        _functionDbContextMock.When(dbContext => dbContext.SaveChangesAsync())
            .Do(_ => throw new DbUpdateException());

        HttpResponseData response = await _triggerPullServicesWebhook.Run(_reqMock);

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}