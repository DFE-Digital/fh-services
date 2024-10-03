using AutoFixture;
using FamilyHubs.Idam.Core.Commands.Add;
using FamilyHubs.Idam.Core.Exceptions;
using FamilyHubs.Idam.Data.Entities;

namespace FamilyHubs.Idam.Core.IntegrationTests.Commands.Add
{
    public class AddUserSessionCommandHandlerTests : DataIntegrationTestBase<AddUserSessionCommandHandler>
    {
        [Fact]
        public async Task Handle_RecordAlreadyExists_Throws()
        {
            //  Arrange
            var existingUserSession = Fixture.Create<UserSession>();
            TestDbContext.Add(existingUserSession);
            await TestDbContext.SaveChangesAsync();
            var command = new AddUserSessionCommand { Sid = existingUserSession.Sid, Email = existingUserSession.Email };
            var sut = new AddUserSessionCommandHandler(TestDbContext, MockLogger);

            //  Act / Assert
            await Assert.ThrowsAsync<AlreadyExistsException>(async() => await sut.Handle(command, new CancellationToken()));

        }

        [Fact]
        public async Task Handle_RecordAdded_ReturnsSid()
        {
            //  Arrange
            var newUserSession = Fixture.Create<UserSession>();
            var command = new AddUserSessionCommand { Sid = newUserSession.Sid, Email = newUserSession.Email };
            var sut = new AddUserSessionCommandHandler(TestDbContext, MockLogger);

            //  Act
            var result = await sut.Handle(command, new CancellationToken());

            //  Assert
            Assert.Equal(newUserSession.Sid, result);

        }
    }
}
