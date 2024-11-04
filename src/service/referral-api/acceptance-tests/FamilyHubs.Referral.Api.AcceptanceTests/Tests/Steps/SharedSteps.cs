using System.Net;
using FamilyHubs.Referral.Api.AcceptanceTests.Fixtures;
using FluentAssertions;

namespace FamilyHubs.Referral.Api.AcceptanceTests.Tests.Steps;

public class SharedSteps
{
    private readonly BearerTokenGenerator _bearerTokenGenerator;
    public SharedSteps()
    {
        _bearerTokenGenerator = new BearerTokenGenerator();
    }

    public string? BearerToken { get; private set; }

    #region Given

    public void GenerateBearerToken(string role)
    {
        BearerToken = _bearerTokenGenerator.CreateBearerToken(role);
    }

    public void HaveAnInvalidBearerToken()
    {
        BearerToken =
            "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZb2xlIjoiRGZlQWRtaW4iLCJleHAiOjE4NzY0NzUxMTJ9.n26mqEewIpsNmhVMZKqXRjnrU2LYwFHu00LCphG3V2o";
    }

    #endregion Given

    #region When

    public void VerifyStatusCode(HttpResponseMessage lastResponse, HttpStatusCode expectedStatusCode)
    {
        lastResponse.StatusCode.Should().Be(expectedStatusCode);
    }

    #endregion When
}