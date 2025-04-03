using FamilyHubs.Referral.Core.ApiClients;
using FamilyHubs.Referral.Core.Models;
using FamilyHubs.ReferralService.Shared.Models;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FluentAssertions;
using System.Text;
using System.Text.Json;
using FamilyHubs.ReferralUi.UnitTests.Helpers;

namespace FamilyHubs.ReferralUi.UnitTests.Core.ApiClients;

public class WhenUsingOrganisationClientService
{
    [Fact]
    public async Task ThenGetCategoryList()
    {
        //Arrange
        var listTaxonomies = ClientHelper.GetTaxonomies();
        var expectedPaginatedList = new PaginatedList<TaxonomyDto>(listTaxonomies, listTaxonomies.Count, 1, listTaxonomies.Count);
        var jsonString = JsonSerializer.Serialize(expectedPaginatedList);
        
        var httpClient = TestHelpers.GetMockClient(jsonString);

        var organisationClientService = new OrganisationClientService(httpClient);

        //Act
        var result = await organisationClientService.GetCategories();

        //Assert
        result.Count.Should().Be(6);
        result[0].Value.Count.Should().Be(7); 
    }

    [Fact]
    public async Task ThenGetLocalOffers()
    {
        //Arrange
        var listServices = new List<ServiceDto> { ClientHelper.GetTestCountyCouncilServicesDto() };
        var expectedPaginatedList = new PaginatedList<ServiceDto>(listServices, listServices.Count, 1, listServices.Count);
        var jsonString = JsonSerializer.Serialize(expectedPaginatedList);
        var httpClient = TestHelpers.GetMockClient(jsonString);

        var organisationClientService = new OrganisationClientService(httpClient);

        var filter = new LocalOfferFilter
        { 
            Status = "active",
            ServiceDeliveries = AttendingType.Online.ToString(),
            IsPaidFor = true,
            TaxonomyIds = "1,2",
            LanguageCode = "en",
            CanFamilyChooseLocation = true,
            DistrictCode = "ABC"
        };

        //Act
        var (result, response) = await organisationClientService.GetLocalOffers(filter);

        //Assert
        result.Items.Count.Should().Be(1);
        result.Items[0].Should().BeEquivalentTo(expectedPaginatedList.Items[0]);
        response.Should().NotBeNull();
    }

    [Fact]
    public async Task ThenGetLocalOfferById()
    {
        //Arrange
        var expectedService = ClientHelper.GetTestCountyCouncilServicesDto();
        var jsonString = JsonSerializer.Serialize(expectedService);
        var httpClient = TestHelpers.GetMockClient(jsonString);

        var organisationClientService = new OrganisationClientService(httpClient);

        //Act
        var result = await organisationClientService.GetLocalOfferById(expectedService.Id.ToString());

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedService);
    }

    [Fact]
    public async Task GetOrganisationDtobyId()
    {
        //Arrange
        var expectedOrganisation = ClientHelper.GetTestCountyCouncilWithoutAnyServices();
        var jsonString = JsonSerializer.Serialize(expectedOrganisation);
        var httpClient = TestHelpers.GetMockClient(jsonString);

        var organisationClientService = new OrganisationClientService(httpClient);

        //Act
        var result = await organisationClientService.GetOrganisationDtoByIdAsync(expectedOrganisation.Id);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedOrganisation);
    }

    [Fact]
    public void ThenCreateAddAgeToUrl()
    {
        //Arrange
        const string expected = "&givenAge=18";
        var organisationClientService = new OrganisationClientService(new HttpClient());
        var url = new StringBuilder();

        //Act 
        OrganisationClientService.AddAgeToUrl(url, 18);
        var result = url.ToString();

        //Assert
        result.Trim().Should().Be(expected.Trim());
    }

    [Fact]
    public void ThenAddTextToUrl()
    {
        //Arrange
        const string expected = "&text=Test";
        var organisationClientService = new OrganisationClientService(new HttpClient());
        var url = new StringBuilder();

        //Act 
        OrganisationClientService.AddTextToUrl(url,"Test");
        var result = url.ToString();

        //Assert
        result.Trim().Should().Be(expected.Trim());
    }
}
