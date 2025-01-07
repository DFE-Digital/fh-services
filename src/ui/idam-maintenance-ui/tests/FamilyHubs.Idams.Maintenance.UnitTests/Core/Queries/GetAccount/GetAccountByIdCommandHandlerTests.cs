using FamilyHubs.Idams.Maintenance.Core.Queries.GetAccount;
using FamilyHubs.Idams.Maintenance.Data.Entities;
using FamilyHubs.Idams.Maintenance.Data.Repository;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FamilyHubs.Idams.Maintenance.UnitTests.Support.MockQueryable;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Core.Queries.GetAccount;

public class GetAccountByIdCommandHandlerTests
{
    [Fact]
    public async Task KnownAccountId_Handle_ReturnsAccount()
    {
        var accountList = TestAccounts.GetListOfAccounts();
        var account = TestAccounts.GetAccount2();
        var repository = Substitute.For<IRepository>();
        repository.Accounts.Returns(accountList.BuildMock());
        var logger = Substitute.For<ILogger<GetAccountByIdCommandHandler>>();
        var handler = new GetAccountByIdCommandHandler(repository, logger);
        var command = new GetAccountByIdCommand { Id = account.Id };
        
        var result = await handler.Handle(command, CancellationToken.None);
        
        result.Should().BeEquivalentTo(account);
    }
    
    [Fact]
    public async Task UnknownAccountId_Handle_ReturnsNull()
    {
        var accountList = new List<Account>([TestAccounts.GetAccount1()]);
        var account = TestAccounts.GetAccount2();
        var repository = Substitute.For<IRepository>();
        repository.Accounts.Returns(accountList.BuildMock());
        var logger = Substitute.For<ILogger<GetAccountByIdCommandHandler>>();
        var handler = new GetAccountByIdCommandHandler(repository, logger);
        var command = new GetAccountByIdCommand { Id = account.Id };
        
        var result = await handler.Handle(command, CancellationToken.None);
        
        result.Should().BeNull();
    }
}