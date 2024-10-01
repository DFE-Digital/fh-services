using System.Text.Json;
using FamilyHubs.ServiceDirectory.Shared.Dto;

namespace FamilyHubs.Idam.Core.Services;

public interface IServiceDirectoryService
{
    Task<List<OrganisationDto>?> GetAllOrganisations();
    Task<List<OrganisationDto>?> GetOrganisationsByAssociatedId(long id);
    Task<List<OrganisationDto>?> GetOrganisationsByName(string name);
    Task<List<OrganisationDto>?> GetOrganisationsByIds(IEnumerable<long> ids);
}

//todo: we should only have a single client per API shared across the world
public class ServiceDirectoryService(HttpClient httpClient) : IServiceDirectoryService
{
    private async Task<List<OrganisationDto>?> GetOrganisationsByUri(string uri)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(httpClient.BaseAddress + uri)
        };

        using var response = await httpClient.SendAsync(request);

        string json = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(json))
        {
            throw new InvalidOperationException("Invalid response from ServiceDirectory Api");
        }

        return JsonSerializer.Deserialize<List<OrganisationDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<List<OrganisationDto>?> GetAllOrganisations()
    {
        return await GetOrganisationsByUri("api/organisations");
    }

    public async Task<List<OrganisationDto>?> GetOrganisationsByAssociatedId(long id)
    {
        return await GetOrganisationsByUri($"api/organisationsByAssociatedOrganisation?id={id}");
    }

    public async Task<List<OrganisationDto>?> GetOrganisationsByName(string name)
    {
        return await GetOrganisationsByUri($"api/organisations?name={name}");
    }

    public async Task<List<OrganisationDto>?> GetOrganisationsByIds(IEnumerable<long> ids)
    {
        var enumerable = ids.ToList();
        if (!enumerable.Any()) return new List<OrganisationDto>();

        string query = string.Join("&ids=", enumerable);
        return await GetOrganisationsByUri($"api/organisations?ids={query}");
    }
}