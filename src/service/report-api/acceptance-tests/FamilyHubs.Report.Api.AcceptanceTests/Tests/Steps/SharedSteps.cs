using System.Net;
using System.Text.Json;
using FamilyHubs.Report.Api.AcceptanceTests.Fixtures;
using FluentAssertions;

namespace FamilyHubs.Report.Api.AcceptanceTests.Tests.Steps;

public class SharedSteps
{
    private readonly BearerTokenGenerator _bearerTokenGenerator = new();
    public string BearerToken { get; private set; } = null!;
    public static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

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

    #region Then

    public void VerifyStatusCode(HttpResponseMessage lastResponse, HttpStatusCode expectedStatusCode)
    {
        lastResponse.StatusCode.Should().Be(expectedStatusCode);
    }

    #endregion Then
}