using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.Idams.Maintenance.Data.Entities;
using FamilyHubs.Idams.Maintenance.UI.Pages;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.UI;

public class DeleteUserModelTests
{
    private readonly IIdamService _idamService = Substitute.For<IIdamService>();
    private readonly DeleteUserModel _model;

    public DeleteUserModelTests()
    {
        _model = new DeleteUserModel(_idamService);
    }

    [Fact]
    public async Task KnownAccountId_OnGet_SetsUserName()
    {
        var account1 = TestAccounts.GetAccount1();
        _idamService.GetAccountById(account1.Id).Returns(account1);

        await _model.OnGet(account1.Id);

        _model.UserName.Should().Be(account1.Name);
    }
    
    [Fact]
    public async Task UnknownAccountId_OnGet_UserNameShouldBeNull()
    {
        const long accountId = 100;
        _idamService.GetAccountById(accountId).Returns((Account?)null);

        await _model.OnGet(accountId);

        _model.UserName.Should().BeNull();
    }
    
    [Fact]
    public async Task UnableToAccount_OnPost_SetsUserName()
    {
        var account1 = TestAccounts.GetAccount1();
        _idamService.DeleteUser(account1.Id).Returns(false);
        _idamService.GetAccountById(account1.Id).Returns(account1);
        _model.DeleteUser = true;
        
        await _model.OnPost(account1.Id);

        _model.UserName.Should().Be(account1.Name);
    }
    
    [Fact]
    public async Task UnableToAccount_OnPost_SetsErrorMessage()
    {
        var account1 = TestAccounts.GetAccount1();
        _idamService.DeleteUser(account1.Id).Returns(false);
        _idamService.GetAccountById(account1.Id).Returns(account1);
        _model.DeleteUser = true;
        
        await _model.OnPost(account1.Id);

        _model.Error.Should().Be($"Failed to delete {account1.Name}");
    }
    
    [Fact]
    public async Task AbleToAccount_OnPost_RedirectsToUsersPage()
    {
        var account1 = TestAccounts.GetAccount1();
        _idamService.DeleteUser(account1.Id).Returns(true);
        _model.DeleteUser = true;
        
        var result = await _model.OnPost(account1.Id) as RedirectToPageResult;

        result.Should().NotBeNull();
        result!.PageName.Should().Be("Users");
    }
}