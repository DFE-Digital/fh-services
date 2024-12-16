using FamilyHubs.Idams.Maintenance.Core.ApiClient;
using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.Idams.Maintenance.UnitTests.Support;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.UI;

[Collection("WebTests")]
public class ModifyUserPermissionsWebTests : BaseWebTest
{
    protected override void Configure(IServiceCollection services)
    {
        var serviceDirectoryClient = Substitute.For<IServiceDirectoryClient>();
        serviceDirectoryClient.GetOrganisations().Returns([TestOrganisations.Organisation1]);
        services.AddSingleton(serviceDirectoryClient);
        
        var idamService = Substitute.For<IIdamService>();
        idamService.GetAccountById(TestAccounts.GetAccount1().Id).Returns(TestAccounts.GetAccount1());
        services.AddSingleton(idamService);
    }

    [Fact]
    public async Task NavigateModifyUserPermissions_Index_HasContinueButton()
    {
        var account1 = TestAccounts.GetAccount1();
        var page = await Navigate($"/ModifyUserPermissions?id={account1.Id}");
        
        var continueButton = page.QuerySelector("[data-testid=\"continue-button\"]");
        continueButton.Should().NotBeNull();
    }
}