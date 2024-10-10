using AutoFixture;
using FamilyHubs.Idam.Core.Commands.Delete;
using FamilyHubs.Idam.Core.Exceptions;
using FamilyHubs.Idam.Data.Entities;

namespace FamilyHubs.Idam.Core.IntegrationTests.Commands.Delete
{
    public class DeleteUserSessionCommandHandlerTests : DataIntegrationTestBase<DeleteUserSessionCommandHandler>
    {
        [Fact]
        public async Task Handle_RecordNotFound_Throws()
        {
            //  Arrange
            var randomSid = Fixture.Create<string>();
            var command = new DeleteUserSessionCommand { Sid = randomSid };
            var sut = new DeleteUserSessionCommandHandler(TestDbContext, MockLogger);

            //  Act / Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await sut.Handle(command, new CancellationToken()));

        }

        [Fact]
        public async Task Handle_RecordDeleted_ReturnsTrue()
        {
            //  Arrange
            var record = Fixture.Create<UserSession>();
            TestDbContext.Add(record);
            await TestDbContext.SaveChangesAsync();
            var command = new DeleteUserSessionCommand { Sid = record.Sid};
            var sut = new DeleteUserSessionCommandHandler(TestDbContext, MockLogger);

            //  Act
            var result = await sut.Handle(command, new CancellationToken());

            //  Assert
            Assert.True(result);

        }
    }
}
