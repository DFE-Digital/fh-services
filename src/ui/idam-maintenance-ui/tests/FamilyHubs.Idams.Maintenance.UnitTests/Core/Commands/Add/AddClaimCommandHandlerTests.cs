using FamilyHubs.Idams.Maintenance.Core.Commands.Add;
using FamilyHubs.Idams.Maintenance.Core.Exceptions;
using FamilyHubs.Idams.Maintenance.Data.Entities;
using FamilyHubs.Idams.Maintenance.Data.Repository;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FamilyHubs.Idams.Maintenance.UnitTests.Support.MockQueryable;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Core.Commands.Add;

public class AddClaimCommandHandlerTests
{
    [Fact]
    public async Task AccountClaimExists_Handle_ThrowsAlreadyExistsException()
    {
        var account = TestAccounts.GetAccount1();
        var accountClaimsList = account.Claims;
        var accountClaims = accountClaimsList.First();
        var repository = Substitute.For<IRepository>();
        repository.AccountClaims.Returns(accountClaimsList.BuildMock());
        var logger = Substitute.For<ILogger<AddClaimCommandHandler>>();
        var handler = new AddClaimCommandHandler(repository, logger);
        var command = new AddClaimCommand { AccountId = account.Id, Name = accountClaims.Name, Value = accountClaims.Value };

        await Assert.ThrowsAsync<AlreadyExistsException>(async () => await handler.Handle(command, CancellationToken.None)); 
    }
    
    [Fact]
    public async Task AccountClaimDoesNotExist_Handle_AddsAccountAndSaves()
    {
        var account = TestAccounts.GetAccount1();
        var accountClaimsList = account.Claims;
        var repository = Substitute.For<IRepository>();
        repository.AccountClaims.Returns(accountClaimsList.BuildMock());
        var logger = Substitute.For<ILogger<AddClaimCommandHandler>>();
        var handler = new AddClaimCommandHandler(repository, logger);
        var command = new AddClaimCommand { AccountId = account.Id, Name = "TestClaim", Value = "TestClaimValue" };
        
        await handler.Handle(command, CancellationToken.None);
        
        await repository.Received().AddAsync(Arg.Is<AccountClaim>(
            ac => ac.AccountId == command.AccountId && ac.Name == command.Name && ac.Value == command.Value));
        await repository.Received().SaveChangesAsync(CancellationToken.None);
    }
}