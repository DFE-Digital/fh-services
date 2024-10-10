using FamilyHubs.Referral.Core.Models;
using FamilyHubs.Referral.Infrastructure.DistributedCache;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text;
using NSubstitute;

namespace FamilyHubs.ReferralUi.UnitTests.Infrastructure.DistributedCache;

public class ReferralDistributedCacheTests
{
    private const string ProfessionalsEmail = "pro@example.com";
    private readonly IDistributedCache _mockDistributedCache;
    private readonly ConnectionRequestDistributedCache _connectionRequestDistributedCache;
    private readonly ConnectionRequestModel _connectionRequestModel;
    private readonly byte[] _professionalReferralModelSerializedBytes;

    public ReferralDistributedCacheTests()
    {
        _mockDistributedCache = Substitute.For<IDistributedCache>();

        var mockDistributedCacheEntryOptions = Substitute.For<DistributedCacheEntryOptions>();
        _connectionRequestDistributedCache = new ConnectionRequestDistributedCache(
            _mockDistributedCache,
            mockDistributedCacheEntryOptions);
        _connectionRequestModel = new ConnectionRequestModel
        {
            FamilyContactFullName = "FamilyContactFullName",
            ServiceId = "1"
        };

        _professionalReferralModelSerializedBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(_connectionRequestModel));
    }

    [Fact]
    public async Task GetProfessionalReferralAsync_WhenCalled_ReturnsProfessionalReferral()
    {
        // Arrange
        _mockDistributedCache.GetAsync(ProfessionalsEmail, default)
            .Returns(_professionalReferralModelSerializedBytes);

        // Act
        var result = await _connectionRequestDistributedCache.GetAsync(ProfessionalsEmail);

        // Assert
        result.Should().BeEquivalentTo(_connectionRequestModel);
    }

    [Fact]
    public async Task SetProfessionalReferralAsync_WhenCalled_SetsProfessionalReferral()
    {
        // act
        await _connectionRequestDistributedCache.SetAsync(ProfessionalsEmail, _connectionRequestModel);
        
        await _mockDistributedCache.Received(1).SetAsync(
            ProfessionalsEmail,
            Arg.Is<byte[]>(bytes => bytes.SequenceEqual(_professionalReferralModelSerializedBytes)),
            Arg.Any<DistributedCacheEntryOptions>(),
            default);
    }

    [Fact]
    public async Task RemoveProfessionalReferralAsync_WhenCalled_RemovesProfessionalReferral()
    {
        // Act
        await _connectionRequestDistributedCache.RemoveAsync(ProfessionalsEmail);
        
        // Assert
        await _mockDistributedCache.Received(1).RemoveAsync(ProfessionalsEmail, default);
    }
}