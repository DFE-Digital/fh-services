using AutoFixture;
using FamilyHubs.Idam.Core.Commands.Delete;
using FamilyHubs.Idam.Data.Entities;

namespace FamilyHubs.Idam.Core.IntegrationTests.Commands.Delete
{
    public class DeleteAllUserSessionsCommandHandlerTests : DataIntegrationTestBase<DeleteAllUserSessionsCommandHandler>
    {       
        [Fact]
        public async Task Handle_RecordsDeleted_DoesNotThrow()
        {
            //  Arrange
            var email = Guid.NewGuid().ToString();
            var session1 = Fixture.Create<UserSession>();
            var session2 = Fixture.Create<UserSession>();
            session1.Email = email;
            session2.Email = email;
            TestDbContext.Add(session1);
            TestDbContext.Add(session2);

            await TestDbContext.SaveChangesAsync();
            var command = new DeleteAllUserSessionsCommand { Email = email};
            var sut = new DeleteAllUserSessionsCommandHandler(TestDbContext, MockLogger);

            //  Act
            var exception = await Record.ExceptionAsync(() => sut.Handle(command, new CancellationToken()));

            //  Assert
            Assert.Null(exception);
        }
    }
}
