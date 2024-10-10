using Ardalis.GuardClauses;
using FamilyHubs.Idam.Core.Commands.Update;
using FluentAssertions;

namespace FamilyHubs.Idam.Core.IntegrationTests.Commands.Update;

public class UpdateClaimCommandTests : DataIntegrationTestBase<UpdateClaimCommandHandler>
{
    [Fact]
    public async Task Handle_ThenUpdateClaimOnly()
    {
        //Arrange
        var claim = await CreateAccountClaim();
        claim.Name = "Unit Test Update Claim Name";
        claim.Value = "Unit Test Update Claim value";

        var updateCommand = new UpdateClaimCommand
        {
            AccountId = claim.AccountId,
            Name = claim.Name,
            Value = claim.Value
        };
        var updateHandler = new UpdateClaimCommandHandler(TestDbContext, MockLogger);

        //Act
        var result = await updateHandler.Handle(updateCommand, new CancellationToken());

        //Assert
        result.Should().BePositive();
        result.Should().Be(claim.AccountId);
        var actualClaim = TestDbContext.AccountClaims.SingleOrDefault(s => s.Name == claim.Name);
        actualClaim.Should().NotBeNull();
        actualClaim!.Value.Should().Be(claim.Value);
    }

    [Fact]
    public async Task Handle_RecordNotFound_Throws()
    {
        //Arrange
        var command = new UpdateClaimCommand
        {
            AccountId = 2,
            Name = "SingleClaimName",
            Value = "test"
        };

        var handler = new UpdateClaimCommandHandler(TestDbContext, MockLogger);

        // Act 
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, new CancellationToken()));
    }
}
