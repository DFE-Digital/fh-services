using FamilyHubs.Referral.Core.Commands.CreateOrUpdateService;
using FamilyHubs.Referral.Data.Entities;
using FamilyHubs.ReferralService.Shared.Dto;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Referral.UnitTests;

public class WhenUsingCreateOrUpdateServiceCommand : BaseCreateDbUnitTest<CreateOrUpdateServiceCommandHandler>
{
    [Fact]
    public async Task ThenCreateNewService()
    {
        //Arrange
        var service = new ReferralServiceDto
        {
            Id = 4,
            Name = "Test Service",
            Description = "Test Organisation Service",
            OrganisationDto = new OrganisationDto
            {
                Id = 4,
                Name = "Test Organisation",
                Description = "Test Organisation Description",
            }

        };

        CreateOrUpdateServiceCommand command = new(service);
        CreateOrUpdateServiceCommandHandler handler = new(MockApplicationDbContext, Mapper, Logger);

        //Act
        var result = await handler.Handle(command, new CancellationToken());
        var dbService = MockApplicationDbContext.ReferralServices.FirstOrDefault(x => x.Id == 4);

        //Assert
        result.Should().BeGreaterThan(0);
        result.Should().Be(service.Id);
        ArgumentNullException.ThrowIfNull(dbService);
        dbService.Name.Should().Be(service.Name);
        dbService.Description.Should().Be(service.Description);
    }

    [Fact]
    public async Task ThenUpdateService()
    {
        //Arrange
        MockApplicationDbContext.ReferralServices.Add(new Data.Entities.ReferralService
        {
            Id = 4,
            Name = "Test Service",
            Description = "Test Service Description",
            OrganizationId = 1,
            Organisation = new Organisation
            {
                Id = 4,
                Name = "Test Organisation",
                Description = "Test Organisation Description",
            }
        });

        await MockApplicationDbContext.SaveChangesAsync();

        var service = new ReferralServiceDto
        {
            Id = 4,
            Name = "Test Service - Updated",
            Description = "Test Organisation Service - Updated",
            OrganisationDto = new OrganisationDto
            {
                Id = 4,
                Name = "Test Organisation - Updated",
                Description = "Test Organisation Description - Updated",
            }

        };

        CreateOrUpdateServiceCommand command = new(service);
        CreateOrUpdateServiceCommandHandler handler = new(MockApplicationDbContext, Mapper, Logger);

        //Act
        var result = await handler.Handle(command, new CancellationToken());
        var dbService = MockApplicationDbContext.ReferralServices
                        .Include(x => x.Organisation)
                        .FirstOrDefault(x => x.Id == 4);

        //Assert
        result.Should().BeGreaterThan(0);
        result.Should().Be(service.Id);
        ArgumentNullException.ThrowIfNull(dbService);
        dbService.Name.Should().Be(service.Name);
        dbService.Description.Should().Be(service.Description);
        dbService.Organisation.Name.Should().Be(service.OrganisationDto.Name);
        dbService.Organisation.Description.Should().Be(service.OrganisationDto.Description);

    }
}
