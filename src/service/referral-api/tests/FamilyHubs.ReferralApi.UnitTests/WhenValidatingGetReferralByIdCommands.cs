using FamilyHubs.Referral.Core.Queries.GetReferrals;
using FluentAssertions;

namespace FamilyHubs.Referral.UnitTests;

public class WhenValidatingGetReferralByIdCommands : BaseUnitTest<GetReferralByIdCommandValidator>
{
    [Fact]
    public void ThenShouldNotErrorWhenGetReferralByIdCommandModelIsValid()
    {
        //Arrange
        var validator = new GetReferralByIdCommandValidator();
        var testModel = new GetReferralByIdCommand(1);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().Be(0);
    }
}
