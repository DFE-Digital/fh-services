using FamilyHubs.Referral.Core.Queries.GetReferrals;
using FluentAssertions;

namespace FamilyHubs.Referral.UnitTests;

public class WhenValidatingGetReferralsByReferrerCommands : BaseUnitTest<GetReferralsByReferrerCommandValidator>
{
    [Fact]
    public void ThenShouldNotErrorWhenGetReferralsByReferrerCommandModelIsValid()
    {
        //Arrange
        var validator = new GetReferralsByReferrerCommandValidator();
        var testModel = new GetReferralsByReferrerCommand("id", null, null, null, 1, 99);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().Be(0);
    }
}