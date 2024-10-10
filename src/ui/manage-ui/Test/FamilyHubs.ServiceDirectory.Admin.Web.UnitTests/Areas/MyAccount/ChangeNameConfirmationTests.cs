using FamilyHubs.ServiceDirectory.Admin.Web.Areas.MyAccount.Pages;
using FamilyHubs.SharedKernel.Identity;
using System.Security.Claims;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.MyAccount;

public class ChangeNameConfirmationTests
{
    [Fact]
    public void OnGet_Valid_SetNewNameFromCache()
    {
        //  Arrange
        const string expectedName = "test name";

        var claims = new List<Claim> { new(FamilyHubsClaimTypes.FullName, expectedName) };
        var mockHttpContext = TestHelper.GetHttpContext(claims);
        var sut = new ChangeNameConfirmationModel
        {
            PageContext = { HttpContext = mockHttpContext }
        };

        //  Act
        sut.OnGet();

        //  Assert
        Assert.Equal(expectedName, sut.NewName);
    }
}