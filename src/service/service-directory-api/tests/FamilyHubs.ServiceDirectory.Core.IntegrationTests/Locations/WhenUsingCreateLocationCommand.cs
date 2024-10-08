using FamilyHubs.ServiceDirectory.Core.Commands.Locations.CreateLocation;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.ServiceDirectory.Core.IntegrationTests.Locations;

public class WhenUsingCreateLocationCommand : DataIntegrationTestBase
{
    private readonly LocationDto _testLocation;

    public WhenUsingCreateLocationCommand()
    {
        _testLocation = GetTestLocation();
    }

    [Fact]
    public async Task ThenCreateLocation()
    {
        //Arrange
        var createLocationCommand = new CreateLocationCommand(_testLocation);
        var handler = new CreateLocationCommandHandler(TestDbContext, Mapper, GetLogger<CreateLocationCommandHandler>());

        //Act
        var result = await handler.Handle(createLocationCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);

        var query = TestDbContext.Locations;

        var actualLocation = await query.SingleOrDefaultAsync(o => o.Id == result);

        actualLocation.Should().NotBeNull();
        actualLocation.Should().BeEquivalentTo(_testLocation, options =>
            options.Excluding((IMemberInfo info) => info.Name.Contains("Id"))
                .Excluding((IMemberInfo info) => info.Name.Contains("Distance")));
    }

    [Fact]
    public async Task ThenCreateLocationWithoutAnyChildEntities()
    {
        _testLocation.Contacts.Clear();
        _testLocation.AccessibilityForDisabilities.Clear();
        _testLocation.Schedules.Clear();
        //Arrange
        var createLocationCommand = new CreateLocationCommand(_testLocation);
        var handler = new CreateLocationCommandHandler(TestDbContext, Mapper, GetLogger<CreateLocationCommandHandler>());

        //Act
        var result = await handler.Handle(createLocationCommand, CancellationToken.None);

        //Assert
        result.Should().NotBe(0);

        var query = TestDbContext.Locations
            .Include(location => location.Contacts)
            .Include(location => location.Schedules)
            .Include(location => location.AccessibilityForDisabilities);

        var actualLocation = await query.SingleOrDefaultAsync(o => o.Id == result);

        actualLocation.Should().NotBeNull();
        actualLocation!.Contacts.Count.Should().Be(0);
        actualLocation.Schedules.Count.Should().Be(0);
        actualLocation.AccessibilityForDisabilities.Count.Should().Be(0);

        actualLocation.Should().BeEquivalentTo(_testLocation, options =>
            options.Excluding((IMemberInfo info) => info.Name.Contains("Id"))
                .Excluding((IMemberInfo info) => info.Name.Contains("Distance")));
    }
}