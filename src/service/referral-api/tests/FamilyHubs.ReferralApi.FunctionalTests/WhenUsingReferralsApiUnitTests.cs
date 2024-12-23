﻿using FamilyHubs.Referral.Data.Repository;
using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.ReferralService.Shared.Models;
using FluentAssertions;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using FamilyHubs.ReferralService.Shared.Dto.CreateUpdate;
using FamilyHubs.ReferralService.Shared.Dto.Metrics;

namespace FamilyHubs.Referral.FunctionalTests;

[Collection("Sequential")]
public class WhenUsingReferralsApiUnitTests : BaseWhenUsingOpenReferralApiUnitTests
{
#if UseJsonService
    const string JsonService = "{\"Id\":\"ba1cca90-b02a-4a0b-afa0-d8aed1083c0d\",\"Name\":\"Test County Council\",\"Description\":\"Test County Council\",\"Logo\":null,\"Uri\":\"https://www.test.gov.uk/\",\"Url\":\"https://www.test.gov.uk/\",\"Services\":[{\"Id\":\"c1b5dd80-7506-4424-9711-fe175fa13eb8\",\"Name\":\"Test Organisation for Children with Tracheostomies\",\"Description\":\"Test Organisation for for Children with Tracheostomies is a national self help group operating as a registered charity and is run by parents of children with a tracheostomy and by people who sympathise with the needs of such families. ACT as an organisation is non profit making, it links groups and individual members throughout Great Britain and Northern Ireland.\",\"Accreditations\":null,\"Assured_date\":null,\"Attending_access\":null,\"Attending_type\":null,\"Deliverable_type\":null,\"Status\":\"active\",\"Url\":\"www.testservice.com\",\"Email\":\"support@testservice.com\",\"Fees\":null,\"ServiceDelivery\":[{\"Id\":\"14db2aef-9292-4afc-be09-5f6f43765938\",\"ServiceDelivery\":2}],\"Eligibilities\":[{\"Id\":\"Test9109Children\",\"Eligibility\":\"\",\"Maximum_age\":0,\"Minimum_age\":13}],\"Contacts\":[{\"Id\":\"5eac5cb6-cc7e-444d-a29b-76ccb85be866\",\"Title\":\"Service\",\"Name\":\"\",\"Phones\":[{\"Id\":\"1568\",\"Number\":\"01827 65779\"}]}],\"Cost_options\":[],\"Languages\":[{\"Id\":\"442a06cd-aa14-4ea3-9f11-b45c1bc4861f\",\"Language\":\"English\"}],\"Service_areas\":[{\"Id\":\"68af19cd-bc81-4585-99a2-85a2b0d62a1d\",\"Service_area\":\"National\",\"Extent\":null,\"Uri\":\"http://statistics.data.gov.uk/id/statistical-geography/K02000001\"}],\"Service_at_locations\":[{\"Id\":\"Test1749\",\"Location\":{\"Id\":\"a878aadc-6097-4a0f-b3e1-77fd4511175d\",\"Name\":\"\",\"Description\":\"\",\"Latitude\":52.6312,\"Longitude\":-1.66526,\"Physical_addresses\":[{\"Id\":\"1076aaa8-f99d-4395-8e4f-c0dde8095085\",\"Address_1\":\"75 Sheepcote Lane\",\"City\":\", Stathe, Tamworth, Staffordshire, \",\"Postal_code\":\"B77 3JN\",\"Country\":\"England\",\"State_province\":null}]}}],\"Service_taxonomys\":[{\"Id\":\"Test9107\",\"Taxonomy\":{\"Id\":\"Test bccsource:Organisation\",\"Name\":\"Organisation\",\"Vocabulary\":\"Test BCC Data Sources\",\"Parent\":null}},{\"Id\":\"Test9108\",\"Taxonomy\":{\"Id\":\"Test bccprimaryservicetype:38\",\"Name\":\"Support\",\"Vocabulary\":\"Test BCC Primary Services\",\"Parent\":null}},{\"Id\":\"Test9109\",\"Taxonomy\":{\"Id\":\"Test bccagegroup:37\",\"Name\":\"Children\",\"Vocabulary\":\"Test BCC Age Groups\",\"Parent\":null}},{\"Id\":\"Test9110\",\"Taxonomy\":{\"Id\":\"Testbccusergroup:56\",\"Name\":\"Long Term Health Conditions\",\"Vocabulary\":\"Test BCC User Groups\",\"Parent\":null}}]}]}";
#endif

