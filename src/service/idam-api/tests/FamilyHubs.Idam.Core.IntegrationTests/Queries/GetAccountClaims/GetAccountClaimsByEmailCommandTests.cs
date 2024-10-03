using FamilyHubs.Idam.Core.Queries.GetAccountClaims;
using FluentAssertions;

namespace FamilyHubs.Idam.Core.IntegrationTests.Queries.GetAccountClaims;

public class GetAccountClaimsByEmailCommandTests : DataIntegrationTestBase<GetAccountClaimsByEmailCommandHandler>
{
    [Fact]
    public async Task Handle_ReturnsClaim()
    {
        //Arrange
        await CreateAccountClaim();

        var command = new GetAccountClaimsByEmailCommand
        {
            Email = "Test@test.com"
        };

        var handler = new GetAccountClaimsByEmailCommandHandler(TestDbContext, MockLogger);

        //Act
        var results = await handler.Handle(command, new CancellationToken());

        //Assert
        results.Should().NotBeNull();
        ArgumentNullException.ThrowIfNull(results);

        results[0].Should().BeEquivalentTo(TestSingleAccountClaim);
    }
}
