using System.Net;
using FamilyHubs.Report.Api.AcceptanceTests.Builders.Http;
using FamilyHubs.Report.Api.AcceptanceTests.Configuration;
using FluentAssertions;

namespace FamilyHubs.Report.Api.AcceptanceTests.Tests.Steps;

public class ApiInfoSteps
{
    private readonly string _baseUrl;
    private HttpResponseMessage _lastResponse;

    public ApiInfoSteps()
    {
        var config = ConfigAccessor.GetApplicationConfiguration();
        _baseUrl = config.BaseUrl;
        _lastResponse = new HttpResponseMessage();
    }

    public async Task CheckTheApiInfo()
    {
        _lastResponse = await HttpRequestFactory.Get(_baseUrl, "api/info", null, null, null);
    }

    public void AnOkStatusCodeIsReturned()
    {
        _lastResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}