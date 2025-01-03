using FluentAssertions;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.UI;

[Collection("WebTests")]
public class CreateEncryptionKeysWebTests : BaseWebTest
{
    [Fact]
    public async Task NavigateToCreateEncryptionKeys_Index_HasDbEncryptionKey()
    {
        var page = await Navigate("/CreateEncryptionKeys");

        var dbEncryptionKey = page.QuerySelector("[data-testid=\"db-encryption-key\"]");
        dbEncryptionKey.Should().NotBeNull();
        dbEncryptionKey!.InnerHtml.Should().StartWith("DbEncryptionKey: ");
    }
    
    [Fact]
    public async Task NavigateToCreateEncryptionKeys_Index_HasDbEncryptionIvKey()
    {
        var page = await Navigate("/CreateEncryptionKeys");

        var dbEncryptionIvKey = page.QuerySelector("[data-testid=\"db-encryption-iv-key\"]");
        dbEncryptionIvKey.Should().NotBeNull();
        dbEncryptionIvKey!.InnerHtml.Should().StartWith("DbEncryptionIvKey: ");
    }
}