using System.Net;
using FamilyHubs.Referral.Api.AcceptanceTests.Tests.Steps;
using TestStack.BDDfy;
using Xunit;

namespace FamilyHubs.Referral.Api.AcceptanceTests.Tests.Stories;

// Define the story/feature being tested
[Story(
    AsA = "user of the referral api",
    IWant = "to be able to get the count of open referrals",
    SoThat = "I can know how many referrals there are")]
public class ReferralCountTests
{
    private readonly ReferralCountSteps _steps;
    private readonly SharedSteps _sharedSteps;

    public ReferralCountTests()
    {
        _steps = new ReferralCountSteps();
        _sharedSteps = new SharedSteps();
    }

    [Theory]
    [InlineData("VcsDualRole", "809", HttpStatusCode.OK)] // Happy path as VCS Dual User
    [InlineData("VcsManager", "809", HttpStatusCode.OK)] // Happy path as VCS Dual User
    [InlineData("LaProfessional", "809", HttpStatusCode.Forbidden)] // Unauthorised as VCS professional
    [InlineData("VcsProfessional","809", HttpStatusCode.Forbidden)] // Unauthorised as La Professional
    public void Referral_Count_Returns_Expected_Status_Code(string role, string serviceId, HttpStatusCode expectedStatusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.WhenISendARequest(_sharedSteps.bearerToken, serviceId))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.lastResponse, expectedStatusCode))
            .BDDfy();
    }

    [Theory]
    [InlineData("809", HttpStatusCode.Unauthorized)]
    public void Referral_Count_Endpoint_Errors_Invalid_Bearer_Token(string serviceId, HttpStatusCode expectedStatusCode)
    {
        this.Given(s => _sharedSteps.HaveAnInvalidBearerToken())
            .When(s => _steps.WhenISendARequest(_sharedSteps.bearerToken,serviceId))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.lastResponse, expectedStatusCode))
            .BDDfy();
    }
}