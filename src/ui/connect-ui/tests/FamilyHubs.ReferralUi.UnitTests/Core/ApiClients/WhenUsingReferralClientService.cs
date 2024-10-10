using FamilyHubs.Referral.Core.ApiClients;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using FamilyHubs.ReferralService.Shared.Dto.CreateUpdate;
using FamilyHubs.ReferralService.Shared.Dto.Metrics;
using FamilyHubs.ReferralService.Shared.Models;
using FamilyHubs.ReferralUi.UnitTests.Helpers;

namespace FamilyHubs.ReferralUi.UnitTests.Core.ApiClients;

public class WhenUsingReferralClientService
{
    private readonly CreateReferralDto _createReferralDto;
    private HttpClient? _httpClient;
    private ReferralClientService? _referralClientService;

    public WhenUsingReferralClientService()
    {
        _createReferralDto = new CreateReferralDto(ClientHelper.GetReferralDto(), new ConnectionRequestsSentMetricDto(DateTimeOffset.UtcNow));
    }

    [Fact]
    public async Task CreateReferral_WithValidData_ReturnsReferralId()
    {
        // Arrange
        var jsonString = JsonSerializer.Serialize(new ReferralResponse
        {
            Id = 123,
            ServiceName = "At your service",
            OrganisationId = 456
        });

        _httpClient = TestHelpers.GetMockClient(jsonString);
        _referralClientService = new ReferralClientService(_httpClient);

        // Act
        var result= await _referralClientService.CreateReferral(_createReferralDto);

        // Assert
        result.Id.Should().Be(123);
    }

    [Fact]
    public async Task CreateReferral_WithInvalidData_ThrowsReferralClientServiceException()
    {
        // Arrange
        _httpClient = TestHelpers.GetMockClient("Invalid request", HttpStatusCode.BadRequest);
        _referralClientService = new ReferralClientService(_httpClient);

        // Act and Assert
        await Assert.ThrowsAsync<ReferralClientServiceException>(() => _referralClientService.CreateReferral(_createReferralDto)); 
    }
}
