using AutoFixture;
using FamilyHubs.Idam.Core.Commands.Update;
using FamilyHubs.Idam.Core.Exceptions;
using FamilyHubs.Idam.Data.Entities;

namespace FamilyHubs.Idam.Core.IntegrationTests.Commands.Update
{
    public class UpdateUserSessionCommandTests : DataIntegrationTestBase<UpdateUserSessionCommandHandler>
    {
        [Fact]
        public async Task Handle_RecordNotFound_Throws()
        {
            //  Arrange
            var randomSid = Fixture.Create<string>();
            var command = new UpdateUserSessionCommand { Sid = randomSid };
            var sut = new UpdateUserSessionCommandHandler(TestDbContext, MockLogger);

            //  Act / Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await sut.Handle(command, new CancellationToken()));

        }

        [Fact]
        public async Task Handle_RecordUpdated_ReturnsSid()
        {
            //  Arrange
            var userSession = Fixture.Create<UserSession>();
            TestDbContext.Add(userSession);
            await TestDbContext.SaveChangesAsync();
            var command = new UpdateUserSessionCommand { Sid = userSession.Sid };
            var sut = new UpdateUserSessionCommandHandler(TestDbContext, MockLogger);

            //  Act
            var result = await sut.Handle(command, new CancellationToken());

            //  Assert
            Assert.Equal(userSession.Sid, result);

        }
    }
}
