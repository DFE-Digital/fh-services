using FamilyHubs.ServiceDirectory.Core.Commands.Organisations.CreateOrganisation;
using FamilyHubs.ServiceDirectory.Core.Commands.Services.CreateService;
using FamilyHubs.ServiceDirectory.Core.Exceptions;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.ServiceDirectory.Core.IntegrationTests.Services;

public class WhenUsingCreateServiceCommand : DataIntegrationTestBase
{
    private const string ServiceName = "New Service with Location";

    [Fact]
    public async Task ThenCreateService()
    {
        //Arrange
        await CreateOrganisation();
        var newService = TestDataProvider.GetTestCountyCouncilServicesChangeDto2(Mapper, TestOrganisationWithoutAnyServices.Id);

        var command = new CreateServiceCommand(newService);

        var handler = new CreateServiceCommandHandler(TestDbContext, Mapper, GetLogger<CreateServiceCommandHandler>());

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);

        var actualService = TestDbContext.Services.SingleOrDefault(s => s.Name == newService.Name);
        actualService.Should().NotBeNull();
        actualService.Should().BeEquivalentTo(newService, options =>
            options.Excluding((IMemberInfo info) => info.Name.Contains("Id"))
                .Excluding((IMemberInfo info) => info.Name.Contains("Distance")));
    }

    [Fact]
    public async Task ThenCreateServiceWithLocation()
    {
        //Arrange
        var organisation = await CreateOrganisationDetails();
        var newService = TestDataProvider.GetTestCountyCouncilServicesChangeDto2(Mapper, organisation.Id);

        newService.Name = ServiceName;
        newService.ServiceAtLocations.Add(new ServiceAtLocationDto
        {
            LocationId = organisation.Locations.First().Id,
            Description = "description",
            Schedules = new List<ScheduleDto>
            {
                new()
                {
                    AttendingType = "Online",
                    ByDay = "MO",
                    Freq = FrequencyType.WEEKLY
                }
            }
        });

        var command = new CreateServiceCommand(newService);

        var handler = new CreateServiceCommandHandler(TestDbContext, Mapper, GetLogger<CreateServiceCommandHandler>());

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);

        var actualService = TestDbContext.Services
            .Include(service => service.Locations)
            .Include(service => service.ServiceAtLocations)
            .SingleOrDefault(s => s.Name == ServiceName);

        actualService.Should().NotBeNull();
        actualService!.Locations.Should().HaveCount(1);
        actualService.ServiceAtLocations.Should().HaveCount(1);

        actualService
            .Should()
            .BeEquivalentTo(newService, options =>
            options
                .Excluding((IMemberInfo info) => info.Name.Contains("Id"))
                .Excluding((IMemberInfo info) => info.Name.Contains("Distance")));

        actualService.Locations
            .Should()
            .BeEquivalentTo(organisation.Locations, options => options
                .Excluding((IMemberInfo info) => info.Name.Contains("Distance")));

        actualService.ServiceAtLocations
            .Should()
            .BeEquivalentTo(newService.ServiceAtLocations, options => options
                .Excluding(info => info.Name.Contains("Id")));
    }

    [Fact]
    public async Task ThenCreateServiceAndAttachExistingTaxonomy()
    {
        //Arrange
        var organisation = await CreateOrganisation();

        var newService = TestDataProvider.GetTestCountyCouncilServicesChangeDto2(Mapper, organisation.Id);

        newService.Name = ServiceName;
        newService.TaxonomyIds.Add(TestDbContext.Taxonomies.First().Id);

        var createServiceCommand = new CreateServiceCommand(newService);
        var handler = new CreateServiceCommandHandler(TestDbContext, Mapper, GetLogger<CreateServiceCommandHandler>());

        //Act
        var result = await handler.Handle(createServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);

        var actualService = TestDbContext.Services
            .Include(service => service.Taxonomies)
            .SingleOrDefault(s => s.Name == newService.Name);

        actualService.Should().NotBeNull();
        actualService!.Taxonomies.Count.Should().Be(1);
        actualService.Taxonomies.First().Should().BeEquivalentTo(TestDbContext.Taxonomies.First());
    }

    [Fact]
    public async Task ThenCreateDuplicateService_ShouldThrowException()
    {
        //Arrange
        await CreateOrganisationDetails();

        var command = new CreateOrganisationCommand(TestOrganisation);
        var handler = new CreateOrganisationCommandHandler(TestDbContext, Mapper, GetLogger<CreateOrganisationCommandHandler>());

        // Act 
        // Assert
        await handler
            .Invoking(x => x.Handle(command, CancellationToken.None))
            .Should()
            .ThrowAsync<AlreadyExistsException>();
    }
}