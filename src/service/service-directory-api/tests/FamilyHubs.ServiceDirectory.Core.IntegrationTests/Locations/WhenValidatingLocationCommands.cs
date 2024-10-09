using FamilyHubs.ServiceDirectory.Core.Commands.Locations.CreateLocation;
using FamilyHubs.ServiceDirectory.Core.Commands.Locations.UpdateLocation;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FluentAssertions;

namespace FamilyHubs.ServiceDirectory.Core.IntegrationTests.Locations;

public class WhenValidatingLocationCommands
{
    private static readonly string LocationNameWithValidLength = new('A', 254);
    private static readonly string LocationNameWithInvalidLength = new('A', 256);

    private readonly LocationDto _testLocation =
        TestDataProvider.GetTestCountyCouncilServicesDto2(1).Locations.ElementAt(0);

    [Fact]
    public void ThenShouldCreateLocationCommandNotErrorWhenModelIsValid()
    {
        //Arrange
        var validator = new CreateLocationCommandValidator();
        var testModel = new CreateLocationCommand(_testLocation);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().Be(0);
    }

    [Fact]
    public void ThenShouldUpdateLocationCommandNotErrorWhenModelIsValid()
    {
        //Arrange
        _testLocation.Id = 1;
        var validator = new UpdateLocationCommandValidator();
        var testModel = new UpdateLocationCommand(_testLocation.Id, _testLocation);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().Be(0);
    }

    [Fact]
    public void ThenShouldCreateLocationCommandNotErrorWhenNameIsLessThen255Char()
    {
        //Arrange
        _testLocation.Name = LocationNameWithValidLength;
        var validator = new CreateLocationCommandValidator();
        var testModel = new CreateLocationCommand(_testLocation);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().Be(0);
    }

    [Fact]
    public void ThenShouldUpdateLocationCommandNotErrorWhenNameIsLessThen255Char()
    {
        //Arrange
        _testLocation.Name = LocationNameWithValidLength;
        _testLocation.Id = 1;
        var validator = new UpdateLocationCommandValidator();
        var testModel = new UpdateLocationCommand(_testLocation.Id, _testLocation);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().Be(0);
    }

    [Fact]
    public void ThenShouldCreateLocationCommandHasErrorsWhenNameIsGreaterThen255Char()
    {
        //Arrange
        _testLocation.Name = LocationNameWithInvalidLength;
        var validator = new CreateLocationCommandValidator();
        var testModel = new CreateLocationCommand(_testLocation);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void ThenShouldUpdateLocationCommandHasErrorsWhenNameIsGreaterThen255Char()
    {
        //Arrange
        _testLocation.Name = LocationNameWithInvalidLength;
        _testLocation.Id = 0;
        var validator = new UpdateLocationCommandValidator();
        var testModel = new UpdateLocationCommand(_testLocation.Id, _testLocation);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData(default)]
    [InlineData("http://wwww.google.co.uk")]
    [InlineData("http://wwww.google.com")]
    [InlineData("https://wwww.google.com")]
    public void ThenShouldValidateLocationContactUrlWhenCreatingLocation_ShouldReturnNoErrors(string? url)
    {
        //Arrange
        foreach (var item in _testLocation.Contacts)
        {
            item.Url = url;
        }

        var validator = new CreateLocationCommandValidator();
        var testModel = new CreateLocationCommand(_testLocation);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().Be(0);
    }

    [Theory]
    [InlineData("")]
    [InlineData("someurl")]
    [InlineData("http://someurl")]
    [InlineData("https://someurl")]
    public void ThenShouldValidateLocationContactUrlWhenCreatingLocation_ShouldReturnErrors(string url)
    {
        //Arrange
        foreach (var item in _testLocation.Contacts)
        {
            item.Url = url;
        }

        var validator = new CreateLocationCommandValidator();
        var testModel = new CreateLocationCommand(_testLocation);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData(default)]
    [InlineData("http://wwww.google.co.uk")]
    [InlineData("http://wwww.google.com")]
    [InlineData("https://wwww.google.com")]
    public void ThenShouldValidateLocationContactUrlWhenUpdatingLocation_ShouldReturnNoErrors(string? url)
    {
        //Arrange
        _testLocation.Id = 1;

        foreach (var item in _testLocation.Contacts)
        {
            item.Url = url;
        }

        var validator = new UpdateLocationCommandValidator();
        var testModel = new UpdateLocationCommand(_testLocation.Id, _testLocation);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().Be(0);
    }

    [Theory]
    [InlineData("")]
    [InlineData("someurl")]
    [InlineData("http:/someurl")]
    [InlineData("https//someurl")]
    public void ThenShouldValidateLocationContactUrlWhenUpdatingLocation_ShouldReturnErrors(string url)
    {
        //Arrange
        _testLocation.Id = 0;

        foreach (var item in _testLocation.Contacts)
        {
            item.Url = url;
        }

        var validator = new UpdateLocationCommandValidator();
        var testModel = new UpdateLocationCommand(_testLocation.Id, _testLocation);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}
