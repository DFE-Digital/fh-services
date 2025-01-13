
using Ardalis.GuardClauses;
using FamilyHubs.Idams.Maintenance.Core.Commands.Update;
using FamilyHubs.Idams.Maintenance.Data.Repository;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FamilyHubs.Idams.Maintenance.UnitTests.Support.MockQueryable;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Core.Commands.Update;

public class UpdateRoleAndOrganisationCommandHandlerTests
{
    [Fact]
    public async Task AccountDoesNotExist_Handle_ThrowsNotFoundException()
    {
        var accountList = TestAccounts.GetListOfAccounts();
        var repository = Substitute.For<IRepository>();
        repository.Accounts.Returns(accountList.BuildMock());
        var sender = Substitute.For<ISender>();
        var logger = Substitute.For<ILogger<UpdateRoleAndOrganisationCommandHandler>>();
        var handler = new UpdateRoleAndOrganisationCommandHandler(repository, sender, logger);
        var command = new UpdateRoleAndOrganisationCommand(
            1000, TestOrganisations.Organisation1.Id, SharedKernel.Identity.RoleTypes.LaProfessional);
        
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
    }
    
    [Fact]
    public async Task RoleClaimDoesNotExist_Handle_ThrowsNotFoundException()
    {
        var accountList = TestAccounts.GetListOfAccounts();
        var account = TestAccounts.GetAccount2();
        var organisation = TestOrganisations.Organisation1;
        var repository = Substitute.For<IRepository>();
        repository.Accounts.Returns(accountList.BuildMock());
        var sender = Substitute.For<ISender>();
        var logger = Substitute.For<ILogger<UpdateRoleAndOrganisationCommandHandler>>();
        var handler = new UpdateRoleAndOrganisationCommandHandler(repository, sender, logger);
        var command = new UpdateRoleAndOrganisationCommand(account.Id, organisation.Id, SharedKernel.Identity.RoleTypes.LaProfessional);
        
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
    }
    
    [Fact]
    public async Task OrganisationClaimDoesNotExist_Handle_ThrowsNotFoundException()
    {
        var accountList = TestAccounts.GetListOfAccounts();
        var account = TestAccounts.GetAccount4();
        var organisation = TestOrganisations.Organisation1;
        var repository = Substitute.For<IRepository>();
        repository.Accounts.Returns(accountList.BuildMock());
        var sender = Substitute.For<ISender>();
        var logger = Substitute.For<ILogger<UpdateRoleAndOrganisationCommandHandler>>();
        var handler = new UpdateRoleAndOrganisationCommandHandler(repository, sender, logger);
        var command = new UpdateRoleAndOrganisationCommand(account.Id, organisation.Id, SharedKernel.Identity.RoleTypes.LaProfessional);
        
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
    }
    
    [Fact]
    public async Task ClaimDoesExist_Handle_UpdatesClaimAndSaves()
    {
        var accountList = TestAccounts.GetListOfAccounts();
        var account = accountList[0];
        var organisation = TestOrganisations.Organisation1;
        var repository = Substitute.For<IRepository>();
        repository.Accounts.Returns(accountList.BuildMock());
        var sender = Substitute.For<ISender>();
        var logger = Substitute.For<ILogger<UpdateRoleAndOrganisationCommandHandler>>();
        var handler = new UpdateRoleAndOrganisationCommandHandler(repository, sender, logger);
        var command = new UpdateRoleAndOrganisationCommand(account.Id, organisation.Id, SharedKernel.Identity.RoleTypes.LaProfessional);
        
        await handler.Handle(command, CancellationToken.None);
        
        account.Claims.FirstOrDefault(c => c is { Name: "role", Value: SharedKernel.Identity.RoleTypes.LaProfessional }).Should().NotBeNull();
        await repository.Received().SaveChangesAsync(CancellationToken.None);
    }
}