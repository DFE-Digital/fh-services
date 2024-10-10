using FamilyHubs.ServiceDirectory.Core.Commands.Services.UpdateService;
using FamilyHubs.ServiceDirectory.Data.Entities;
using FamilyHubs.ServiceDirectory.Shared.CreateUpdateDto;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.ServiceDirectory.Core.IntegrationTests.Services;

public class WhenUsingUpdateServiceCommand : DataIntegrationTestBase
{
    private ServiceDto _serviceDto = null!;
    private ServiceChangeDto _serviceChange = null!;
    private UpdateServiceCommand _updateServiceCommand = null!;
    private UpdateServiceCommandHandler _updateServiceCommandHandler = null!;

    private async Task Setup()
    {
        await CreateOrganisationDetails();
        _serviceDto = TestOrganisation.Services.ElementAt(0);
        _serviceChange = Mapper.Map<ServiceChangeDto>(_serviceDto);
        _updateServiceCommand = new UpdateServiceCommand(_serviceDto.Id, _serviceChange);
        _updateServiceCommandHandler = new UpdateServiceCommandHandler(TestDbContext, Mapper);
    }

    private Service? GetService(string serviceDtoName) => TestDbContext.Services
        .Include(s => s.Eligibilities)
        .Include(s => s.ServiceAreas)
        .Include(s => s.ServiceDeliveries)
        .Include(s => s.Languages)
        .Include(s => s.CostOptions)
        .Include(s => s.Contacts)
        .Include(s => s.Schedules)
        .Include(s => s.Locations)
        .Include(s => s.Taxonomies)
        .SingleOrDefault(s => s.Name == serviceDtoName);

    [Fact]
    public async Task ThenUpdateServiceOnly()
    {
        //Arrange
        await Setup();

        //Act
        var result = await _updateServiceCommandHandler.Handle(_updateServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_serviceDto.Id);
        var actualService = GetService(_serviceDto.Name);
        actualService.Should().NotBeNull();
        actualService!.Description.Should().Be(_serviceDto.Description);
    }

    [Fact]
    public async Task ThenUpdateServiceAddAndDeleteEligibilities()
    {
        //Arrange
        await Setup();

        var existingItem = _serviceChange.Eligibilities.ElementAt(0);

        var expected = new EligibilityDto
        {
            MaximumAge = 2,
            MinimumAge = 0,
            EligibilityType = null
        };
        _serviceChange.Eligibilities.Clear();
        _serviceChange.Eligibilities.Add(expected);

        //Act
        var result = await _updateServiceCommandHandler.Handle(_updateServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_serviceChange.Id);

        var actualService = GetService(_serviceDto.Name);
        actualService.Should().NotBeNull();
        actualService!.Eligibilities.Count.Should().Be(1);

        var actualEligibility =
            TestDbContext.Eligibilities.SingleOrDefault(s => s.EligibilityType == expected.EligibilityType);
        actualEligibility.Should().NotBeNull();
        actualEligibility.Should().BeEquivalentTo(expected, options =>
            options.Excluding(info => info.Name.Contains("Id"))
                .Excluding(info => info.Name.Contains("Distance")));

        var unexpectedEligibilityCount = TestDbContext.Eligibilities.Count(e => e.Id == existingItem.Id);
        unexpectedEligibilityCount.Should().Be(0);
    }

