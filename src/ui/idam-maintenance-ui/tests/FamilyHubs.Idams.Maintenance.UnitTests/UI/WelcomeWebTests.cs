using FluentAssertions;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.UI;

[Collection("WebTests")]
public class WelcomeWebTests : BaseWebTest
{
    [Fact]
    public async Task NavigateToRoot_Index_HasLinkToAddDfEAdminPage()
    {
        var page = await Navigate("/Welcome");

        var link = page.QuerySelector("[id=\"add-dfeadmin\"]");
        link.Should().NotBeNull();
    }
    
    [Fact]
    public async Task NavigateToRoot_Index_HasLinkToChangeUserPermissionsPage()
    {
        var page = await Navigate("/Welcome");

        var link = page.QuerySelector("[id=\"change-user-permissions\"]");
        link.Should().NotBeNull();
    }
    
    [Fact]
    public async Task NavigateToRoot_Index_HasLinkToCreateEncryptionKeysPage()
    {
        var page = await Navigate("/Welcome");

        var link = page.QuerySelector("[id=\"create-encryption-keys\"]");
        link.Should().NotBeNull();
    }
}