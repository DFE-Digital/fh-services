using AutoFixture;
using FamilyHubs.Idam.Core.Commands.Delete;
using FamilyHubs.Idam.Data.Entities;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace FamilyHubs.Idam.Core.IntegrationTests.Commands.Delete
{
    public class DeleteExpiredUserSessionsCommandTests : DataIntegrationTestBase<DeleteExpiredUserSessionsCommandHandler>
    {
        [Fact]
        public async Task Handle_RecordDeleted_ReturnsTrue()
        {
            //  Arrange
            var record = Fixture.Create<UserSession>();
            record.LastActive = DateTime.UtcNow.AddDays(-1);
            TestDbContext.Add(record);
            await TestDbContext.SaveChangesAsync();
            var command = new DeleteExpiredUserSessionsCommand();

            var inMemorySettings = new Dictionary<string, string?> { { "ExpiredSessionCleanupInterval", "3600" } };
            IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();

            var sut = new DeleteExpiredUserSessionsCommandHandler(TestDbContext, MockLogger, configuration);

            //  Act
            var result = await sut.Handle(command, new CancellationToken());

            //  Assert
            Assert.True(result);
        }
    }
}
