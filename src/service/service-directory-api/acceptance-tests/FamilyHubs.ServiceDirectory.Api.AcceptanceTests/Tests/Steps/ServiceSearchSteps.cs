using FluentAssertions;
using System.Net;
using FamilyHubs.ServiceDirectory.Api.AcceptanceTests.Configuration;
using FamilyHubs.ServiceDirectory.Api.AcceptanceTests.Models;
using FamilyHubs.ServiceDirectory.Api.AcceptanceTests.Builders.Http;

namespace FamilyHubs.ServiceDirectory.Api.AcceptanceTests.Tests.Steps;

/// <summary>
/// These are the steps required for testing the Postcodes endpoints
/// </summary>
public class ServiceSearchSteps
{
    private readonly string _baseUrl = ConfigAccessor.GetApplicationConfiguration().BaseUrl;
    private ServiceSearchRequest _request = new();
    private HttpResponseMessage _lastResponse = new();
    private HttpStatusCode _statusCode;
    private const string ServiceSearchPath = "api/metrics/service-search";


    public void GivenIHaveASearchServiceRequest(string radiusValue, string postcodeEntry,
        string postCodeEndpointResponseEntry, string searchTriggerEventId, string serviceSearchTypeId)
    {
        var time = DateTime.UtcNow;
        var radius = int.Parse(radiusValue);
        var postcodeEndpointStatusCode = int.Parse(postCodeEndpointResponseEntry);
        var searchTriggerEventIdEntry = int.Parse(searchTriggerEventId);
        var serviceSearchTypeIdEntry = int.Parse(serviceSearchTypeId);
        _request = new ServiceSearchRequest
        {
            searchPostcode = postcodeEntry,
            searchRadiusMiles = radius,
            userId = 0,
            httpResponseCode = postcodeEndpointStatusCode,
            requestTimestamp = time,
            responseTimestamp = time,
            correlationId = "",
            searchTriggerEventId = searchTriggerEventIdEntry,
            serviceSearchTypeId = serviceSearchTypeIdEntry,
            serviceSearchResults = new List<ServiceSearchResults>()
        };
    }

    public async Task<HttpStatusCode> WhenISendARequest()
    {
        _lastResponse = await HttpRequestFactory.Post(_baseUrl, ServiceSearchPath, _request);
        _statusCode = _lastResponse.StatusCode;

        return _statusCode;
    }

    public void ThenExpectedStatusCodeReturned(HttpStatusCode expectedStatusCode)
    {
        _statusCode.Should().Be(expectedStatusCode,
            ResponseNotExpectedMessage(
                _lastResponse.RequestMessage!.Method,
                _lastResponse.RequestMessage.RequestUri!,
                _statusCode));
    }

    private static string ResponseNotExpectedMessage(HttpMethod method, System.Uri requestUri,
        HttpStatusCode statusCode)
    {
        return $"Response from {method} {requestUri} {statusCode} was not as expected";
    }
}