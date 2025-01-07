using FamilyHubs.Idams.Maintenance.Core.Commands.Add;
using FamilyHubs.Idams.Maintenance.Data.Entities;
using FamilyHubs.Idams.Maintenance.Data.Repository;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FamilyHubs.Idams.Maintenance.UnitTests.Support.MockQueryable;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Core.Commands.Add;

public class AddAccountCommandHandlerTests
{
    [Fact]
    public async Task AccountDoesNotExist_Handle_AddsAccountAndSaves()
    {
        var accountList = TestAccounts.GetListOfAccounts();
        var repository = Substitute.For<IRepository>();
        repository.Accounts.Returns(accountList.BuildMock());
        var sender = Substitute.For<ISender>();
        var logger = Substitute.For<ILogger<AddAccountCommandHandler>>();
        var handler = new AddAccountCommandHandler(repository, sender, logger);
        var command = new AddAccountCommand { Name = "Jim Bean", Email = "jb@temp.org", PhoneNumber = "01234556677" };
        
        await handler.Handle(command, CancellationToken.None);
        
        await repository.Received().AddAsync(Arg.Is<Account>(
            a => a.Name == command.Name && a.Email == command.Email && a.PhoneNumber == command.PhoneNumber));
        await repository.Received().SaveChangesAsync(CancellationToken.None);
    }
}