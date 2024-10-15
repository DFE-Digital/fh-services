using FamilyHubs.RequestForSupport.Web.Pages.Vcs;
using FluentAssertions;

namespace FamilyHubs.RequestForSupport.UnitTests;

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