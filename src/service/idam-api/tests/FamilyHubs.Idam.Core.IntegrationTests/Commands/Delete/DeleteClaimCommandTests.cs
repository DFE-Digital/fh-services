using Ardalis.GuardClauses;
using FamilyHubs.Idam.Core.Commands.Delete;
using FamilyHubs.SharedKernel.Identity;
using FluentAssertions;

namespace FamilyHubs.Idam.Core.IntegrationTests.Commands.Delete
{
    public class DeleteClaimCommandHandlerTests : DataIntegrationTestBase<DeleteClaimCommandHandler>
    {
        [Fact]
        public async Task Handle_ClaimDeleted()
        {
            //Arrange
            await CreateAccountClaim(TestSingleAccountClaim);

            var command = new DeleteClaimCommand
            {
                AccountId = 1,
                Name = FamilyHubsClaimTypes.OrganisationId
            };

            var handler = new DeleteClaimCommandHandler(TestDbContext, MockLogger);

            //Act
            var results = await handler.Handle(command, new CancellationToken());

            //Assert
            results.Should().Be(true);

        }

        [Fact]
        public async Task Handle_RecordNotFound_ThrowsException()
        {
            //Arrange
            var command = new DeleteClaimCommand
            {
                AccountId = 2,
                Name = "SingleClaimName"
            };

            var handler = new DeleteClaimCommandHandler(TestDbContext, MockLogger);

            // Act 
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, new CancellationToken()));
        }
    }
}
