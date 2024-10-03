using FamilyHubs.Idam.Core.Queries.GetAccounts;
using FamilyHubs.Idam.Core.Services;
using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using NSubstitute;

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

        var mockServiceDirectoryService = Substitute.For<IServiceDirectoryService>();
        mockServiceDirectoryService.GetOrganisationsByAssociatedId(Arg.Any<long>()).Returns(TestDataProvider.GetOrganisations());
        mockServiceDirectoryService.GetOrganisationsByIds(Arg.Any<IEnumerable<long>>()).Returns(TestDataProvider.GetOrganisations());

        var mockHttpContextAccessor = GetMockHttpContextAccessor(organisationId, role);
        var command = new GetAccountsCommand(null, null, null, null, null, null, null, null, null);

        var handler = new GetAccountsCommandHandler(
            TestDbContext,
            mockServiceDirectoryService,
            mockHttpContextAccessor);

        //Act
        await handler.Handle(command, new CancellationToken());

        //Assert
        await mockServiceDirectoryService.Received(0).GetAllOrganisations();
        if (getMultipleOrganisations)
        {
            await mockServiceDirectoryService.Received(1).GetOrganisationsByIds(Arg.Any<IEnumerable<long>>());
            await mockServiceDirectoryService.Received(0).GetOrganisationsByAssociatedId(Arg.Any<long>());
        }
        else
        {
            await mockServiceDirectoryService.Received(0).GetOrganisationsByIds(Arg.Any<IEnumerable<long>>());
            await mockServiceDirectoryService.Received(1).GetOrganisationsByAssociatedId(Arg.Any<long>());
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

        var mockServiceDirectoryService = Substitute.For<IServiceDirectoryService>();

        mockServiceDirectoryService.GetAllOrganisations().Returns(TestDataProvider.GetOrganisations());
        mockServiceDirectoryService.GetOrganisationsByAssociatedId(Arg.Any<long>()).Returns(TestDataProvider.GetOrganisations());
        mockServiceDirectoryService.GetOrganisationsByIds(Arg.Any<IEnumerable<long>>()).Returns(TestDataProvider.GetOrganisations());
        mockServiceDirectoryService.GetOrganisationsByName(Arg.Any<string>())!.Returns(async name =>
                (await TestDataProvider.GetOrganisations())!.Where(oDto => oDto.Name.Contains(name.Arg<string>(), StringComparison.CurrentCultureIgnoreCase)).ToList());

        var mockHttpContextAccessor = GetMockHttpContextAccessor(-1, RoleTypes.DfeAdmin);

        var command = new GetAccountsCommand(null, null, null, userName, email, organisationName, null, null, null);

        var handler = new GetAccountsCommandHandler(
            TestDbContext,
            mockServiceDirectoryService,
            mockHttpContextAccessor);

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


    private static IHttpContextAccessor GetMockHttpContextAccessor(long organisationId, string userRole)
    {
        var mockUser = Substitute.For<ClaimsPrincipal>();
        var claims = new List<Claim>
        {
            new(FamilyHubsClaimTypes.OrganisationId, organisationId.ToString()),
            new(FamilyHubsClaimTypes.Role, userRole)
        };

        mockUser.Claims.Returns(claims);

        var mockHttpContext = Substitute.For<HttpContext>();
        mockHttpContext.User.Returns(mockUser);

        var mockHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        mockHttpContextAccessor.HttpContext.Returns(mockHttpContext);

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
