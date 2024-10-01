using FluentAssertions;
using System.Net;
using FamilyHubs.ServiceDirectory.Api.AcceptanceTests.Configuration;
using FamilyHubs.ServiceDirectory.Api.AcceptanceTests.Builders.Http;

namespace FamilyHubs.ServiceDirectory.Api.AcceptanceTests.Tests.Steps;

public class ApiInfoSteps
{
    private readonly string _baseUrl;
    private HttpResponseMessage? _lastResponse;

    public ApiInfoSteps()
    {
        var config = ConfigAccessor.GetApplicationConfiguration();
        _baseUrl = config.BaseUrl;
    }

    public async Task CheckTheApiInfo()
    {
        _lastResponse = await HttpRequestFactory.Get(_baseUrl, "api/info");
    }

    public void AnOkStatusCodeIsReturned()
    {
        _lastResponse?.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}