using TestStack.BDDfy;
using Xunit;
using FamilyHubs.ServiceDirectory.Api.AcceptanceTests.Tests.Steps;

namespace FamilyHubs.ServiceDirectory.Api.AcceptanceTests.Tests.Stories;

public class ApiInfoTests
{
    private readonly ApiInfoSteps _steps = new();

    [Fact]
    public void Api_Info_Returned()
    {
        this.When(s => _steps.CheckTheApiInfo())
            .Then(s => _steps.AnOkStatusCodeIsReturned())
            .BDDfy();
    }
}