using FluentAssertions;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.UI;

[Collection("WebTests")]
public class AddDfEAdminAccountWebTests: BaseWebTest
{
    [Fact]
    public async Task NavigateToAddDfEAdminAccount_Index_HasContinueButton()
    {
        var page = await Navigate("/AddDfEAdminAccount");

        var continueButton = page.QuerySelector("[data-testid=\"continue\"]");
        continueButton.Should().NotBeNull();
    }
    
    [Fact]
    public async Task NavigateToAddDfEAdminAccount_Index_HasNameEntryField()
    {
        var page = await Navigate("/AddDfEAdminAccount");

        var nameField = page.QuerySelector("[data-testid=\"name\"]");
        nameField.Should().NotBeNull();
    }
    
    [Fact]
    public async Task NavigateToAddDfEAdminAccount_Index_HasEmailEntryField()
    {
        var page = await Navigate("/AddDfEAdminAccount");

        var emailField = page.QuerySelector("[data-testid=\"email\"]");
        emailField.Should().NotBeNull();
    }
    
    [Fact]
    public async Task NavigateToAddDfEAdminAccount_Index_HasPhoneNumberEntryField()
    {
        var page = await Navigate("/AddDfEAdminAccount");

        var phoneNumberField = page.QuerySelector("[data-testid=\"phone-number\"]");
        phoneNumberField.Should().NotBeNull();
    }
}