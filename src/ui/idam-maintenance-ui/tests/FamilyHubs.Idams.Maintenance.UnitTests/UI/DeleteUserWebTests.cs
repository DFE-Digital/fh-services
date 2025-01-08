using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.UI;

[Collection("WebTests")]
public class DeleteUserWebTests : BaseWebTest
{
    private readonly IIdamService _idamService = Substitute.For<IIdamService>();
    
    protected override void Configure(IServiceCollection services)
    {
        services.AddSingleton(_idamService);
    }
    
    [Fact]
    public async Task NavigateToRoot_Index_HasRemoveUserRadioButton()
    {
        var account1 = TestAccounts.GetAccount1();
        _idamService.GetAccountById(account1.Id).Returns(account1);
        var page = await Navigate($"/DeleteUser?accountId={account1.Id}");

        var removeUserButton = page.QuerySelector("[id=\"remove-user\"]");
        removeUserButton.Should().NotBeNull();
    }
    
    [Fact]
    public async Task NavigateToRoot_Index_HasDotNotRemoveUserRadioButton()
    {
        var account1 = TestAccounts.GetAccount1();
        _idamService.GetAccountById(account1.Id).Returns(account1);
        var page = await Navigate($"/DeleteUser?accountId={account1.Id}");

        var dotNotRemoveUserButton = page.QuerySelector("[id=\"remove-user-2\"]");
        dotNotRemoveUserButton.Should().NotBeNull();
    }
    
    [Fact]
    public async Task NavigateToRoot_Index_HasContinueRadioButton()
    {
        var account1 = TestAccounts.GetAccount1();
        _idamService.GetAccountById(account1.Id).Returns(account1);
        var page = await Navigate($"/DeleteUser?accountId={account1.Id}");

        var continueButton = page.QuerySelector("[data-testid=\"continue-button\"]");
        continueButton.Should().NotBeNull();
    }
}