    [Fact]
    public async Task ThenReferralsByReferrerAreRetrieved()
    {
        var referrer = ReferralSeedData.SeedReferral().ElementAt(0).UserAccount.EmailAddress;

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(Client.BaseAddress + $"api/referrals/{referrer}?pageNumber=1&pageSize=10"),
        };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", new JwtSecurityTokenHandler().WriteToken(Token));

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var retVal = await JsonSerializer.DeserializeAsync<PaginatedList<ReferralDto>>(await response.Content.ReadAsStreamAsync(), options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        ArgumentNullException.ThrowIfNull(retVal);
        retVal.Should().NotBeNull();
        retVal.Items.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task ThenReferralsByReferrerIdAreRetrieved()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(Client.BaseAddress + "api/referralsByReferrer/5?pageNumber=1&pageSize=10"),
        };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", new JwtSecurityTokenHandler().WriteToken(Token));

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var retVal = await JsonSerializer.DeserializeAsync<PaginatedList<ReferralDto>>(await response.Content.ReadAsStreamAsync(), options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        ArgumentNullException.ThrowIfNull(retVal);
        retVal.Should().NotBeNull();
        retVal.Items.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task ThenTheOpenReferralIsCreated()
    {
        var command = new CreateReferralDto(
            new ReferralDto
            {
                Id = 3,
                ReasonForSupport = "Reason For Support",
                EngageWithFamily = "Engage With Family",
                RecipientDto = new RecipientDto
                {
                    Id = 3,
                    Name = "Fred Blogs",
                    Email = "FredBlog@email.com",
                    Telephone = "078123455",
                    TextPhone = "078123455",
                    AddressLine1 = "Unit Test Address Line 1",
                    AddressLine2 = "Unit Test Address Line 2",
                    TownOrCity = "Town or City",
                    County = "County",
                    PostCode = "B31 2TV"
                },
                ReferralUserAccountDto = new UserAccountDto
                {
                    Id = 2,
                    EmailAddress = "Bob.Referrer@email.com",
                    Name = "Bob Referrer",
                    PhoneNumber = "011 222 5555",
                    Team = "Social Work team North",
                    UserAccountRoles = new List<UserAccountRoleDto>(),
                    ServiceUserAccounts = new List<UserAccountServiceDto>(),
                    OrganisationUserAccounts = null,
                },
                Status = new ReferralStatusDto
                {
                    Id = 1,
                    Name = "New",
                    SortOrder = 0
                },
                ReferralServiceDto = new ReferralServiceDto
                {
                    Id = 3,
                    Name = "New Service",
                    Description = "Service Description",
                    Url = "www.service.com",
                    OrganisationDto = new OrganisationDto
                    {
                        Id = 3,
                        Name = "New Organisation",
                        Description = "Organisation Description",
                    }
                }
            },
            new ConnectionRequestsSentMetricDto(DateTimeOffset.UtcNow)
        );

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(Client.BaseAddress + "api/referrals"),
            Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json"),
        };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", new JwtSecurityTokenHandler().WriteToken(Token));

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var result = await JsonSerializer.DeserializeAsync<ReferralResponse>(await response.Content.ReadAsStreamAsync(), options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        ArgumentNullException.ThrowIfNull(result);
        result.Id.Should().Be(command.Referral.ReferralServiceDto.Id);
        result.ServiceName.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ThenReferralsByOrganisationIdAreRetrieved()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(Client.BaseAddress + "api/organisationreferrals/1?pageNumber=1&pageSize=10"),
        };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{new JwtSecurityTokenHandler().WriteToken(Vcstoken)}");

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var retVal = await JsonSerializer.DeserializeAsync<PaginatedList<ReferralDto>>(await response.Content.ReadAsStreamAsync(), options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        ArgumentNullException.ThrowIfNull(retVal);
        retVal.Should().NotBeNull();
        retVal.Items.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task ThenReferralByIdIsRetrievedByProfessional()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(Client.BaseAddress + "api/referral/1"),
        };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{new JwtSecurityTokenHandler().WriteToken(TokenForOrganisation1)}");

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var retVal = await JsonSerializer.DeserializeAsync<ReferralDto>(await response.Content.ReadAsStreamAsync(), options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        ArgumentNullException.ThrowIfNull(retVal);
        retVal.Should().NotBeNull();
    }

    [Fact]
    public async Task ThenReferralByIdIsRetrievedByVcsAdmin()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(Client.BaseAddress + "api/referral/1"),
        };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{new JwtSecurityTokenHandler().WriteToken(Vcstoken)}");

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var retVal = await JsonSerializer.DeserializeAsync<ReferralDto>(await response.Content.ReadAsStreamAsync(), options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        ArgumentNullException.ThrowIfNull(retVal);
        retVal.Should().NotBeNull();
    }

