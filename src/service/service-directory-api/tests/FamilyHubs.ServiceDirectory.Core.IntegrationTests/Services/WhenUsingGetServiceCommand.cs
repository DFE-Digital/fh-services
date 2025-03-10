using Ardalis.GuardClauses;
using FamilyHubs.ServiceDirectory.Core.Commands.Services.DeleteService;
using FamilyHubs.ServiceDirectory.Core.Queries.Services.GetServices;
using FamilyHubs.ServiceDirectory.Core.Queries.Services.GetServicesByOrganisationId;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FluentAssertions;

namespace FamilyHubs.ServiceDirectory.Core.IntegrationTests.Services;

public class WhenUsingGetServiceCommand : DataIntegrationTestBase
{
    [Fact]
    public async Task ThenGetService()
    {
        //Arrange
        await CreateOrganisationDetails();

        var command = new GetServicesCommandBuilder()
            .WithServiceType(ServiceType.InformationSharing)
            .WithServiceStatusType(ServiceStatusType.Active)
            .WithDistrictCode("XTEST")
            .WithPage(1, 10)
            .Build();

        var handler = new GetServicesCommandHandler(Configuration, TestDbContext, Mapper);

        //Act
        var results = await handler.Handle(command, CancellationToken.None);

        //Assert
        results.Should().NotBeNull();
        results.Items[0].Should().BeEquivalentTo(TestOrganisation.Services.ElementAt(0));
    }

    [Fact]
    public async Task ThenGetServicesByOrganisationId()
    {
        //Arrange
        await CreateOrganisationDetails();

        var command = new GetServiceNamesCommand
        {
            OrganisationId = TestOrganisation.Id,
            PageNumber = 1,
            PageSize = 10,
            Order = SortOrder.ascending
        };
        var handler = new GetServiceNamesCommandHandler(TestDbContext, Mapper);

        //Act
        var results = await handler.Handle(command, CancellationToken.None);

        //Assert
        results.Should().NotBeNull();

        var expectedService = TestOrganisation.Services.ElementAt(0);
        var expectedServiceNameDto = new ServiceNameDto
        {
            Id = expectedService.Id,
            Name = expectedService.Name,
        };
        results.Items[0].Should().BeEquivalentTo(expectedServiceNameDto);
    }

    [Theory]
    [InlineData("[[0,2]]", 9)]
    [InlineData("[[3,5]]", 12)]
    [InlineData("[[6,11]]", 11)]
    [InlineData("[[12,15]]", 12)]
    [InlineData("[[16,18]]", 10)]
    [InlineData("[[19,127]]", 9)]
    [InlineData("[[0,2],[19,127]]", 15)]
    [InlineData("[[3,5],[16,18]]", 17)]
    [InlineData("[[6,11],[12,15]]", 14)]
    [InlineData("[[0,2],[6,11],[16,18]]", 20)]
    [InlineData("[[3,5],[12,15],[19,127]]", 20)]
    [InlineData(null, 26)]
    [InlineData("[[0, 2],[3, 5],[6, 11],[12, 15],[16, 18],[19, 127]]", 24)]
    public async Task ThenGetServicesByAge(string? ageRangeList, int expectedNumberOfServices)
    {
        //Arrange
        OrganisationDetailsDto organisationDetailsDto = TestDataProvider.GetTestCountyCouncilWithAgeRangeServices();
        await CreateOrganisationDetails(organisationDetailsDto);

        var command = new GetServicesCommandBuilder()
            .WithServiceType(ServiceType.FamilyExperience)
            .WithServiceStatusType(ServiceStatusType.Active)
            .WithDistrictCode("XTEST")
            .WithPage(1, organisationDetailsDto.Services.Count)
            .WithAgeRangeList(ageRangeList)
            .Build();

        var handler = new GetServicesCommandHandler(Configuration, TestDbContext, Mapper);

        //Act
        var results = await handler.Handle(command, CancellationToken.None);

        //Assert
        results.Should().NotBeNull();
        results.Items.Count.Should().Be(expectedNumberOfServices);
    }

    [Fact]
    public async Task ThenGetServiceThatArePaidFor()
    {
        //Arrange
        await CreateOrganisationDetails(TestDataProvider.GetTestCountyCouncilDto2());

        var command = new GetServicesCommandBuilder()
            .WithServiceType(ServiceType.InformationSharing)
            .WithServiceStatusType(ServiceStatusType.Active)
            .WithDistrictCode("XTEST")
            .WithPage(1, 10)
            .Build();

        var handler = new GetServicesCommandHandler(Configuration, TestDbContext, Mapper);

        //Act
        var results = await handler.Handle(command, CancellationToken.None);

        //Assert
        results.Should().NotBeNull();
        results.Items.Count.Should().Be(1);
    }

    [Fact]
    public async Task ThenGetServiceThatAreFree()
    {
        //Arrange
        await CreateOrganisationDetails(TestOrganisationFreeService);        

        var command = new GetServicesCommandBuilder()
            .WithServiceType(ServiceType.InformationSharing)
            .WithServiceStatusType(ServiceStatusType.Active)
            .WithDistrictCode("XTEST")
            .WithPage(1, 10)
            .Build();

        var handler = new GetServicesCommandHandler(Configuration, TestDbContext, Mapper);

        //Act
        var results = await handler.Handle(command, CancellationToken.None);
        
        //Assert
        results.Should().NotBeNull();
        results.Items[0].Should().BeEquivalentTo(TestOrganisationFreeService.Services.ElementAt(0));
    }
    
    [Fact]
    public async Task ThenDeleteService()
    {
        //Arrange
        await CreateOrganisationDetails();

        var command = new DeleteServiceByIdCommand(1);
        var handler = new DeleteServiceByIdCommandHandler(TestDbContext, GetLogger<DeleteServiceByIdCommandHandler>());

        //Act
        var results = await handler.Handle(command, CancellationToken.None);

        //Assert
        results.Should().BeTrue();
    }

    [Fact]
    public async Task ThenDeleteServiceThatDoesNotExist_ShouldThrowException()
    {
        //Arrange
        var command = new DeleteServiceByIdCommand(Random.Shared.Next());
        var handler = new DeleteServiceByIdCommandHandler(TestDbContext, GetLogger<DeleteServiceByIdCommandHandler>());

        // Act 
        // Assert
        await handler
            .Invoking(x => x.Handle(command, CancellationToken.None))
            .Should()
            .ThrowAsync<NotFoundException>();
    }
}