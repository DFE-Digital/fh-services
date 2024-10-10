using FamilyHubs.Referral.Core.Queries.GetReferrals;
using FluentAssertions;

namespace FamilyHubs.Referral.UnitTests;

public class WhenValidatingGetReferralsByOrganisationIdCommands : BaseUnitTest<GetReferralsByOrganisationIdCommandValidator>
{
    [Fact]
    public void ThenShouldNotErrorWhenGetReferralsByOrganisationIdCommandModelIsValid()
    {
        //Arrange
        var validator = new GetReferralsByOrganisationIdCommandValidator();
        var testModel = new GetReferralsByOrganisationIdCommand(1, null, null, null, 1, 99);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().Be(0);
    }
}