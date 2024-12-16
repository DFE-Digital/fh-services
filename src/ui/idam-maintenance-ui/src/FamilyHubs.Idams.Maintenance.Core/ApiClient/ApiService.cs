using Microsoft.Extensions.Logging;
using System.Text.Json;
// ReSharper disable StaticMemberInGenericType

namespace FamilyHubs.Idams.Maintenance.Core.ApiClient;

public abstract class ApiService<TApiService>
{
    protected readonly HttpClient Client;
    protected readonly ILogger<TApiService> Logger;
    private static readonly JsonSerializerOptions CaseInsensitive = new() { PropertyNameCaseInsensitive = true };

    protected ApiService(HttpClient client, ILogger<TApiService> logger)
    {
        Client = client;
        Logger = logger;
    }

    protected async Task<T?> DeserializeResponse<T>(HttpResponseMessage response, CancellationToken? cancellationToken = null)
    {
        try
        {
            var contents = cancellationToken is not null 
                ? await response.Content.ReadAsStringAsync(cancellationToken.Value) 
                : await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(contents, CaseInsensitive);
        }
        catch (Exception exception)
        {
            Logger.LogError("Failed to DeserializeResponse StatusCode:{StatusCode} Error:{Error}", response.StatusCode, exception.Message);
            throw;
        }
    }
}