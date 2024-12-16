using System.Runtime.InteropServices;
using FamilyHubs.Idams.Maintenance.Core.Commands.Add;
using FamilyHubs.Idams.Maintenance.Core.Commands.Delete;
using FamilyHubs.Idams.Maintenance.Core.Commands.Update;
using FamilyHubs.Idams.Maintenance.Core.Queries.GetAccount;
using FamilyHubs.Idams.Maintenance.Core.Queries.GetAccountClaims;
using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.Idams.Maintenance.Data.Entities;
using FamilyHubs.ReferralService.Shared.Models;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Core.Services;

public class IdamServiceTests
{
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly ILogger<IdamService> _logger = Substitute.For<ILogger<IdamService>>();
    private readonly IdamService _idamService;

    public IdamServiceTests()
    {
        _idamService = new IdamService(_mediator, _logger);
    }
    
    [Theory]
    [InlineData("Fred Bloggs", "fb@temp.org", null)]
    [InlineData("Jane Doe", "jd@temp.org", "01234556677")]
    public async Task WithDfEUserDetails_AddNewDfeAccount_ReturnsEmail(string name, string email, string? phoneNumber)
    {
        _mediator.Send(Arg.Is<AddAccountCommand>(
                p => p.Name == name && 
                     p.Email == email && 
                     p.PhoneNumber == phoneNumber &&
                     p.Claims.Any(c => c.Name == "role" && c.Value == "DfeAdmin") &&
                     p.Claims.Any(c => c.Name == "OrganisationId" && c.Value == "-1")))
            .Returns(email);
        
        var result = await _idamService.AddNewDfeAccount(name, email, phoneNumber);
        
        result.Should().Be(email);
    }

    [Fact]
    public async Task KnownEmail_GetAccountIdByEmail_ReturnsAccountId()
    {
        var account = new Account { Id = 100, Email = "jd@temp.org", Name = "Jane Doe", Status = AccountStatus.Active };
        _mediator.Send(Arg.Is<GetAccountByEmailCommand>(p => p.Email == account.Email)).Returns(account);
        
        var result = await _idamService.GetAccountIdByEmail(account.Email);
        
        result.Should().Be(account.Id);
    }
    
    [Fact]
    public async Task UnknownEmail_GetAccountIdByEmail_ReturnsMinusOneAccountId()
    {
        const string email = "jd@temp.org";
        _mediator.Send(Arg.Is<GetAccountByEmailCommand>(p => p.Email == email)).Returns((Account?)null);
        
        var result = await _idamService.GetAccountIdByEmail(email);
        
        result.Should().Be(-1);
    }
    
    [Fact]
    public async Task KnownId_GetAccountById_ReturnsAccount()
    {
        var account = new Account { Id = 100, Email = "jd@temp.org", Name = "Jane Doe", Status = AccountStatus.Active };
        _mediator.Send(Arg.Is<GetAccountByIdCommand>(p => p.Id == account.Id)).Returns(account);
        
        var result = await _idamService.GetAccountById(account.Id);
        
        result.Should().Be(account);
    }
    
    [Fact]
    public async Task KnownAccountId_DeleteUser_ReturnsTrue()
    {
        var account = new Account { Id = 100, Email = "jd@temp.org", Name = "Jane Doe", Status = AccountStatus.Active };
        _mediator.Send(Arg.Is<DeleteAccountCommand>(p => p.AccountId == account.Id)).Returns(true);
        
        var result = await _idamService.DeleteUser(account.Id);
        
        result.Should().BeTrue();
    }
    
    [Fact]
    public async Task UnknownAccountId_DeleteUser_ReturnsTrue()
    {
        var account = new Account { Id = 200, Email = "jd@temp.org", Name = "Jane Doe", Status = AccountStatus.Active };
        _mediator.Send(Arg.Is<DeleteAccountCommand>(p => p.AccountId == account.Id)).Returns(false);
        
        var result = await _idamService.DeleteUser(account.Id);
        
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("janedoe1", null, 1, 10)]
    [InlineData(null, "jd@temp.org", 2, 5)]
    public async Task WithUsernameOrEmailAndPaging_GetAccounts_ReturnsAccounts(
        string? username, 
        string? email, 
        int pageNumber, 
        int pageSize)
    {
        const long organisationId = 10;
        var account = new Account
        {
            Id = 100, Email = email ?? "jd@temp.org", Name = "Jane Doe", Status = AccountStatus.Active
        };
        var accounts = new List<Account> { account };
        _mediator.Send(Arg.Is<GetAccountsCommand>(
                p => p.UserName == username &&
                     p.Email == email &&
                     p.OrganisationId == organisationId &&
                     !p.IsLaUser &&
                     !p.IsVcsUser &&
                     p.PageNumber == pageNumber &&
                     p.PageSize == pageSize))
            .Returns(new PaginatedList<Account>(accounts, accounts.Count, pageNumber, pageSize));

        var result = await _idamService.GetAccounts(
            username, email, organisationId, false, false, null, pageNumber, pageSize);
        
        result.Items.Should().BeEquivalentTo(accounts);
    }

    [Fact]
    public async Task KnownAccount_UpdateRoleAndOrganisation_ReturnsTrue()
    {
        const long accountId = 100;
        const long organisationId = 10;
        const string role = "DfeAdmin";
        _mediator.Send(Arg.Is<UpdateRoleAndOrganisationCommand>(
            p => p.AccountId == accountId && p.OrganisationId == organisationId && p.Role == role))
            .Returns(true);
        
        var result = await _idamService.UpdateRoleAndOrganisation(accountId, organisationId, role);

        result.Should().BeTrue();
    }
    
    [Fact]
    public async Task UnknownAccount_UpdateRoleAndOrganisation_ReturnsFalse()
    {
        const long accountId = 200;
        const long organisationId = 10;
        const string role = "DfeAdmin";
        _mediator.Send(Arg.Is<UpdateRoleAndOrganisationCommand>(
                p => p.AccountId == accountId && p.OrganisationId == organisationId && p.Role == role))
            .Returns(false);
        
        var result = await _idamService.UpdateRoleAndOrganisation(accountId, organisationId, role);

        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task KnownId_GetAccountClaimsById_ReturnsAccount()
    {
        var accountClaim = new AccountClaim { AccountId = 10, Name = "role", Value = "DfeAdmin" };
        var accountClaims = new List<AccountClaim> { accountClaim };
        _mediator.Send(Arg.Is<GetAccountClaimsByIdCommand>(p => p.AccountId == accountClaim.Id)).Returns(accountClaims);
        
        var result = await _idamService.GetAccountClaimsById(accountClaim.Id);
        
        result.Should().BeEquivalentTo(accountClaims);
    }
}