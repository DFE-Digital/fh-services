using FamilyHubs.ReferralService.Shared.Dto;
using System.Net;
using FamilyHubs.Referral.Api.AcceptanceTests.Builders.Http;
using FamilyHubs.Referral.Api.AcceptanceTests.Configuration;
using FamilyHubs.ReferralService.Shared.Dto.CreateUpdate;
using FamilyHubs.ReferralService.Shared.Dto.Metrics;
using FluentAssertions;

namespace FamilyHubs.Referral.Api.AcceptanceTests.Tests.Steps;

/// <summary>
/// These are the steps required for testing the Postcodes endpoints
/// </summary>
public class ReferralCountSteps
{
    private readonly string _baseUrl;
    private HttpStatusCode _statusCode;
    public HttpResponseMessage LastResponse { get; private set; }
    public ReferralCountSteps()
    {
        _baseUrl = ConfigAccessor.GetApplicationConfiguration().BaseUrl;
        LastResponse = new HttpResponseMessage();
    }

    #region Step Definitions

    #region When

    public async Task<HttpStatusCode> WhenISendARequest(string bearerToken, string serviceId)
    {
        Dictionary<string, string> headers = new Dictionary<string, string>() { };
        headers.Add("traceparent", new Guid().ToString());
        
        LastResponse = await HttpRequestFactory.Get(_baseUrl, $"api/referral/count?serviceId={serviceId}", bearerToken,
            headers, null);
        _statusCode = LastResponse.StatusCode;

        return _statusCode;
    }

    #endregion When

    #region Then
    
    public void ResponseBodyContainsValue()
    {
        LastResponse.Content.Should().NotBeNull();
    }

    #endregion Then

    #endregion Step Definitions
}