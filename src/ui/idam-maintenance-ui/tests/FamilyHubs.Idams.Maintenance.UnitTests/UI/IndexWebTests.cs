using FluentAssertions;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.UI;

[Collection("WebTests")]
public class IndexWebTests : BaseWebTest
{
    [Fact]
    public async Task NavigateToRoot_Index_HasStartButton()
    {
        var page = await Navigate("/");

        var startButton = page.QuerySelector("[data-testid=\"start-button\"]");
        startButton.Should().NotBeNull();
    }
}