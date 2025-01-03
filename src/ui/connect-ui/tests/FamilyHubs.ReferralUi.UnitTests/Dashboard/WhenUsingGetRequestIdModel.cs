using FamilyHubs.Referral.Web.Pages.Referrals.Vcs;
using FluentAssertions;

namespace FamilyHubs.ReferralUi.UnitTests.Dashboard;

public class WhenUsingGetRequestIdModel
{
    [Fact]
    public void ThenRequestIdIsSet()
    {
        // Arrange
        var model = new GetRequestIdModel();

        // Act
        model.OnGet(1);

        // Assert
        model.RequestId.Should().Be(1);
    }
}