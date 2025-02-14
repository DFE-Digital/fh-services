using System.Text.Json;
using FamilyHubs.OpenReferral.Function.Models;
using FamilyHubs.SharedKernel;
using FamilyHubs.SharedKernel.OpenReferral.Enums;
using FamilyHubs.SharedKernel.Utilities;

namespace FamilyHubs.OpenReferral.Function.ClientServices;

// TODO: Change name to HsdaApiService once we no longer use the mock
public interface IApiService
{
    Task<Result<ServicesResponse>> GetAllServicesMinimal(string baseServiceUrl);
    Task<Result<ServiceResponse>> GetServiceById(string baseServiceUrl, string serviceId, OpenReferralSpec openReferralSpec);
}


public class ApiService(IHttpClientFactory httpClientFactory) : IApiService
{
    private readonly HttpClient _client = httpClientFactory.CreateClient();

    public async Task<Result<ServicesResponse>> GetAllServicesMinimal(string baseServiceUrl)
    {
        var result = await _client.GetAsync(baseServiceUrl);
        if (!result.IsSuccessStatusCode)
        {
            return Result<ServicesResponse>.Failure($"Failed to get the service list from {baseServiceUrl} | Status Code = {result.StatusCode}");
        }
        
        var jsonResponse = await result.Content.ReadAsStringAsync();
        var serviceList = GetContents(jsonResponse);
        var serviceIds = serviceList.Select(service => service.GetProperty("id").ToString()).ToArray();
        
        return Result<ServicesResponse>.Success(new ServicesResponse { ServiceIds = serviceIds });
    }

    public async Task<Result<ServiceResponse>> GetServiceById(string baseServiceUrl, string serviceId, OpenReferralSpec openReferralSpec)
    {
        var result = await _client.GetAsync($"{baseServiceUrl}/{serviceId}");
        
        if (!result.IsSuccessStatusCode)
        {
            return Result<ServiceResponse>.Failure($"Failed to get {baseServiceUrl}/{serviceId} | Status Code = {result.StatusCode}");
        }
        
        var jsonResponse = await result.Content.ReadAsStringAsync();
        var checksum = HashingAlgorithms.ComputeXxHashToLong64(jsonResponse);
        
        var service = openReferralSpec switch
        {
            OpenReferralSpec.InternationalSpec3 => DedsServiceModelFactory.CreateFromInternationalSpec3(jsonResponse),
            OpenReferralSpec.OldUkSpec => DedsServiceModelFactory.CreateFromOrUkSpec(jsonResponse),
            _ => throw new ArgumentOutOfRangeException(nameof(openReferralSpec), openReferralSpec, "OpenReferralSpec not supported.")
        };

        return service is null 
            ? Result<ServiceResponse>.Failure($"After attempting to deserialize the incoming JSON, the Service is null | JSON = {jsonResponse}") 
            : Result<ServiceResponse>.Success(new ServiceResponse { Service = service, Checksum = checksum });
    }

    /// <summary>
    /// Get the contents of the JSON response that caters for both 'contents' and 'content' properties.
    /// </summary>
    /// <param name="jsonResponse"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private static JsonElement.ArrayEnumerator GetContents(string jsonResponse)
    {
        var root = JsonDocument.Parse(jsonResponse).RootElement;

        if (root.TryGetProperty("contents", out var contents))
        {
            return contents.EnumerateArray();
        }

        if (root.TryGetProperty("content", out var content))
        {
            return content.EnumerateArray();
        }

        throw new InvalidOperationException("Neither 'contents' nor 'content' property found in the JSON response.");
    }
}

public class ServicesResponse
{
    public required string[] ServiceIds { get; set; }
}

public class ServiceResponse
{
    public required ServiceDto Service { get; set; }
    public required long Checksum { get; set; }
}