    [Fact]
    public async Task ThenReferralByIdIsForbidden()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(Client.BaseAddress + "api/referral/1"),
        };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{new JwtSecurityTokenHandler().WriteToken(Forbiddentoken)}");

        using var response = await Client.SendAsync(request);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task ThenTheOpenReferralIsUpdated()
    {
        var command = new ReferralDto
        {
            Id = 1,
            ReasonForSupport = "Reason For Support",
            EngageWithFamily = "Engage With Family",
            RecipientDto = new RecipientDto
            {
                Id = 1,
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
                PhoneNumber = "011 222 5555",
                Team = "Social Work team North",
                UserAccountRoles = new List<UserAccountRoleDto>(),
                ServiceUserAccounts = new List<UserAccountServiceDto>(),
                OrganisationUserAccounts = null,
            },
            Status = new ReferralStatusDto
            {
                Id = 1,
                Name = "New",
                SortOrder = 0
            },
            ReferralServiceDto = new ReferralServiceDto
            {
                Id = 1,
                Name = "Service",
                Description = "Service Description",
                Url = "www.service.com",
                OrganisationDto = new OrganisationDto
                {
                    Id = 1,
                    Name = "Organisation",
                    Description = "Organisation Description",
                }
            }

        };

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri(Client.BaseAddress + "api/referrals/1"),
            Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json"),
        };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", new JwtSecurityTokenHandler().WriteToken(Token));

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task ThenAcceptedReferralStatusIsSet()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(Client.BaseAddress + "api/status/1/Accepted")
        };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{new JwtSecurityTokenHandler().WriteToken(Vcstoken)}");

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var stringResult = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        stringResult.Should().Be("Accepted");
    }


    [Fact]
    public async Task ThenDeclinedReferralStatusIsSet()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(Client.BaseAddress + "api/status/1/Declined/Unable to help")
        };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{new JwtSecurityTokenHandler().WriteToken(Vcstoken)}");

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var stringResult = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        stringResult.Should().Be("Declined");
    }

    [Fact]
    public async Task ThenForbiddenReferralStatusIsReturned()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(Client.BaseAddress + "api/status/1/Declined/Unable to help")
        };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", new JwtSecurityTokenHandler().WriteToken(Token));

        using var response = await Client.SendAsync(request);

        response.StatusCode.Should().NotBe(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task ThenReferralStatusListIsRetrieved()
    {

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(Client.BaseAddress + "api/statuses"),
        };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", new JwtSecurityTokenHandler().WriteToken(Token));

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var retVal = await JsonSerializer.DeserializeAsync<List<ReferralStatusDto>>(await response.Content.ReadAsStreamAsync(), options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        ArgumentNullException.ThrowIfNull(retVal);
        retVal.Should().NotBeNull();
        retVal.Count.Should().Be(ReferralSeedData.SeedStatuses().Count);
    }
    [Fact]
    public async Task ThenReferralsByCompositeKeysAreRetrieved()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(Client.BaseAddress + "api/referral/compositekeys?serviceId=1&statusId=1&recipientId=1&referralId=1&pageNumber=1&pageSize=10"),
        };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", new JwtSecurityTokenHandler().WriteToken(Token));

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var retVal = await JsonSerializer.DeserializeAsync<PaginatedList<ReferralDto>>(await response.Content.ReadAsStreamAsync(), options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        ArgumentNullException.ThrowIfNull(retVal);
        retVal.Should().NotBeNull();
        retVal.Items.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 0)]
    public async Task Then_ReferralCount_ByServiceId_IsRetrieved(int serviceId, int expected)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(Client.BaseAddress + $"api/referral/count?serviceId={serviceId}"),
        };

        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{new JwtSecurityTokenHandler().WriteToken(TokenLaManager!)}");

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        int result = JsonSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync());

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("TestUser@email.com", default, "Email")]
    [InlineData("078873456", default, "Telephone")]
    [InlineData("078873456", default, "Textphone")]
    [InlineData("Test User", "B30 2TV", "Name")]
    public async Task ThenReferralsByRecipientAreRetrieved(string value1, string? value2, string paraType)
    {
        var urlParam = paraType switch
        {
            "Email" => $"api/referral/recipient?email={value1}",
            "Telephone" => $"api/referral/recipient?telephone={value1}",
            "Textphone" => $"api/referral/recipient?textphone={value1}",
            "Name" => $"api/referral/recipient?name={value1}&postcode={value2}",
            _ => string.Empty
        };

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(Client.BaseAddress + urlParam),
        };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", new JwtSecurityTokenHandler().WriteToken(Token));

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var retVal = await JsonSerializer.DeserializeAsync<List<ReferralDto>>(await response.Content.ReadAsStreamAsync(), options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        ArgumentNullException.ThrowIfNull(retVal);
        retVal.Should().NotBeNull();
        retVal.Count.Should().BeGreaterThan(0);
    }
}
