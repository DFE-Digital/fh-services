using TestStack.BDDfy;
using Xunit;
using FamilyHubs.ServiceDirectory.Api.AcceptanceTests.Tests.Steps;

namespace FamilyHubs.ServiceDirectory.Api.AcceptanceTests.Tests.Stories;

public class ApiInfoTests
{
    private readonly ApiInfoSteps _steps;

    public ApiInfoTests()
    {
        _steps = new ApiInfoSteps();
    }

    [Fact]
    public void Api_Info_Returned()
    {
        this.When(s => _steps.ICheckTheApiInfo())
            .Then(s => _steps.AnOkStatusCodeIsReturned())
            .BDDfy();
    }
}