using System.Net;
using FamilyHubs.Referral.Api.AcceptanceTests.Tests.Steps;
using TestStack.BDDfy;
using Xunit;

namespace FamilyHubs.Referral.Api.AcceptanceTests.Tests.Stories;

public class ApiInfoTests
{
    private readonly ApiInfoSteps _steps;
    private readonly SharedSteps _sharedSteps;

    public ApiInfoTests()
    {
        _steps = new ApiInfoSteps();
        _sharedSteps = new SharedSteps();
    }

    [Fact]
    public void Api_Info_Returned()
    {
        this.When(s => _steps.CheckTheApiInfo())
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.lastResponse, HttpStatusCode.OK))
            .BDDfy();
    }
}