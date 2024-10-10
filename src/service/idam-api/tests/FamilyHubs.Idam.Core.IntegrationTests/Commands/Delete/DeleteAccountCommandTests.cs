using Ardalis.GuardClauses;
using FamilyHubs.Idam.Core.Commands.Delete;
using FluentAssertions;

namespace FamilyHubs.Idam.Core.IntegrationTests.Commands.Delete
{
    public class DeleteAccountCommandTests : DataIntegrationTestBase<DeleteAccountCommandHandler>
    {
        [Fact]
        public async Task Handle_AccountDeleted()
        {
            //Arrange
            var command = new DeleteAccountCommand { AccountId = 1 };
            var handler = new DeleteAccountCommandHandler(TestDbContext, MockLogger);

            //Act
            var results = await handler.Handle(command, new CancellationToken());

            //Assert
            results.Should().Be(true);

        }

        [Fact]
        public async Task Handle_RecordNotFound_ThrowsException()
        {
            //Arrange
            var command = new DeleteAccountCommand { AccountId = 2 };
            var handler = new DeleteAccountCommandHandler(TestDbContext, MockLogger);

            // Act 
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, new CancellationToken()));
        }
    }
}
