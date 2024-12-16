using AngleSharp.Html.Dom;
using FamilyHubs.Idams.Maintenance.Core.ApiClient;
using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.Idams.Maintenance.Data.Entities;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FamilyHubs.ReferralService.Shared.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.UI;

[Collection("WebTests")]
public class UsersWebTests : BaseWebTest
{
    private readonly IServiceDirectoryClient _serviceDirectoryClient = Substitute.For<IServiceDirectoryClient>();
    private readonly IIdamService _idamService = Substitute.For<IIdamService>();
    
    protected override void Configure(IServiceCollection services)
    {
        services.AddSingleton(_serviceDirectoryClient);
        services.AddSingleton(_idamService);
    }

    public UsersWebTests()
    {
        _serviceDirectoryClient.GetOrganisations().Returns([TestOrganisations.Organisation1]);
        _idamService
            .GetAccounts(string.Empty, string.Empty, null, false, false, string.Empty)
            .Returns(new PaginatedList<Account>([TestAccounts.GetAccount1(), TestAccounts.GetAccount2()], 2, 1, 10));
    }
    
    [Fact]
    public async Task NavigateToUsers_Index_HasShowFiltersButton()
    {
        var page = await Navigate("/Users");

        var applyFiltersButton = page.QuerySelector("[data-testid=\"show-filters\"]");
        applyFiltersButton.Should().NotBeNull();
    }
    
    [Fact]
    public async Task NavigateToUsers_Index_HasApplyFiltersButton()
    {
        var page = await Navigate("/Users");

        var applyFiltersButton = page.QuerySelector("[data-testid=\"apply-filters\"]");
        applyFiltersButton.Should().NotBeNull();
    }
    
    [Fact]
    public async Task NavigateToUsers_Index_HasNameFilterTextBox()
    {
        var page = await Navigate("/Users");

        var nameFilterTextBox = page.QuerySelector("[data-testid=\"name-filter\"]");
        nameFilterTextBox.Should().NotBeNull();
    }
    
    [Fact]
    public async Task NavigateToUsers_Index_HasEmailFilterTextBox()
    {
        var page = await Navigate("/Users");

        var emailFilterTextBox = page.QuerySelector("[data-testid=\"email-filter\"]");
        emailFilterTextBox.Should().NotBeNull();
    }
    
    [Theory]
    [InlineData("is-la-user")]
    [InlineData("is-vcs-user")]
    public async Task NavigateToUsers_Index_HasUserTypeCheckBox(string id)
    {
        var page = await Navigate("/Users");

        var userCheckBox = page.QuerySelector($"[data-testid=\"{id}\"]");
        userCheckBox.Should().NotBeNull();
    }
    
    [Fact]
    public async Task NavigateToUsers_Index_ListsAccounts()
    {
        var page = await Navigate("/Users");

        var accountTable = page.QuerySelector("[class=\"govuk-table\"]") as IHtmlTableElement;
        accountTable.Should().NotBeNull();
        AccountExistsInTable(accountTable!, TestAccounts.GetAccount1()).Should().BeTrue();
        AccountExistsInTable(accountTable!, TestAccounts.GetAccount2()).Should().BeTrue();
    }

    private static bool AccountExistsInTable(IHtmlTableElement accountTable, Account account)
    {
        return accountTable.Bodies.Any(
            body => body.Rows.Any(
                row => row.Cells.Any(
                    cell => cell.InnerHtml.Contains(account.Name))));
    }
}