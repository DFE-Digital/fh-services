using FamilyHubs.Referral.Core.Commands.CreateReferral;
using FamilyHubs.ReferralService.Shared.Dto.CreateUpdate;
using FamilyHubs.ReferralService.Shared.Dto.Metrics;
using FamilyHubs.SharedKernel.Identity.Models;
using FluentAssertions;

namespace FamilyHubs.Referral.UnitTests;

public class WhenValidatingCreateReferralCommands : BaseUnitTest<CreateReferralCommandValidator>
{
    private const long ExpectedAccountId = 123L;
    private const long ExpectedOrganisationId = 456L;

    private DateTimeOffset RequestTimestamp { get; } = new(new DateTime(2025, 1, 1, 12, 0, 0));
    private FamilyHubsUser FamilyHubsUser { get; } = new()
    {
        AccountId = ExpectedAccountId.ToString(),
        OrganisationId = ExpectedOrganisationId.ToString()
    };

    [Fact]
    public void ThenShouldNotErrorWhenCreateReferralCommandModelIsValid()
    {
        //Arrange
        var testReferral = GetReferralDto();
        testReferral.Id = 0;
        var createReferral = new CreateReferralDto(testReferral, new ConnectionRequestsSentMetricDto(RequestTimestamp));

        var validator = new CreateReferralCommandValidator();
        var testModel = new CreateReferralCommand(createReferral, FamilyHubsUser);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().Be(0);
    }

    [Fact]
    public void ThenShouldErrorWhenConnectionReferralIsNull()
    {
        //Arrange
        var testReferral = WhenUsingReferralCommands.GetReferralDto();
        testReferral.Id = 0;
        var createReferral = new CreateReferralDto(null!, new ConnectionRequestsSentMetricDto(RequestTimestamp));

        var validator = new CreateReferralCommandValidator();
        var testModel = new CreateReferralCommand(createReferral, FamilyHubsUser);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void ThenShouldErrorWhenReferrerIdIsZero()
    {
        //Arrange
        var testReferral = WhenUsingReferralCommands.GetReferralDto();
        testReferral.Id = 0;
        testReferral.ReferralUserAccountDto.Id = 0;
        var createReferral = new CreateReferralDto(testReferral, new ConnectionRequestsSentMetricDto(RequestTimestamp));

        var validator = new CreateReferralCommandValidator();
        var testModel = new CreateReferralCommand(createReferral, FamilyHubsUser);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void ThenShouldErrorWhenConnectionRequestsSentMetricDtoIsNull()
    {
        //Arrange
        var testReferral = WhenUsingReferralCommands.GetReferralDto();
        testReferral.Id = 0;
        var createReferral = new CreateReferralDto(testReferral, null!);

        var validator = new CreateReferralCommandValidator();
        var testModel = new CreateReferralCommand(createReferral, FamilyHubsUser);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}
