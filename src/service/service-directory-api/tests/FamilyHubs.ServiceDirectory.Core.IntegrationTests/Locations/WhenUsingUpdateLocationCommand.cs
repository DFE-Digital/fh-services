using FamilyHubs.ServiceDirectory.Core.Commands.Locations.UpdateLocation;
using FamilyHubs.ServiceDirectory.Data.Entities;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.ServiceDirectory.Core.IntegrationTests.Locations;

public class WhenUsingUpdateLocationCommand : DataIntegrationTestBase
{
    private readonly ILogger<UpdateLocationCommandHandler> _updateLogger = GetLogger<UpdateLocationCommandHandler>();

    private readonly LocationDto _testLocation;

    public WhenUsingUpdateLocationCommand()
    {
        _testLocation = GetTestLocation();
        _testLocation.Id = CreateLocation(_testLocation);
        _testLocation.Name = "Unit Test Update Service Name";
        _testLocation.Description = "Unit Test Update Service Name";
    }

    private Location? GetLocation() => TestDbContext.Locations
        .SingleOrDefault(s => s.Name == _testLocation.Name);

    private Contact? GetContact(ContactDto contactDto) => TestDbContext.Contacts
        .SingleOrDefault(s => s.Name == contactDto.Name);

    private Schedule? GetSchedule(ScheduleDto scheduleDto) => TestDbContext
        .Schedules.SingleOrDefault(s => s.ByDay == scheduleDto.ByDay);

    [Fact]
    public async Task ThenUpdateLocationOnly()
    {
        //Arrange
        var updateCommand = new UpdateLocationCommand(_testLocation.Id, _testLocation);
        var updateHandler = new UpdateLocationCommandHandler(TestDbContext, Mapper, _updateLogger);

        //Act
        var result = await updateHandler.Handle(updateCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_testLocation.Id);

        var actualLocation = GetLocation();

        actualLocation.Should().NotBeNull();
        actualLocation!.Description.Should().Be(_testLocation.Description);
    }

    [Fact]
    public async Task ThenUpdateLocationAddAndDeleteContacts()
    {
        //Arrange
        var existingItem = _testLocation.Contacts.ElementAt(0);
        var contact = new ContactDto
        {
            Id = 0,
            Name = "New Contact",
            Telephone = "New Telephone"
        };

        _testLocation.Contacts.Clear();
        _testLocation.Contacts.Add(contact);

        var updateCommand = new UpdateLocationCommand(_testLocation.Id, _testLocation);
        var updateHandler = new UpdateLocationCommandHandler(TestDbContext, Mapper, _updateLogger);

        //Act
        var result = await updateHandler.Handle(updateCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_testLocation.Id);

        var actualLocation = GetLocation();

        actualLocation.Should().NotBeNull();
        actualLocation!.Contacts.Count.Should().Be(1);

        var actualContact = GetContact(contact);
        actualContact.Should().NotBeNull();
        actualContact.Should().BeEquivalentTo(contact, options =>
            options.Excluding((IMemberInfo info) => info.Name.Contains("Id"))
                .Excluding((IMemberInfo info) => info.Name.Contains("Distance")));

        var unexpectedContactCount = TestDbContext.Contacts.Count(c => c.Id == existingItem.Id);
        unexpectedContactCount.Should().Be(0);
    }

    [Fact]
    public async Task ThenUpdateLocationWithUpdatedContacts()
    {
        //Arrange
        var contact = _testLocation.Contacts.ElementAt(0);
        contact.Name = "Updated Name";
        contact.Email = "Updated Email";
        contact.Telephone = "Updated Telephone";

        var updateCommand = new UpdateLocationCommand(_testLocation.Id, _testLocation);
        var updateHandler = new UpdateLocationCommandHandler(TestDbContext, Mapper, _updateLogger);

        //Act
        var result = await updateHandler.Handle(updateCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_testLocation.Id);

        var actualLocation = GetLocation();
        actualLocation.Should().NotBeNull();
        actualLocation!.Contacts.Count.Should().Be(1);

        var actualContact = GetContact(contact);
        actualContact.Should().NotBeNull();
        actualContact.Should().BeEquivalentTo(contact, options =>
            options.Excluding((IMemberInfo info) => info.Name.Contains("Id")));
    }

    [Fact]
    public async Task ThenUpdateLocationAddAndDeleteSchedules()
    {
        //Arrange
        var existingItem = _testLocation.Schedules.ElementAt(0);
        var expected = new ScheduleDto
        {
            ValidFrom = DateTime.UtcNow,
            ValidTo = DateTime.UtcNow,
            ByDay = "New ByDay",
            ByMonthDay = "New ByMonthDay"
        };

        _testLocation.Schedules.Clear();
        _testLocation.Schedules.Add(expected);

        var updateCommand = new UpdateLocationCommand(_testLocation.Id, _testLocation);
        var updateHandler = new UpdateLocationCommandHandler(TestDbContext, Mapper, _updateLogger);

        //Act
        var result = await updateHandler.Handle(updateCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_testLocation.Id);

        var actualLocation = GetLocation();
        actualLocation.Should().NotBeNull();
        actualLocation!.Schedules.Count.Should().Be(1);

        var actualSchedule = GetSchedule(expected);
        actualSchedule.Should().NotBeNull();
        actualSchedule.Should().BeEquivalentTo(expected, options =>
            options.Excluding(info => info.Name.Contains("Id"))
                .Excluding(info => info.Name.Contains("Distance")));

        var unexpectedScheduleCount = TestDbContext.Schedules.Count(s => s.Id == existingItem.Id);
        unexpectedScheduleCount.Should().Be(0);
    }

    [Fact]
    public async Task ThenUpdateLocationUpdatedSchedules()
    {
        //Arrange
        var expected = _testLocation.Schedules.ElementAt(0);
        expected.ByDay = "Updated ByDay";
        expected.ByMonthDay = "Updated ByMonthDay";

        var updateCommand = new UpdateLocationCommand(_testLocation.Id, _testLocation);
        var updateHandler = new UpdateLocationCommandHandler(TestDbContext, Mapper, _updateLogger);

        //Act
        var result = await updateHandler.Handle(updateCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);
        result.Should().Be(_testLocation.Id);

        var actualLocation = GetLocation();
        actualLocation.Should().NotBeNull();
        actualLocation!.Schedules.Count.Should().Be(1);

        var actualSchedule = GetSchedule(expected);
        actualSchedule.Should().NotBeNull();
        actualSchedule.Should().BeEquivalentTo(expected, options =>
            options.Excluding(info => info.Name.Contains("Id")));
    }
}