    [Fact]
    public async Task ThenUpdateServiceUpdatedEligibilities()
    {
        //Arrange
        await Setup();

        var expected = _serviceChange.Eligibilities.ElementAt(0);
        expected.MinimumAge = 500;
        expected.MaximumAge = 5000;

        //Act
        var result = await _updateServiceCommandHandler.Handle(_updateServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_serviceChange.Id);

        var actualService = GetService(_serviceDto.Name);
        actualService.Should().NotBeNull();
        actualService!.Eligibilities.Count.Should().Be(1);

        var actualEligibility =
            TestDbContext.Eligibilities.SingleOrDefault(s => s.EligibilityType == expected.EligibilityType);
        actualEligibility.Should().NotBeNull();
        actualEligibility.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ThenUpdateServiceAddAndDeleteServiceAreas()
    {
        //Arrange
        await Setup();

        var existingItem = _serviceChange.ServiceAreas.ElementAt(0);
        var expected = new ServiceAreaDto
        {
            ServiceAreaName = "ServiceAreaName",
            Extent = "Extent",
            Uri = "Uri"
        };
        _serviceChange.ServiceAreas.Clear();
        _serviceChange.ServiceAreas.Add(expected);

        //Act
        var result = await _updateServiceCommandHandler.Handle(_updateServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_serviceChange.Id);

        var actualService = GetService(_serviceDto.Name);
        actualService.Should().NotBeNull();
        actualService!.ServiceAreas.Count.Should().Be(1);

        var actualServiceArea =
            TestDbContext.ServiceAreas.SingleOrDefault(s => s.ServiceAreaName == expected.ServiceAreaName);
        actualServiceArea.Should().NotBeNull();
        actualServiceArea.Should().BeEquivalentTo(expected, options =>
            options.Excluding(info => info.Name.Contains("Id"))
                .Excluding(info => info.Name.Contains("Distance")));

        var unexpectedServiceAreaCount = TestDbContext.ServiceAreas.Count(e => e.Id == existingItem.Id);
        unexpectedServiceAreaCount.Should().Be(0);
    }

    [Fact]
    public async Task ThenUpdateServiceUpdatedServiceAreas()
    {
        //Arrange
        await Setup();

        var expected = _serviceChange.ServiceAreas.ElementAt(0);
        expected.ServiceAreaName = "Updated ServiceAreaName";
        expected.Extent = "Updated Extent";

        //Act
        var result = await _updateServiceCommandHandler.Handle(_updateServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_serviceChange.Id);

        var actualService = GetService(_serviceDto.Name);
        actualService.Should().NotBeNull();
        actualService!.ServiceAreas.Count.Should().Be(1);

        var actualEntity =
            TestDbContext.ServiceAreas.SingleOrDefault(s => s.ServiceAreaName == expected.ServiceAreaName);
        actualEntity.Should().NotBeNull();
        actualEntity.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ThenUpdateServiceDeleteServiceDeliveries()
    {
        //Arrange
        await Setup();

        var existingItem = _serviceChange.ServiceDeliveries.ElementAt(0);

        _serviceChange.ServiceDeliveries.Clear();

        //Act
        var result = await _updateServiceCommandHandler.Handle(_updateServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_serviceChange.Id);

        var actualService = GetService(_serviceDto.Name);
        actualService.Should().NotBeNull();
        actualService!.ServiceDeliveries.Count.Should().Be(0);

        var unexpectedServiceDeliveryCount = TestDbContext.ServiceDeliveries.Count(e => e.Id == existingItem.Id);
        unexpectedServiceDeliveryCount.Should().Be(0);
    }

    [Fact]
    public async Task ThenUpdateServiceUpdatedServiceDeliveries()
    {
        //Arrange
        await Setup();

        var expected = _serviceChange.ServiceDeliveries.ElementAt(0);
        expected.Name = AttendingType.InPerson;

        //Act
        var result = await _updateServiceCommandHandler.Handle(_updateServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_serviceChange.Id);

        var actualService = GetService(_serviceDto.Name);
        actualService.Should().NotBeNull();
        actualService!.ServiceDeliveries.Count.Should().Be(1);

        var actualServiceDelivery = TestDbContext.ServiceDeliveries.SingleOrDefault(s => s.Name == expected.Name);
        actualServiceDelivery.Should().NotBeNull();
        actualServiceDelivery.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ThenUpdateServiceAddAndDeleteLanguages()
    {
        //Arrange
        await Setup();

        var existingItem = _serviceChange.Languages.ElementAt(0);
        var expected = new LanguageDto()
        {
            Name = "New Language",
            Code = "xx"
        };

        _serviceChange.Languages.Clear();
        _serviceChange.Languages.Add(expected);

        //Act
        var result = await _updateServiceCommandHandler.Handle(_updateServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_serviceChange.Id);

        var actualService = GetService(_serviceDto.Name);
        actualService.Should().NotBeNull();
        actualService!.Languages.Count.Should().Be(1);

        var actualLanguage = TestDbContext.Languages.SingleOrDefault(s => s.Name == expected.Name);
        actualLanguage.Should().NotBeNull();
        actualLanguage.Should().BeEquivalentTo(expected, options =>
            options.Excluding((IMemberInfo info) => info.Name.Contains("Id"))
                .Excluding((IMemberInfo info) => info.Name.Contains("Distance")));

        var unexpectedLanguageCount = TestDbContext.Languages.Count(e => e.Id == existingItem.Id);
        unexpectedLanguageCount.Should().Be(0);
    }

    [Fact]
    public async Task ThenUpdateServiceUpdatedLanguages()
    {
        //Arrange
        await Setup();

        var expected = _serviceChange.Languages.ElementAt(0);
        expected.Name = "Updated Language";
        expected.Code = "UL";

        //Act
        var result = await _updateServiceCommandHandler.Handle(_updateServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_serviceChange.Id);

        var actualService = GetService(_serviceDto.Name);
        actualService.Should().NotBeNull();
        actualService!.Languages.Count.Should().Be(1);

        var actualLanguage = TestDbContext.Languages.SingleOrDefault(s => s.Name == expected.Name);
        actualLanguage.Should().NotBeNull();
        actualLanguage.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ThenUpdateServiceAddAndDeleteCostOptions()
    {
        //Arrange
        await Setup();

        var existingItem = _serviceChange.CostOptions.ElementAt(0);
        var expected = new CostOptionDto
        {
            ValidFrom = DateTime.UtcNow,
            ValidTo = DateTime.UtcNow,
            Option = "new Option",
            Amount = 123,
            AmountDescription = "Amount Description"
        };

        _serviceChange.CostOptions.Clear();
        _serviceChange.CostOptions.Add(expected);

        //Act
        var result = await _updateServiceCommandHandler.Handle(_updateServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_serviceChange.Id);

        var actualService = GetService(_serviceChange.Name);
        actualService.Should().NotBeNull();
        actualService!.CostOptions.Count.Should().Be(1);

        var actualCostOption = TestDbContext.CostOptions.SingleOrDefault(s => s.Option == expected.Option);
        actualCostOption.Should().NotBeNull();
        actualCostOption.Should().BeEquivalentTo(expected, options =>
            options.Excluding(info => info.Name.Contains("Id"))
                .Excluding(info => info.Name.Contains("Distance")));

        var unexpectedCostOptionCount = TestDbContext.CostOptions.Count(e => e.Id == existingItem.Id);
        unexpectedCostOptionCount.Should().Be(0);
    }

    [Fact]
    public async Task ThenUpdateServiceUpdatedCostOptions()
    {
        //Arrange
        await Setup();

        var expected = _serviceChange.CostOptions.ElementAt(0);
        expected.Amount = 987;
        expected.Option = "Updated Option";
        expected.AmountDescription = "Updated Amount Description";

        //Act
        var result = await _updateServiceCommandHandler.Handle(_updateServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_serviceChange.Id);

        var actualService = GetService(_serviceChange.Name);
        actualService.Should().NotBeNull();
        actualService!.CostOptions.Count.Should().Be(1);

        var actualCostOption = TestDbContext.CostOptions.SingleOrDefault(s => s.Option == expected.Option);
        actualCostOption.Should().NotBeNull();
        actualCostOption.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ThenUpdateServiceAddAndDeleteContacts()
    {
        //Arrange
        await Setup();

        var existingItem = _serviceChange.Contacts.ElementAt(0);
        var contact = new ContactDto
        {
            Id = 0,
            Name = "New Contact",
            Telephone = "New Telephone"
        };
        _serviceChange.Contacts.Clear();
        _serviceChange.Contacts.Add(contact);

        //Act
        var result = await _updateServiceCommandHandler.Handle(_updateServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_serviceChange.Id);

        var actualService = GetService(_serviceChange.Name);
        actualService.Should().NotBeNull();
        actualService!.Contacts.Count.Should().Be(1);

        var actualContact = TestDbContext.Contacts.SingleOrDefault(s => s.Name == contact.Name);
        actualContact.Should().NotBeNull();
        actualContact.Should().BeEquivalentTo(contact, options =>
            options.Excluding((IMemberInfo info) => info.Name.Contains("Id"))
                .Excluding((IMemberInfo info) => info.Name.Contains("Distance")));

        var unexpectedContactCount = TestDbContext.Contacts.Count(e => e.Id == existingItem.Id);
        unexpectedContactCount.Should().Be(0);
    }

    [Fact]
    public async Task ThenUpdateServiceWithUpdatedContacts()
    {
        //Arrange
        await Setup();

        var contact = _serviceChange.Contacts.ElementAt(0);
        contact.Name = "Updated Name";
        contact.Email = "Updated Email";
        contact.Telephone = "Updated Telephone";

        //Act
        var result = await _updateServiceCommandHandler.Handle(_updateServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_serviceChange.Id);

        var actualService = GetService(_serviceChange.Name);
        actualService.Should().NotBeNull();
        actualService!.Contacts.Count.Should().Be(1);

        var actualContact = TestDbContext.Contacts.SingleOrDefault(s => s.Name == contact.Name);
        actualContact.Should().NotBeNull();
        actualContact.Should().BeEquivalentTo(contact);
    }

    [Fact]
    public async Task ThenUpdateServiceAddAndDeleteSchedules()
    {
        //Arrange
        await Setup();

        var existingItem = _serviceChange.Schedules.ElementAt(0);
        var expected = new ScheduleDto
        {
            ValidFrom = DateTime.UtcNow,
            ValidTo = DateTime.UtcNow,
            ByDay = "New ByDay",
            ByMonthDay = "New ByMonthDay"
        };

        _serviceChange.Schedules.Clear();
        _serviceChange.Schedules.Add(expected);

        //Act
        var result = await _updateServiceCommandHandler.Handle(_updateServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_serviceChange.Id);

        var actualService = GetService(_serviceChange.Name);
        actualService.Should().NotBeNull();
        actualService!.Schedules.Count.Should().Be(1);

        var actualSchedule = TestDbContext.Schedules.SingleOrDefault(s => s.ByDay == expected.ByDay);
        actualSchedule.Should().NotBeNull();
        actualSchedule.Should().BeEquivalentTo(expected, options =>
            options.Excluding(info => info.Name.Contains("Id"))
                .Excluding(info => info.Name.Contains("Distance")));

        var unexpectedScheduleCount = TestDbContext.Schedules.Count(e => e.Id == existingItem.Id);
        unexpectedScheduleCount.Should().Be(0);
    }

    [Fact]
    public async Task ThenUpdateServiceUpdatedSchedules()
    {
        //Arrange
        await Setup();

        var expected = _serviceChange.Schedules.ElementAt(0);
        expected.ByDay = "Updated ByDay";
        expected.ByMonthDay = "Updated ByMonthDay";

        //Act
        var result = await _updateServiceCommandHandler.Handle(_updateServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_serviceChange.Id);

        var actualService = GetService(_serviceChange.Name);
        actualService.Should().NotBeNull();
        actualService!.Schedules.Count.Should().Be(1);

        var actualSchedule = TestDbContext.Schedules.SingleOrDefault(s => s.ByDay == expected.ByDay);
        actualSchedule.Should().NotBeNull();
        actualSchedule.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ThenUpdateServiceAddAndDeleteLocations()
    {
        //Arrange
        await Setup();

        var location = _serviceDto.Locations.ElementAt(0);
        var expected = new LocationDto
        {
            Name = "New Location",
            Description = "new Description",
            Address1 = "Address1",
            City = "City",
            Country = "Country",
            PostCode = "PostCode",
            StateProvince = "StateProvince",
            LocationTypeCategory = LocationTypeCategory.NotSet,
            Latitude = 0,
            Longitude = 0,
            LocationType = LocationType.Postal
        };
        var existingLocationId = CreateLocation(expected);

        _serviceChange.ServiceAtLocations.Clear();
        _serviceChange.ServiceAtLocations.Add(new ServiceAtLocationDto
        {
            LocationId = existingLocationId
        });

        //Act
        var result = await _updateServiceCommandHandler.Handle(_updateServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_serviceChange.Id);

        var actualService = GetService(_serviceChange.Name);
        actualService.Should().NotBeNull();
        actualService!.Locations.Count.Should().Be(1);

        var actualEntity = TestDbContext.Locations.SingleOrDefault(s => s.Name == expected.Name);
        actualEntity.Should().NotBeNull();
        actualEntity.Should().BeEquivalentTo(expected, options =>
            options.Excluding((IMemberInfo info) => info.Name.Contains("Id"))
                .Excluding((IMemberInfo info) => info.Name.Contains("Distance")));

        //Delete wont cascade delete Locations, so existing will be left behind
        var detachedEntity = TestDbContext.Locations.SingleOrDefault(s => s.Name == location.Name);
        detachedEntity.Should().NotBeNull();
        detachedEntity.Should().BeEquivalentTo(location, options =>
            options.Excluding((IMemberInfo info) => info.Name.Contains("Id"))
                .Excluding((IMemberInfo info) => info.Name.Contains("Distance")));
    }

    [Fact]
    public async Task ThenUpdateServiceAddAndDeleteLocationsWithSchedules()
    {
        //Arrange
        await Setup();

        await AddServiceAtLocationSchedule(1, new ScheduleDto
        {
            AttendingType = AttendingType.InPerson.ToString(),
            Freq = FrequencyType.WEEKLY,
            ByDay = "MO"
        });

        var location = _serviceDto.Locations.ElementAt(0);
        var expected = new LocationDto
        {
            Name = "New Location",
            Description = "new Description",
            Address1 = "Address1",
            City = "City",
            Country = "Country",
            PostCode = "PostCode",
            StateProvince = "StateProvince",
            LocationTypeCategory = LocationTypeCategory.NotSet,
            Latitude = 0,
            Longitude = 0,
            LocationType = LocationType.Postal
        };
        var existingLocationId = CreateLocation(expected);

        var expectedSchedule = new ScheduleDto()
        {
            AttendingType = AttendingType.InPerson.ToString(),
            Freq = FrequencyType.WEEKLY,
            ByDay = "TU"
        };

        var expectedServiceAtLocation = new ServiceAtLocationDto
        {
            LocationId = existingLocationId,
            Schedules = new List<ScheduleDto>
            {
                expectedSchedule
            }
        };

        _serviceChange.ServiceAtLocations.Clear();
        _serviceChange.ServiceAtLocations.Add(expectedServiceAtLocation);

        //Act
        var result = await _updateServiceCommandHandler.Handle(_updateServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_serviceChange.Id);

        var actualService = GetService(_serviceChange.Name);
        actualService.Should().NotBeNull();
        actualService!.Locations.Should().HaveCount(1);

        var actualEntity = TestDbContext.Locations.SingleOrDefault(s => s.Name == expected.Name);
        actualEntity.Should().NotBeNull();
        actualEntity.Should().BeEquivalentTo(expected, options =>
            options.Excluding((IMemberInfo info) => info.Name.Contains("Id"))
                .Excluding((IMemberInfo info) => info.Name.Contains("Distance")));

        //Delete wont cascade delete Locations, so existing will be left behind
        var detachedEntity = TestDbContext.Locations.SingleOrDefault(s => s.Name == location.Name);
        detachedEntity.Should().NotBeNull();
        detachedEntity.Should().BeEquivalentTo(location, options =>
            options.Excluding((IMemberInfo info) => info.Name.Contains("Id"))
                .Excluding((IMemberInfo info) => info.Name.Contains("Distance")));

        // check old ServiceAtLocation is deleted and the new one is saved correctly
        TestDbContext.ServiceAtLocations.Should().HaveCount(1);
        TestDbContext.ServiceAtLocations.First().Id.Should().Be(2);
        TestDbContext.ServiceAtLocations.First().Should().BeEquivalentTo(expectedServiceAtLocation, options =>
            options.Excluding(info => info.Name.Contains("Id")));

        // check the old schedule is deleted and the new one is saved correctly
        TestDbContext.Schedules.Where(s => s.ServiceAtLocationId != null).Should().HaveCount(1);
        TestDbContext.Schedules.Single(s => s.ServiceAtLocationId != null).Id.Should().Be(4);
        TestDbContext.Schedules.Single(s => s.ServiceAtLocationId != null).Should().BeEquivalentTo(expectedSchedule,
            options =>
                options.Excluding(info => info.Name.Contains("Id")));
    }

    [Fact]
    public async Task ThenUpdateServiceAddAndDeleteTaxonomies()
    {
        //Arrange
        await Setup();

        var newTaxonomy = TestDbContext.Taxonomies.First(t => t.Name == "Sports and recreation");

        var taxonomy = _serviceDto.Taxonomies.ElementAt(0);
        _serviceChange.TaxonomyIds.Clear();
        _serviceChange.TaxonomyIds.Add(newTaxonomy.Id);

        //Act
        var result = await _updateServiceCommandHandler.Handle(_updateServiceCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_serviceChange.Id);

        var actualService = GetService(_serviceChange.Name);
        actualService.Should().NotBeNull();
        actualService!.Taxonomies.Count.Should().Be(1);
        actualService.Taxonomies.Select(t => t.Id).Should().BeEquivalentTo(new[] { newTaxonomy.Id });

        // Delete wont cascade delete Taxonomies, so existing will be left behind
        var detachedEntity = TestDbContext.Taxonomies.SingleOrDefault(s => s.Name == taxonomy.Name);
        detachedEntity.Should().NotBeNull();
        detachedEntity.Should().BeEquivalentTo(taxonomy, options =>
            options.Excluding((IMemberInfo info) => info.Name.Contains("Id"))
                .Excluding((IMemberInfo info) => info.Name.Contains("Distance")));
    }
}