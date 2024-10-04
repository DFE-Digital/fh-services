using FamilyHubs.Referral.Core.Commands.CreateOrUpdateOrganisation;
using FamilyHubs.ReferralService.Shared.Dto;
using FluentAssertions;

namespace FamilyHubs.Referral.UnitTests;

public class WhenUsingCreateOrUpdateOrganisationCommand : BaseCreateDbUnitTest<CreateOrUpdateOrganisationCommandHandler>
{
    [Fact]
    public async Task ThenCreateNewOrganisation()
    {
        //Arrange
        var organisation = new OrganisationDto()
        {
            Id = 4,
            Name = "Test Organisation",
            Description = "Test Organisation Description",
        };

        CreateOrUpdateOrganisationCommand command = new(organisation);
        CreateOrUpdateOrganisationCommandHandler handler = new(MockApplicationDbContext, Mapper, Logger);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().BeGreaterThan(0);
        result.Should().Be(organisation.Id);
    }

    [Fact]
    public async Task ThenUpdateOrganisation()
    {
        //Arrange
        MockApplicationDbContext.Organisations.Add(new Data.Entities.Organisation
        {
            Id = 4,
            Name = "Test Organisation",
            Description = "Test Organisation Description",
        });

        await MockApplicationDbContext.SaveChangesAsync();

        var expected = new OrganisationDto
        {
            Id = 4,
            Name = "Test Organisation - Updated",
            Description = "Test Organisation Description - Updated",
        };

        CreateOrUpdateOrganisationCommand command = new(expected);
        CreateOrUpdateOrganisationCommandHandler handler = new(MockApplicationDbContext, Mapper, Logger);

        //Act
        var result = await handler.Handle(command, new CancellationToken());
        var dbOrganisation = MockApplicationDbContext.Organisations.FirstOrDefault(x => x.Id == 4);

        //Assert
        result.Should().BeGreaterThan(0);
        result.Should().Be(expected.Id);
        ArgumentNullException.ThrowIfNull(dbOrganisation);
        dbOrganisation.Name.Should().Be(expected.Name);
        dbOrganisation.Description.Should().Be(expected.Description);

    }
}
