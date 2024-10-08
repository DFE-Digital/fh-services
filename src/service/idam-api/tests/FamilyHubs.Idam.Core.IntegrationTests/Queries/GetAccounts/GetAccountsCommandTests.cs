using FamilyHubs.Idam.Core.Queries.GetAccounts;
using FamilyHubs.Idam.Core.Services;
using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace FamilyHubs.Idam.Core.IntegrationTests.Queries.GetAccounts;

public class GetAccountsCommandTests : DataIntegrationTestBase<GetAccountsCommandHandler>
{
    [Theory]
    [InlineData(RoleTypes.DfeAdmin, -1, true)]
    [InlineData(RoleTypes.LaManager, 1, false)]
    public async Task Handle_BasedOnBearerRole_CorrectServiceDirectoryEndpointCalled(string role, long organisationId, bool getMultipleOrganisations)
    {
        //Arrange
        await CreateAccountClaim();

        var mockServiceDirectoryService = new Mock<IServiceDirectoryService>();
        mockServiceDirectoryService.Setup(x => x.GetOrganisationsByAssociatedId(It.IsAny<long>())).Returns(TestDataProvider.GetOrganisations());
        mockServiceDirectoryService.Setup(x => x.GetOrganisationsByIds(It.IsAny<IEnumerable<long>>())).Returns(TestDataProvider.GetOrganisations());

        var mockHttpContextAccessor = GetMockHttpContextAccessor(organisationId, role);
        var command = new GetAccountsCommand(null, null, null, null, null, null, null, null, null);

        var handler = new GetAccountsCommandHandler(
            TestDbContext,
            mockServiceDirectoryService.Object,
            mockHttpContextAccessor.Object);

        //Act
        await handler.Handle(command, new CancellationToken());

        //Assert
        mockServiceDirectoryService.Verify(x => x.GetAllOrganisations(), Times.Never);
        if (getMultipleOrganisations)
        {
            mockServiceDirectoryService.Verify(x => x.GetOrganisationsByIds(It.IsAny<IEnumerable<long>>()), Times.Once);
            mockServiceDirectoryService.Verify(x => x.GetOrganisationsByAssociatedId(It.IsAny<long>()), Times.Never);
        }
        else
        {
            mockServiceDirectoryService.Verify(x => x.GetOrganisationsByIds(It.IsAny<IEnumerable<long>>()), Times.Never);
            mockServiceDirectoryService.Verify(x => x.GetOrganisationsByAssociatedId(It.IsAny<long>()), Times.Once);
        }

    }

    [Theory]
    [InlineData(2, null, null, null)]
    [InlineData(1, "filter", null, null)]
    [InlineData(0, "GetNotMatches", null, null)]
    [InlineData(1, null, "filter", null)]
    [InlineData(0, null, "GetNotMatches", null)]
    [InlineData(1, null, null, "filter")]
    [InlineData(0, null, null, "GetNotMatches")]
    public async Task Handle_ReturnsFilteredResults(
        int expectedTotal, string? userName, string? email, string? organisationName)
    {
        //Arrange
        await CreateAccountClaim();
        await AddTestAccount();

        var mockServiceDirectoryService = new Mock<IServiceDirectoryService>();

        mockServiceDirectoryService.Setup(x => x.GetAllOrganisations()).Returns(TestDataProvider.GetOrganisations());
        mockServiceDirectoryService.Setup(x => x.GetOrganisationsByAssociatedId(It.IsAny<long>())).Returns(TestDataProvider.GetOrganisations());
        mockServiceDirectoryService.Setup(x => x.GetOrganisationsByIds(It.IsAny<IEnumerable<long>>())).Returns(TestDataProvider.GetOrganisations());
        mockServiceDirectoryService.Setup(x => x.GetOrganisationsByName(It.IsAny<string>())).Returns<string>(async name =>
            (await TestDataProvider.GetOrganisations())!.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList());

        var mockHttpContextAccessor = GetMockHttpContextAccessor(-1, RoleTypes.DfeAdmin);

        var command = new GetAccountsCommand(null, null, null, userName, email, organisationName, null, null, null);

        var handler = new GetAccountsCommandHandler(
            TestDbContext,
            mockServiceDirectoryService.Object,
            mockHttpContextAccessor.Object);

        //Act
        var results = await handler.Handle(command, new CancellationToken());

        //Assert
        if (expectedTotal != 0)
        {
            Assert.Equal(expectedTotal, results!.Items.Count);
        }
        else
        {
            Assert.Null(results);
        }

    }


    private Mock<IHttpContextAccessor> GetMockHttpContextAccessor(long organisationId, string userRole)
    {
        var mockUser = new Mock<ClaimsPrincipal>();
        var claims = new List<Claim>();
        claims.Add(new Claim(FamilyHubsClaimTypes.OrganisationId, organisationId.ToString()));
        claims.Add(new Claim(FamilyHubsClaimTypes.Role, userRole));

        mockUser.SetupGet(x => x.Claims).Returns(claims);


        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.SetupGet(x => x.User).Returns(mockUser.Object);

        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        mockHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(mockHttpContext.Object);

        return mockHttpContextAccessor;
    }

    private async Task AddTestAccount()
    {

        var claim = new AccountClaim { AccountId = 2, Name = FamilyHubsClaimTypes.OrganisationId, Value = "2" };
        var claims = new List<AccountClaim> { claim };
        var account = new Account { Id = 2, Email = "testEmailFilter", Name = "TestNameFilter", Claims = claims, Status = Data.Entities.AccountStatus.Active };

        TestDbContext.Accounts.Add(account);
        await TestDbContext.SaveChangesAsync();
    }
}
