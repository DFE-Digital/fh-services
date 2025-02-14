using System.Net;
using FamilyHubs.OpenReferral.Function.ClientServices;
using FamilyHubs.OpenReferral.UnitTests.Helpers;
using FamilyHubs.SharedKernel.OpenReferral.Enums;
using NSubstitute;

namespace FamilyHubs.OpenReferral.UnitTests.Services;

public class WhenUsingApiService
{
    private readonly MockHttpMessageHandler _mockHttpMessageHandler;
    private readonly ApiService _apiService;
    private const string Url = "http://example.com/services";

    public WhenUsingApiService()
    {
        var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
        _mockHttpMessageHandler = new MockHttpMessageHandler();
        var httpClient = new HttpClient(_mockHttpMessageHandler);
        httpClientFactoryMock.CreateClient(Arg.Any<string>()).Returns(httpClient);
        _apiService = new ApiService(httpClientFactoryMock);
    }

    [Fact]
    public async Task GetAllServiceIds_ShouldReturnSuccessResult_WhenApiCallIsSuccessful()
    {
        // Arrange
        var responseContent = "{\"contents\":[{\"id\":\"1\"},{\"id\":\"2\"}]}";
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.OK;
        _mockHttpMessageHandler.Content = responseContent;

        // Act
        var result = await _apiService.GetAllServicesMinimal(Url);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(new[] { "1", "2" }, result.Data?.ServiceIds);
    }

    [Fact]
    public async Task GetAllServiceIds_ShouldReturnFailureResult_WhenApiCallFails()
    {
        // Arrange
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.InternalServerError;
        _mockHttpMessageHandler.Content = string.Empty;

        // Act
        var result = await _apiService.GetAllServicesMinimal(Url);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Failed to get the service list", result.ErrorMessage);
    }

    [Fact]
    public async Task GetServiceById_ShouldReturnSuccessResult_WhenApiCallIsSuccessful()
    {
        // Arrange
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.OK;
        _mockHttpMessageHandler.Content = MockService.ServiceJson;

        // Act
        var result = await _apiService.GetServiceById(Url, MockService.Service.OrId.ToString(), OpenReferralSpec.InternationalSpec3);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(MockService.Service.OrId, result.Data?.Service.OrId);
    }

    [Fact]
    public async Task GetServiceById_ShouldReturnFailureResult_WhenApiCallFails()
    {
        // Arrange
        var url = "http://example.com/services";
        var serviceId = "1";
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.BadRequest;
        _mockHttpMessageHandler.Content = string.Empty;

        // Act
        var result = await _apiService.GetServiceById(url, serviceId, OpenReferralSpec.InternationalSpec3);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Failed to get", result.ErrorMessage);
    }
    
    [Fact]
    public async Task GetServiceById_ShouldReturnChecksum_WhenApiCallIsSuccessful()
    {
        // Arrange
        _mockHttpMessageHandler.StatusCode = HttpStatusCode.OK;
        _mockHttpMessageHandler.Content = MockService.ServiceJson;

        // Act
        var result = await _apiService.GetServiceById(Url, MockService.Service.OrId.ToString(), OpenReferralSpec.InternationalSpec3);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(0, result.Data?.Checksum);
    }
}