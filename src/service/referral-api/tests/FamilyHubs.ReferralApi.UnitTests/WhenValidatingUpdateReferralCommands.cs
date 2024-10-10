using FamilyHubs.Referral.Core.Commands.UpdateReferral;
using FluentAssertions;

namespace FamilyHubs.Referral.UnitTests;

public class WhenValidatingUpdateReferralCommands : BaseUnitTest<UpdateReferralCommandValidator>
{
    [Fact]
    public void ThenShouldNotErrorWhenUpdateReferralCommandModelIsValid()
    {
        //Arrange
        var testReferral = WhenUsingReferralCommands.GetReferralDto();
        var validator = new UpdateReferralCommandValidator();
        var testModel = new UpdateReferralCommand(testReferral.Id, testReferral);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().Be(0);
    }

    [Fact]
    public void ThenShouldErrorWhenUpdateReferralCommandModelIsValidButRefererIdIsZero()
    {
        //Arrange
        var testReferral = WhenUsingReferralCommands.GetReferralDto();
        var validator = new UpdateReferralCommandValidator();
        var testModel = new UpdateReferralCommand(testReferral.Id, testReferral);
        testReferral.ReferralUserAccountDto.Id = 0;

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}
