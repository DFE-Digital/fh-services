using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.ReferralService.Shared.Models;
using FamilyHubs.RequestForSupport.Core.ApiClients;
using FluentAssertions;
using System.Text.Json;
using FamilyHubs.RequestForSupport.UnitTests.Helpers;

namespace FamilyHubs.RequestForSupport.UnitTests;

public class WhenUsingReferralClientService
{ 

    [Fact]
    public async Task ThenGetRequestsByLaProfessional()
    {
        var listReferral = new List<ReferralDto> { GetReferralDto() };
        var expectedList = new PaginatedList<ReferralDto>(listReferral, 1, 1, 10);
        var jsonString = JsonSerializer.Serialize(expectedList);
        var httpClient = TestHelpers.GetMockClient(jsonString);
        var referralClientService = new ReferralClientService(httpClient);

        // Act
        var result = await referralClientService
            .GetRequestsByLaProfessional(accountId:"123", null, null);

        // Assert
        result.Should().BeEquivalentTo(expectedList);
    }

    [Fact]
    public async Task ThenGetRequestsForConnectionByOrganisationId()
    {
        var listReferral = new List<ReferralDto> { GetReferralDto() };
        var expectedList = new PaginatedList<ReferralDto>(listReferral, 1, 1, 10);
        var jsonString = JsonSerializer.Serialize(expectedList);
        var httpClient = TestHelpers.GetMockClient(jsonString);
        var referralClientService = new ReferralClientService(httpClient);

        // Act
        var result = await referralClientService
            .GetRequestsForConnectionByOrganisationId(organisationId:"123", null, null);

        // Assert
        result.Should().BeEquivalentTo(expectedList);
    }

    [Fact]
    public async Task ThenGetReferralById()
    {
        var expectedReferral = GetReferralDto();
        var jsonString = JsonSerializer.Serialize(expectedReferral);
        var httpClient = TestHelpers.GetMockClient(jsonString);
        var referralClientService = new ReferralClientService(httpClient);

        // Act
        var result = await referralClientService.GetReferralById(referralId: 1L);

        // Assert
        result.Should().BeEquivalentTo(expectedReferral, options => 
            options.Excluding(x => x.ReasonForSupport).Excluding(x => x.EngageWithFamily));
    }


    [Theory]
    [InlineData(ReferralStatus.Accepted)]
    [InlineData(ReferralStatus.Declined)]
    public async Task ThenUpdateReferralStatus(ReferralStatus expectedReferralStatus)
    {
        var jsonString = JsonSerializer.Serialize(expectedReferralStatus.ToString());
        var httpClient = TestHelpers.GetMockClient(jsonString);
        var referralClientService = new ReferralClientService(httpClient);

        // Act
        var result = await referralClientService.UpdateReferralStatus(referralId:1L, expectedReferralStatus);

        // Assert
        result.Replace("\"","").Should().Be(expectedReferralStatus.ToString());
      
    }

    private static ReferralDto GetReferralDto()
    {
        return new ReferralDto
        {
            Id = 2,
            ReasonForSupport = "Reason For Support",
            EngageWithFamily = "Engage With Family",
            RecipientDto = new RecipientDto
            {
                Id = 2,
                Name = "Joe Blogs",
                Email = "JoeBlog@email.com",
                Telephone = "078123456",
                TextPhone = "078123456",
                AddressLine1 = "Address Line 1",
                AddressLine2 = "Address Line 2",
                TownOrCity = "Town or City",
                County = "County",
                PostCode = "B30 2TV"
            },
            ReferralUserAccountDto = new UserAccountDto
            {
                Id = 2,
                EmailAddress = "Bob.Referrer@email.com",
                Name = "Bob Referrer",
                PhoneNumber = "1234567890",
                Team = "Team",
                UserAccountRoles = new List<UserAccountRoleDto>
                {
                        new()
                        {
                            UserAccount = new UserAccountDto
                            {
                                EmailAddress = "Bob.Referrer@email.com",
                            },
                            Role = new RoleDto
                            {
                                Name = "VcsProfessional"
                            }
                        }
                    },
                ServiceUserAccounts = new List<UserAccountServiceDto>(),
                OrganisationUserAccounts = new List<UserAccountOrganisationDto>(),
            },
            Status = new ReferralStatusDto
            {
                Id = 1,
                Name = "New",
                SortOrder = 0
            },
            ReferralServiceDto = new ReferralServiceDto
            {
                Id = 2,
                Name = "Service",
                Description = "Service Description",
                Url = "www.service.com",
                OrganisationDto = new OrganisationDto
                {
                    Id = 2,
                    ReferralServiceId = 2,
                    Name = "Organisation",
                    Description = "Organisation Description",
                }
            }

        };
    }
}
