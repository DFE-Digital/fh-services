using FamilyHubs.Referral.Core.Commands.CreateOrUpdateService;
using FamilyHubs.Referral.Data.Entities;
using FamilyHubs.ReferralService.Shared.Dto;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace FamilyHubs.Referral.Integration.Tests;

public class WhenCreateOrUpdateService : DataIntegrationTestBase
{
    [Fact]
    public async Task ThenCreateServiceWithExistingOrganisation()
    {
        var organisation = await CreateOrganisation();

        Data.Entities.ReferralService service = new Data.Entities.ReferralService
        {
            Id = 787,
            Name = "Test service name 002",
            Description = "Keeping Children Safe Online",
            OrganizationId = 1,
            Organisation = organisation
        };

        var serviceDto = Mapper.Map<ReferralServiceDto>(service);

        CreateOrUpdateServiceCommand createOrUpdateOrganisationCommand = new(serviceDto);
        CreateOrUpdateServiceCommandHandler handler = new(TestDbContext, Mapper, Substitute.For<ILogger<CreateOrUpdateServiceCommandHandler>>());

        //Act
        var result = await handler.Handle(createOrUpdateOrganisationCommand, new CancellationToken());

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(787);
    }

    private async Task<Organisation> CreateOrganisation()
    {
        var organisation = new Organisation
        {
            Id = 198,
            Name = "Test Harsha Madhu Vcs002",
            Description = "Test Harsha Madhu Vcs002",
        };

        TestDbContext.Organisations.Add(organisation);

        await TestDbContext.SaveChangesAsync();

        return organisation;
    }
}
