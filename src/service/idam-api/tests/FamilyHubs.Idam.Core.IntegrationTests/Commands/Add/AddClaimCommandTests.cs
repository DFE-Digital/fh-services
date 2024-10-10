using AutoFixture;
using FamilyHubs.Idam.Core.Commands.Add;
using FamilyHubs.Idam.Core.Exceptions;
using FluentAssertions;

namespace FamilyHubs.Idam.Core.IntegrationTests.Commands.Add
{
    public class AddClaimCommandTests : DataIntegrationTestBase<AddClaimCommandHandler>
    {
        [Fact]
        public async Task Handle_RecordAdded_ReturnsAccountId()
        {
            //Arrange
            var command = Fixture.Build<AddClaimCommand>().With(x => x.AccountId, TestSingleAccountClaim.AccountId).Create();
            var handler = new AddClaimCommandHandler(TestDbContext, MockLogger);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            result.Should().Be(command.AccountId);
            var actualClaim = TestDbContext.AccountClaims.SingleOrDefault(o => o.AccountId == result);
            Assert.NotNull(actualClaim);
            Assert.Equal(command.Name, actualClaim.Name);
            Assert.Equal(command.Value, actualClaim.Value);

        }

        [Fact]
        public async Task Handle_RecordAlreadyExists_ThrowsException()
        {
            //Arrange
            await CreateAccountClaim(TestSingleAccountClaim);

            var command = new AddClaimCommand
            {
                AccountId = TestSingleAccountClaim.AccountId,
                Name = TestSingleAccountClaim.Name,
                Value = TestSingleAccountClaim.Value
            };

            var handler = new AddClaimCommandHandler(TestDbContext, GetLogger<AddClaimCommandHandler>());

            // Act 
            // Assert
            await Assert.ThrowsAsync<AlreadyExistsException>(() => handler.Handle(command, new CancellationToken()));
        }
    }
}
