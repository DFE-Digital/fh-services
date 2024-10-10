using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Services;

public class WhenUsingOrganisationAdminClientService : BaseClientService
{
    private readonly ILogger<ServiceDirectoryClient> _mockLogger;

    public WhenUsingOrganisationAdminClientService()
    {
        _mockLogger = Substitute.For<ILogger<ServiceDirectoryClient>>();
    }

    [Fact]
    public async Task ThenGetListOrganisations()
    {
        //Arrange
        var list = new List<OrganisationDto>
        {
            new()
            {
                Id = OrganisationId,
                AdminAreaCode = "E1234",
                Description = "Unit Test County Council",
                Name = "Unit Test County Council",
                Uri = new Uri("https://www.unittest.gov.uk/").ToString(),
                Url = "https://www.unittest.gov.uk/",
                OrganisationType = OrganisationType.LA
            }
        };

        var json = JsonConvert.SerializeObject(list);
        var mockClient = GetMockClient(json);
        var organisationAdminClientService =
            new ServiceDirectoryClient(mockClient, Substitute.For<ICacheService>(), _mockLogger);

        //Act
        var result = await organisationAdminClientService.GetCachedLaOrganisations();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(list);
    }

    [Fact]
    public async Task ThenGetOrganisationById()
    {
        //Arrange
        var organisation = GetTestCountyCouncilDto();
        var json = JsonConvert.SerializeObject(organisation);
        var mockClient = GetMockClient(json);
        var organisationAdminClientService =
            new ServiceDirectoryClient(mockClient, Substitute.For<ICacheService>(), _mockLogger);

        //Act
        var result = await organisationAdminClientService.GetOrganisationById(OrganisationId);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(organisation);
    }

    [Fact]
    public async Task ThenCreateOrganisation()
    {
        //Arrange
        var organisation = GetTestCountyCouncilDto();
        var mockClient = GetMockClient(organisation.Id.ToString());
        var serviceDirectoryClient =
            new ServiceDirectoryClient(mockClient, Substitute.For<ICacheService>(), _mockLogger);

        //Act
        var result = await serviceDirectoryClient.CreateOrganisation(organisation);

        //Assert
        result.SuccessResult.Should().BeGreaterThan(0);
        result.SuccessResult.Should().Be(organisation.Id);
    }

    [Fact]
    public async Task ThenUpdateOrganisation()
    {
        //Arrange
        var organisation = GetTestCountyCouncilDto();
        var mockClient = GetMockClient(organisation.Id.ToString());
        var serviceDirectoryClient =
            new ServiceDirectoryClient(mockClient, Substitute.For<ICacheService>(), _mockLogger);

        //Act
        var result = await serviceDirectoryClient.UpdateOrganisation(organisation);

        //Assert
        result.Should().BeGreaterThan(0);
        result.Should().Be(organisation.Id);
    }
}