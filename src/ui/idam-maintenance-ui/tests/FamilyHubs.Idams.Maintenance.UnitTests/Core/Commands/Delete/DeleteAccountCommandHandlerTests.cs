using Ardalis.GuardClauses;
using FamilyHubs.Idams.Maintenance.Core.Commands.Delete;
using FamilyHubs.Idams.Maintenance.Data.Entities;
using FamilyHubs.Idams.Maintenance.Data.Repository;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FamilyHubs.Idams.Maintenance.UnitTests.Support.MockQueryable;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Core.Commands.Delete;

public class DeleteAccountCommandHandlerTests
{
    [Fact]
    public async Task AccountClaimExists_Handle_ThrowsAlreadyExistsException()
    {
        var accountList = TestAccounts.GetListOfAccounts();
        var repository = Substitute.For<IRepository>();
        repository.Accounts.Returns(accountList.BuildMock());
        var logger = Substitute.For<ILogger<DeleteAccountCommandHandler>>();
        var handler = new DeleteAccountCommandHandler(repository, logger);
        var command = new DeleteAccountCommand { AccountId = 1000 };

        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None)); 
    }
    
    [Fact]
    public async Task AccountClaimDoesNotExist_Handle_AddsAccountAndSaves()
    {
        var accountList = TestAccounts.GetListOfAccounts();
        var repository = Substitute.For<IRepository>();
        repository.Accounts.Returns(accountList.BuildMock());
        var logger = Substitute.For<ILogger<DeleteAccountCommandHandler>>();
        var handler = new DeleteAccountCommandHandler(repository, logger);
        var command = new DeleteAccountCommand { AccountId = accountList[0].Id };
        
        await handler.Handle(command, CancellationToken.None);
        
        repository.Received().Remove(Arg.Is<Account>(a => a.Id == command.AccountId));
        await repository.Received().SaveChangesAsync(CancellationToken.None);
    }
}