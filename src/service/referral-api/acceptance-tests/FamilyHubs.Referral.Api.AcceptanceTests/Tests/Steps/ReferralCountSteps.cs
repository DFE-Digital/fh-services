using FamilyHubs.ReferralService.Shared.Dto;
using System.Net;
using FamilyHubs.Referral.Api.AcceptanceTests.Builders.Http;
using FamilyHubs.Referral.Api.AcceptanceTests.Configuration;
using FamilyHubs.ReferralService.Shared.Dto.CreateUpdate;
using FamilyHubs.ReferralService.Shared.Dto.Metrics;

namespace FamilyHubs.Referral.Api.AcceptanceTests.Tests.Steps;

/// <summary>
/// These are the steps required for testing the Postcodes endpoints
/// </summary>
public class ReferralCountSteps
{
    private readonly string _baseUrl;
    private HttpStatusCode _statusCode;
    public HttpResponseMessage lastResponse { get; private set; }
    public ReferralCountSteps()
    {
        _baseUrl = ConfigAccessor.GetApplicationConfiguration().BaseUrl;
        lastResponse = new HttpResponseMessage();
    }

    private static string ResponseNotExpectedMessage(HttpMethod method, System.Uri requestUri,
        HttpStatusCode statusCode)
    {
        return $"Response from {method} {requestUri} {statusCode} was not as expected";
    }

    #region Step Definitions

    #region When

    public async Task<HttpStatusCode> WhenISendARequest(string bearerToken, string serviceId)
    {
        Dictionary<string, string> headers = new Dictionary<string, string>() { };
        headers.Add("traceparent", new Guid().ToString());
        
        lastResponse = await HttpRequestFactory.Get(_baseUrl, $"api/referral/count?serviceId={serviceId}", bearerToken,
            headers, null);
        _statusCode = lastResponse.StatusCode;

        return _statusCode;
    }

    #endregion When

    #endregion Step Definitions
}