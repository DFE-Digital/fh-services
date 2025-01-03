using FamilyHubs.ServiceDirectory.Shared.Dto;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idams.Maintenance.Core.ApiClient;

public interface IServiceDirectoryClient
{
    Task<List<OrganisationDto>> GetOrganisations(CancellationToken cancellationToken = default);
}

public class ServiceDirectoryClient : ApiService<ServiceDirectoryClient>, IServiceDirectoryClient
{
    public ServiceDirectoryClient(HttpClient client, ILogger<ServiceDirectoryClient> logger)
        : base(client, logger)
    {
    }

    public async Task<List<OrganisationDto>> GetOrganisations(CancellationToken cancellationToken = default)
    {
        var semaphore = new SemaphoreSlim(1, 1);

        await semaphore.WaitAsync(cancellationToken);

        var request = new HttpRequestMessage();
        request.Method = HttpMethod.Get;
        request.RequestUri = new Uri(Client.BaseAddress + "api/organisations");

        using var response = await Client.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();

        var organisations = await DeserializeResponse<List<OrganisationDto>>(response, cancellationToken) ?? [];

        Logger.LogInformation("{Client} Returning {Count} Organisations", nameof(ServiceDirectoryClient), organisations.Count);

        return organisations;
    }
}
