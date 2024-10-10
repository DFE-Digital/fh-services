using AutoMapper;
using FamilyHubs.Idam.Core.Exceptions;
using FamilyHubs.Idam.Core.Models;
using FamilyHubs.Idam.Core.Queries.GetUserSession;
using FamilyHubs.Idam.Data.Entities;
using NSubstitute;

namespace FamilyHubs.Idam.Core.IntegrationTests.Queries.GetUserSession
{
    public class GetUserSessionCommandTests : DataIntegrationTestBase<GetUserSessionCommandHandler>
    {
        private readonly IMapper _mockMapper;
        private const string ExpectedSid = "expectedSid";
        private const string ExpectedEmail = "expected@email.com";

        public GetUserSessionCommandTests()
        {
            _mockMapper = Substitute.For<IMapper>();
            _mockMapper.Map<UserSessionDto>(Arg.Any<UserSession>()).Returns(source =>
                new UserSessionDto
                    {
                        Email = source.Arg<UserSession>().Email,
                        Sid = source.Arg<UserSession>().Sid
                    });
        }

        [Fact]
        public async Task Handle_RecordNotFound_Throws()
        {
            //  Arrange
            var command = new GetUserSessionCommand { Sid = null, Email = null };
            var sut = new GetUserSessionCommandHandler(TestDbContext, MockLogger, _mockMapper);

            //  Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await sut.Handle(command, new CancellationToken()));

            //  Assert
            Assert.Equal("Get UserSession must query by 'email' or 'sid'", exception.Message);

        }

        [Theory]
        [InlineData(ExpectedSid, null)]
        [InlineData(null, ExpectedEmail)]
        public async Task Handle_ReturnsRecord(string? sid, string? email)
        {
            //  Arrange
            var userSession = new UserSession { Sid = ExpectedSid, Email = ExpectedEmail};
            TestDbContext.Add(userSession);
            await TestDbContext.SaveChangesAsync();

            var command = new GetUserSessionCommand { Sid = sid, Email = email };
            var sut = new GetUserSessionCommandHandler(TestDbContext, MockLogger, _mockMapper);

            //  Act
            var result = await sut.Handle(command, new CancellationToken());

            //  Assert
            Assert.NotNull(result);
            Assert.Equal(userSession.Sid, result.Sid);
            Assert.Equal(userSession.Email, result.Email);

        }

    }
}
