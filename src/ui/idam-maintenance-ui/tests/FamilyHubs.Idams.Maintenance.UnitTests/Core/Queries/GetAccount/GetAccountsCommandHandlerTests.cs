using FamilyHubs.Idams.Maintenance.Core.Queries.GetAccount;
using FamilyHubs.Idams.Maintenance.Data.Repository;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FamilyHubs.Idams.Maintenance.UnitTests.Support.MockQueryable;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Core.Queries.GetAccount;

public class GetAccountsCommandHandlerTests
{
    [Fact]
    public async Task SearchAccountsByName_Handle_ReturnsAccounts()
    {
        var accountList = TestAccounts.GetListOfAccounts();
        var account3 = TestAccounts.GetAccount3();
        var organisation1 = TestOrganisations.Organisation2;
        var repository = Substitute.For<IRepository>();
        repository.Accounts.Returns(accountList.BuildMock());
        var logger = Substitute.For<ILogger<GetAccountsCommandHandler>>();
        var handler = new GetAccountsCommandHandler(repository, logger);
        var command = new GetAccountsCommand(account3.Name, null, organisation1.Id, true, true, null);
        
        var result = await handler.Handle(command, CancellationToken.None);
        
        result.Items.Should().ContainSingle();
        result.Items[0].Should().BeEquivalentTo(account3);
    }
    
    [Fact]
    public async Task SearchAccountsByEmail_Handle_ReturnsAccounts()
    {
        var accountList = TestAccounts.GetListOfAccounts();
        var account3 = TestAccounts.GetAccount3();
        var organisation1 = TestOrganisations.Organisation2;
        var repository = Substitute.For<IRepository>();
        repository.Accounts.Returns(accountList.BuildMock());
        var logger = Substitute.For<ILogger<GetAccountsCommandHandler>>();
        var handler = new GetAccountsCommandHandler(repository, logger);
        var command = new GetAccountsCommand(null, account3.Email, organisation1.Id, true, true, null);
        
        var result = await handler.Handle(command, CancellationToken.None);
        result.Items.Should().ContainSingle();
        result.Items[0].Should().BeEquivalentTo(account3);
    }
}