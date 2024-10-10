using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Services;

public class WhenUsingLocalOfferClientService : BaseClientService
{
    private readonly ILogger<ServiceDirectoryClient> _mockLogger;

    public WhenUsingLocalOfferClientService()
    {
        _mockLogger = Substitute.For<ILogger<ServiceDirectoryClient>>();
    }

    [Fact]
    public async Task ThenGetLocalOfferById()
    {
        //Arrange
        var service = GetTestCountyCouncilServicesDto(OrganisationId);
        var json = JsonConvert.SerializeObject(service);
        var mockClient = GetMockClient(json);
        var localOfferClientService = new ServiceDirectoryClient(mockClient, Substitute.For<ICacheService>(), _mockLogger);

        //Act
        var result = await localOfferClientService.GetServiceById(service.Id);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(service);
    }

    [Fact]
    public async Task GetServiceSummariesTest()
    {
        //Arrange
        var paginatedList = new PaginatedList<ServiceNameDto>
        {
            Items = new List<ServiceNameDto> { new()  {Id = 123, Name = "TestService"} },
            PageNumber = 1,
            TotalPages = 1,
            TotalCount = 1
        };

        var json = JsonConvert.SerializeObject(paginatedList);
        var mockClient = GetMockClient(json);
        var localOfferClientService = new ServiceDirectoryClient(mockClient, Substitute.For<ICacheService>(), _mockLogger);

        //Act
        var result = await localOfferClientService.GetServiceSummaries(123);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(paginatedList);
    }
}
