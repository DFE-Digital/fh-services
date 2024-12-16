using FamilyHubs.Idams.Maintenance.UI.Pages;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.UI;

public class WelcomeModelTests
{
    private readonly WelcomeModel _model = new();
    
    [Fact]
    public void OnGetAddDfEAdminAccount_ReturnsAddDfEAdminPageRedirect()
    {
        var result = _model.OnGetAddDfEAdminAccount() as RedirectToPageResult;

        result.Should().NotBeNull();
        result!.PageName.Should().Be("AddDfEAdminAccount");
    }
    
    [Fact]
    public void OnGetChangeUserPermissions_ReturnsUsersPageRedirect()
    {
        var result = _model.OnGetChangeUserPermissions() as RedirectToPageResult;

        result.Should().NotBeNull();
        result!.PageName.Should().Be("Users");
    }
    
    [Fact]
    public void OnGetCreateEncryptionKeys_ReturnsEncryptionKeysPageRedirect()
    {
        var result = _model.OnGetCreateEncryptionKeys() as RedirectToPageResult;

        result.Should().NotBeNull();
        result!.PageName.Should().Be("CreateEncryptionKeys");
    }
}