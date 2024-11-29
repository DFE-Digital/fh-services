using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.Idams.Maintenance.Core.ApiClient;

public interface IServiceDirectoryClient
{
    Task<OrganisationDetailsDto?> GetOrganisationById(long id);
    Task<List<OrganisationDto>> GetOrganisations(CancellationToken cancellationToken = default);
}

public class ServiceDirectoryClient : ApiService<ServiceDirectoryClient>, IServiceDirectoryClient
{
#if MABUSE_DistributedCache
    private readonly ICacheService _cacheService;
#endif

    public ServiceDirectoryClient(HttpClient client, ICacheService cacheService, ILogger<ServiceDirectoryClient> logger)
        : base(client, logger)
    {
#if MABUSE_DistributedCache
        _cacheService = cacheService;
#endif
    }

    public async Task<OrganisationDetailsDto?> GetOrganisationById(long id)
    {
        var request = new HttpRequestMessage();
        request.Method = HttpMethod.Get;
        request.RequestUri = new Uri(Client.BaseAddress + $"api/organisations/{id}");

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        Logger.LogInformation($"{nameof(ServiceDirectoryClient)} Returning Organisation");
        return await DeserializeResponse<OrganisationDetailsDto>(response);
    }

    public async Task<List<OrganisationDto>> GetOrganisations(CancellationToken cancellationToken = default)
    {
        var semaphore = new SemaphoreSlim(1, 1);
#if MABUSE_DistributedCache
        var cachedOrganisations = await _cacheService.GetOrganisations();
        if (cachedOrganisations is not null)
            return cachedOrganisations;
#endif

        await semaphore.WaitAsync(cancellationToken);

        var request = new HttpRequestMessage();
        request.Method = HttpMethod.Get;
        request.RequestUri = new Uri(Client.BaseAddress + "api/organisations");

        using var response = await Client.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();

        var organisations = await DeserializeResponse<List<OrganisationDto>>(response, cancellationToken) ?? new List<OrganisationDto>();

        Logger.LogInformation($"{nameof(ServiceDirectoryClient)} Returning  {organisations.Count} Organisations");

#if MABUSE_DistributedCache
        await _cacheService.StoreOrganisations(organisations);
#endif

        return organisations;
    }
}
