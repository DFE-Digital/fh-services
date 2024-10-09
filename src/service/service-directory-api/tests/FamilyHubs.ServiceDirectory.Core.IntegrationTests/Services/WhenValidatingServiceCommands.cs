using AutoMapper;
using AutoMapper.EquivalencyExpression;
using FamilyHubs.ServiceDirectory.Core.Commands.Services.CreateService;
using FamilyHubs.ServiceDirectory.Core.Commands.Services.DeleteService;
using FamilyHubs.ServiceDirectory.Core.Commands.Services.UpdateService;
using FluentAssertions;

namespace FamilyHubs.ServiceDirectory.Core.IntegrationTests.Services;

public class WhenValidatingServiceCommands
{
    private static readonly string[] ConfigAction = ["CreatedBy", "Created", "LastModified", "LastModified"];

    private IMapper Mapper { get; } = new Mapper(new MapperConfiguration(cfg =>
    {
        var auditProperties = ConfigAction;
        cfg.AddProfile<AutoMappingProfiles>();
        cfg.AddCollectionMappers();
        cfg.ShouldMapProperty = propertyInfo => !auditProperties.Contains(propertyInfo.Name);
    }));

    [Fact]
    public void ThenShouldCreateServiceCommandNotErrorWhenModelIsValid()
    {
        //Arrange
        var testService = TestDataProvider.GetTestCountyCouncilServicesChangeDto2(Mapper, Random.Shared.Next());
        var validator = new CreateServiceCommandValidator();
        var testModel = new CreateServiceCommand(testService);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().Be(0);
    }

    [Fact]
    public void ThenShouldUpdateServiceCommandNotErrorWhenModelIsValid()
    {
        //Arrange
        var testService = TestDataProvider.GetTestCountyCouncilServicesChangeDto2(Mapper, Random.Shared.Next());
        testService.Id = 1;
        var validator = new UpdateServiceCommandValidator();
        var testModel = new UpdateServiceCommand(testService.Id, testService);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().Be(0);
    }

    [Fact]
    public void ThenShouldCreateServiceCommandNotErrorWhenNameIsLessThen255Char()
    {
        //Arrange
        var testService = TestDataProvider.GetTestCountyCouncilServicesChangeDto2(Mapper, Random.Shared.Next());
        testService.Name = string.Join(string.Empty, Enumerable.Range(0, 254).Select(_ => "a"));
        var validator = new CreateServiceCommandValidator();
        var testModel = new CreateServiceCommand(testService);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().Be(0);
    }

    [Fact]
    public void ThenShouldUpdateServiceCommandNotErrorWhenNameIsLessThen255Char()
    {
        //Arrange
        var testService = TestDataProvider.GetTestCountyCouncilServicesChangeDto2(Mapper, Random.Shared.Next());
        testService.Id = Random.Shared.Next();
        testService.Name = string.Join(string.Empty, Enumerable.Range(0, 254).Select(_ => "a"));
        var validator = new UpdateServiceCommandValidator();
        var testModel = new UpdateServiceCommand(testService.Id, testService);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().Be(0);
    }

    [Fact]
    public void ThenShouldCreateServiceCommandHasErrorsWhenNameIsGreaterThen255Char()
    {
        //Arrange
        var testService = TestDataProvider.GetTestCountyCouncilServicesChangeDto2(Mapper, Random.Shared.Next());
        testService.Name = string.Join(string.Empty, Enumerable.Range(0, 256).Select(_ => "a"));
        var validator = new CreateServiceCommandValidator();
        var testModel = new CreateServiceCommand(testService);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void ThenShouldUpdateServiceCommandHasErrorsWhenNameIsGreaterThen255Char()
    {
        //Arrange
        var testService = TestDataProvider.GetTestCountyCouncilServicesChangeDto2(Mapper, Random.Shared.Next());
        testService.Name = string.Join(string.Empty, Enumerable.Range(0, 256).Select(_ => "a"));
        var validator = new UpdateServiceCommandValidator();
        var testModel = new UpdateServiceCommand(testService.Id, testService);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void ThenShouldDeleteServiceByIdCommandNotErrorWhenModelIsValid()
    {
        //Arrange
        var validator = new DeleteServiceByIdCommandValidator();
        var testModel = new DeleteServiceByIdCommand(1);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().Be(0);
    }

    [Theory]
    [InlineData(default)]
    [InlineData("http://wwww.google.co.uk")]
    [InlineData("http://wwww.google.com")]
    [InlineData("https://wwww.google.com")]
    public void ThenShouldValidateContactUrlWhenCreatingService_ShouldReturnNoErrors(string? url)
    {
        //Arrange
        var testService = TestDataProvider.GetTestCountyCouncilServicesChangeDto2(Mapper, Random.Shared.Next());
        testService.Id = 0;
        foreach (var item in testService.Contacts)
        {
            item.Url = url;
        }

        var validator = new CreateServiceCommandValidator();
        var testModel = new CreateServiceCommand(testService);

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
    public void ThenShouldValidateContactUrlWhenCreatingService_ShouldReturnErrors(string url)
    {
        //Arrange
        var testService = TestDataProvider.GetTestCountyCouncilServicesChangeDto2(Mapper, Random.Shared.Next());
        testService.Id = 1;
        foreach (var item in testService.Contacts)
        {
            item.Url = url;
        }

        var validator = new CreateServiceCommandValidator();
        var testModel = new CreateServiceCommand(testService);

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
    public void ThenShouldValidateContactUrlWhenUpdatingService_ShouldReturnNoErrors(string? url)
    {
        //Arrange
        var testService = TestDataProvider.GetTestCountyCouncilServicesChangeDto2(Mapper, Random.Shared.Next());
        testService.Id = 1;
        foreach (var item in testService.Contacts)
        {
            item.Url = url;
        }

        var validator = new UpdateServiceCommandValidator();
        var testModel = new UpdateServiceCommand(testService.Id, testService);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().Be(0);
    }

    [Theory]
    [InlineData("")]
    [InlineData("someurl")]
    [InlineData("http:/someurl.")]
    [InlineData("https//someurl.")]
    public void ThenShouldValidateContactUrlWhenUpdatingService_ShouldReturnErrors(string url)
    {
        //Arrange
        var testService = TestDataProvider.GetTestCountyCouncilServicesChangeDto2(Mapper, Random.Shared.Next());
        testService.Id = 1;
        foreach (var item in testService.Contacts)
        {
            item.Url = url;
        }

        var validator = new UpdateServiceCommandValidator();
        var testModel = new UpdateServiceCommand(testService.Id, testService);

        //Act
        var result = validator.Validate(testModel);

        //Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}