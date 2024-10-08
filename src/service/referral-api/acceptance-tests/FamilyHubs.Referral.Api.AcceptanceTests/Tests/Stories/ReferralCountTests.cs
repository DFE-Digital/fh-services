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
    [InlineData("VcsManager", "15648", HttpStatusCode.OK)] // Happy path service does not exist
    public void Referral_Count_Returns_Expected_Status_Code(string role, string serviceId, HttpStatusCode expectedStatusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.WhenISendARequest(_sharedSteps.BearerToken!, serviceId))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, expectedStatusCode))
            .And(s => _steps.ResponseBodyContainsValue())
            .BDDfy();
    }

    [Theory]
    [InlineData("809", HttpStatusCode.Unauthorized)]
    public void Referral_Count_Endpoint_Errors_Invalid_Bearer_Token(string serviceId, HttpStatusCode expectedStatusCode)
    {
        this.Given(s => _sharedSteps.HaveAnInvalidBearerToken())
            .When(s => _steps.WhenISendARequest(_sharedSteps.BearerToken!,serviceId))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, expectedStatusCode))
            .BDDfy();
    }
}