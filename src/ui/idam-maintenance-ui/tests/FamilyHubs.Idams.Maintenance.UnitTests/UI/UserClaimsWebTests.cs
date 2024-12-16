using System.Text.Encodings.Web;
using AngleSharp.Html.Dom;
using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.UI;

[Collection("WebTests")]
public class UserClaimsWebTests : BaseWebTest
{
    private readonly IIdamService _idamService = Substitute.For<IIdamService>();
    
    protected override void Configure(IServiceCollection services)
    {
        services.AddSingleton(_idamService);
    }

    [Fact]
    public async Task NavigateToRoot_Index_HasStartButton()
    {
        var account1 = TestAccounts.GetAccount1();
        var organisationClaim = account1.Claims.First(c => c.Name == "OrganisationId");
        var roleClaim = account1.Claims.First(c => c.Name == "role");
        _idamService.GetAccountClaimsById(account1.Id).Returns(account1.Claims.ToList());
        
        var page = await Navigate($"/UserClaims?accountId={account1.Id}&username={UrlEncoder.Default.Encode(account1.Name)}");

        var accountTable = page.QuerySelector("[class=\"govuk-table\"]") as IHtmlTableElement;
        accountTable.Should().NotBeNull();
        AccountExistsInTable(accountTable!, organisationClaim.Value).Should().BeTrue();
        AccountExistsInTable(accountTable!, roleClaim.Value).Should().BeTrue();
    }
    
    private static bool AccountExistsInTable(IHtmlTableElement accountTable, string value)
    {
        return accountTable.Bodies.Any(
            body => body.Rows.Any(
                row => row.Cells.Any(
                    cell => cell.InnerHtml.Contains(value))));
    }
}