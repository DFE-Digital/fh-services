using System.Text.Json;
using FamilyHubs.ReferralService.Shared.Dto;

namespace FamilyHubs.RequestForSupport.Core.ApiClients;

public interface IOrganisationClientService
{
    Task<OrganisationDto?> GetOrganisationDtoByIdAsync(long id);
}

public class OrganisationClientService : ApiService, IOrganisationClientService
{
    public OrganisationClientService(HttpClient client) : base(client)
    {
    }

    public async Task<OrganisationDto?> GetOrganisationDtoByIdAsync(long id)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(Client.BaseAddress + $"api/organisations/{id}"),
        };

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        return await JsonSerializer.DeserializeAsync<OrganisationDto>(await response.Content.ReadAsStreamAsync(), options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}