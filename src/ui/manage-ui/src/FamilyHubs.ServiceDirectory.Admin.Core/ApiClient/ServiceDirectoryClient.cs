﻿using FamilyHubs.ServiceDirectory.Admin.Core.Exceptions;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.ServiceDirectory.Shared.Models;
using FamilyHubs.SharedKernel.Exceptions;
using FamilyHubs.SharedKernel.Razor.Dashboard;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient.Exceptions;
using FamilyHubs.ServiceDirectory.Shared.CreateUpdateDto;
using Microsoft.AspNetCore.WebUtilities;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;

public interface IServiceDirectoryClient
{
    Task<List<OrganisationDto>> GetOrganisations(
        CancellationToken cancellationToken = default,
        OrganisationType? organisationType = null,
        long? associatedOrganisationId = null);
    Task<List<OrganisationDto>> GetOrganisationByAssociatedOrganisation(long id);
    //todo: caching will be problematic when someone adds an org, and they can't use it until the app service recycles
    // caching orgs would be useful (but shouldn't be in the client), but we'd probably need a way to invalidate the cache when an org is added
    // but we'd need something distributed to cope with the multiple instances of the app service
    Task<List<OrganisationDto>> GetCachedLaOrganisations(CancellationToken cancellationToken = default);
    Task<List<OrganisationDto>> GetCachedVcsOrganisations(long laOrganisationId, CancellationToken cancellationToken = default);
    Task<OrganisationDetailsDto> GetOrganisationById(long id, CancellationToken cancellationToken = default);
    Task<Outcome<long, ApiException>> CreateOrganisation(OrganisationDto organisation);
    Task<long> UpdateOrganisation(OrganisationDto organisation);
    Task<bool> DeleteOrganisation(long id);
    Task<long> CreateService(ServiceChangeDto service, CancellationToken cancellationToken = default);
    Task<long> UpdateService(ServiceChangeDto service, CancellationToken cancellationToken = default);
    Task<ServiceDto> GetServiceById(long id, CancellationToken cancellationToken = default);

    Task<PaginatedList<ServiceNameDto>> GetServiceSummaries(
        long? organisationId = null,
        string? serviceNameSearch = null,
        int pageNumber = 1,
        int pageSize = 10,
        SortOrder sortOrder = SortOrder.ascending,
        CancellationToken cancellationToken = default);

    Task<LocationDto> GetLocationById(long id, CancellationToken cancellationToken = default);
    Task<long> CreateLocation(LocationDto location, CancellationToken cancellationToken = default);
    Task<long> UpdateLocation(LocationDto location, CancellationToken cancellationToken = default);
    Task<PaginatedList<LocationDto>> GetLocations(bool isAscending, string orderByColumn, string? searchName, bool isFamilyHub, int pageNumber = 1, int pageSize = 10,  CancellationToken cancellationToken = default);
    Task<PaginatedList<LocationDto>> GetLocationsByOrganisationId(long organisationId,  bool? isAscending, string orderByColumn, string? searchName, bool? isFamilyHub, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task DeleteService(long serviceId, CancellationToken cancellationToken = default);
}

public class ServiceDirectoryClient : ApiService, IServiceDirectoryClient
{
    private readonly ICacheService _cacheService;
    private readonly ILogger<ServiceDirectoryClient> _logger;

    public ServiceDirectoryClient(
        HttpClient client,
        ICacheService cacheService,
        ILogger<ServiceDirectoryClient> logger)
        : base(client)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    private async Task<List<OrganisationDto>> GetCachedOrganisationsInternal(CancellationToken cancellationToken = default)
    {
        var semaphore = new SemaphoreSlim(1, 1);
        var organisations = await _cacheService.GetOrganisations();
        if (organisations is not null)
            return organisations;
        try
        {
            await semaphore.WaitAsync(cancellationToken);

            // recheck to make sure it didn't populate before entering semaphore
            organisations = await _cacheService.GetOrganisations();
            if (organisations is not null)
                return organisations;

            organisations = await GetOrganisations(cancellationToken);

            await _cacheService.StoreOrganisations(organisations);

            return organisations;
        }
        finally
        {
            semaphore.Release();
        }
    }

    public async Task<List<OrganisationDto>> GetOrganisations(
        CancellationToken cancellationToken = default,
        OrganisationType? organisationType = null,
        long? associatedOrganisationId = null)
    {
        var queryParams = new Dictionary<string, string?>();
        if (organisationType != null)
        {
            queryParams.Add("organisationType", organisationType.ToString());
        }
        if (associatedOrganisationId != null)
        {
            queryParams.Add("associatedOrganisationId", associatedOrganisationId.ToString());
        }

        string requestUri = QueryHelpers.AddQueryString($"{Client.BaseAddress}api/organisations", queryParams);

        using var response = await Client.GetAsync(requestUri, cancellationToken);

        return await Read<List<OrganisationDto>>(response, cancellationToken);
    }

    public async Task<List<OrganisationDto>> GetOrganisationByAssociatedOrganisation(long id)
    {
        var request = new HttpRequestMessage();
        request.Method = HttpMethod.Get;
        request.RequestUri = new Uri(Client.BaseAddress + $"api/organisationsByAssociatedOrganisation?id={id}");

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var organisations = await DeserializeResponse<List<OrganisationDto>>(response) ?? new List<OrganisationDto>();

        _logger.LogInformation("Returning {Count} associated organisations", organisations.Count);

        return organisations;
    }

    public async Task<List<OrganisationDto>> GetCachedLaOrganisations(CancellationToken cancellationToken = default)
    {
        var organisations = await GetCachedOrganisationsInternal(cancellationToken);

        var laOrganisations = organisations.Where(x => x.OrganisationType == OrganisationType.LA).ToList();

        return laOrganisations;
    }

    public async Task<List<OrganisationDto>> GetCachedVcsOrganisations(long laOrganisationId, CancellationToken cancellationToken = default)
    {
        // recheck to make sure it didn't populate before entering semaphore
        var organisations = await GetCachedOrganisationsInternal(cancellationToken);

        var vcsOrganisations = organisations.Where(x => x.OrganisationType == OrganisationType.VCFS && x.AssociatedOrganisationId == laOrganisationId).ToList();

        return vcsOrganisations;
    }

    public async Task<OrganisationDetailsDto> GetOrganisationById(long id, CancellationToken cancellationToken = default)
    {
        using var response = await Client.GetAsync($"{Client.BaseAddress}api/organisations/{id}", cancellationToken);

        return await Read<OrganisationDetailsDto>(response, cancellationToken);
    }

    public async Task<Outcome<long, ApiException>> CreateOrganisation(OrganisationDto organisation)
    {
        var request = new HttpRequestMessage();
        request.Method = HttpMethod.Post;
        request.RequestUri = new Uri(Client.BaseAddress + "api/organisations");
        request.Content =
            new StringContent(JsonConvert.SerializeObject(organisation), Encoding.UTF8, "application/json");

        using var response = await Client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            await _cacheService.ResetOrganisations();
            var stringResult = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Organisation Created id:{Id}", stringResult);
            return new Outcome<long, ApiException>(long.Parse(stringResult));
        }

        var failure = await response.Content.ReadFromJsonAsync<ApiExceptionResponse<ValidationError>>();
        if (failure != null)
        {
            _logger.LogWarning("Failed to add Organisation {ApiExceptionResponse}", failure);
            return new Outcome<long, ApiException>(new ApiException(failure));
        }

        _logger.LogError("Response from api failed with an unknown response body {StatusCode}", response.StatusCode);
        var unhandledException = new ApiExceptionResponse<ValidationError>
        {
            Title = "Failed to add Organisation",
            Detail = "Response from api failed with an unknown response body",
            StatusCode = (int)response.StatusCode,
            ErrorCode = ErrorCodes.UnhandledException.ParseToCodeString()
        };

        return new Outcome<long, ApiException>(new ApiException(unhandledException));
    }

    public async Task<long> UpdateOrganisation(OrganisationDto organisation)
    {
        var request = new HttpRequestMessage();
        request.Method = HttpMethod.Put;
        request.RequestUri = new Uri(Client.BaseAddress + $"api/organisations/{organisation.Id}");
        request.Content =
            new StringContent(JsonConvert.SerializeObject(organisation), Encoding.UTF8, "application/json");

        using var response = await Client.SendAsync(request);
        await _cacheService.ResetOrganisations();

        await ValidateResponse(response);

        var stringResult = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("Organisation Updated id:{Id}", stringResult);
        return long.Parse(stringResult);
    }

    public async Task<bool> DeleteOrganisation(long id)
    {
        var request = new HttpRequestMessage();
        request.Method = HttpMethod.Delete;
        request.RequestUri = new Uri(Client.BaseAddress + $"api/organisations/{id}");

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var retVal = await DeserializeResponse<bool>(response);
        ArgumentNullException.ThrowIfNull(retVal);
        _logger.LogInformation("Organisation Deleted id:{Id}", id);

        return retVal;
    }

    public async Task<long> CreateService(ServiceChangeDto service, CancellationToken cancellationToken = default)
    {
        using var response = await Client.PostAsJsonAsync($"{Client.BaseAddress}api/services", service, cancellationToken);

        return await Read<long>(response, cancellationToken);
    }

    public async Task<long> UpdateService(ServiceChangeDto service, CancellationToken cancellationToken = default)
    {
        using var response = await Client.PutAsJsonAsync($"{Client.BaseAddress}api/services/{service.Id}", service, cancellationToken);

        return await Read<long>(response, cancellationToken);
    }

    public async Task<ServiceDto> GetServiceById(long id, CancellationToken cancellationToken = default)
    {
        //todo:
        using var response = await Client.GetAsync($"{Client.BaseAddress}api/services-simple/{id}", cancellationToken);

        return await Read<ServiceDto>(response, cancellationToken);
    }

    public async Task<PaginatedList<ServiceNameDto>> GetServiceSummaries(
        long? organisationId = null,
        string? serviceNameSearch = null,
        int pageNumber = 1,
        int pageSize = 10, 
        SortOrder sortOrder = SortOrder.ascending,
        CancellationToken cancellationToken = default)
    {
        if (sortOrder == SortOrder.none)
            throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, "SortOrder can not be none");

        string endpointUrl = $"{Client.BaseAddress}api/services/summary?pageNumber={pageNumber}&pageSize={pageSize}&sortOrder={sortOrder}";
        if (organisationId != null)
        {
            endpointUrl += $"&organisationId={organisationId}";
        }

        if (serviceNameSearch != null)
        {
            endpointUrl += $"&serviceNameSearch={serviceNameSearch}";
            
        }

        using var response = await Client.GetAsync(endpointUrl, cancellationToken);

        //todo: extension method with generic type on extension (with base extension)
        return await Read<PaginatedList<ServiceNameDto>>(response, cancellationToken);
    }

    private async Task<T> Read<T>(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new ServiceDirectoryClientServiceException(response, await response.Content.ReadAsStringAsync(cancellationToken));
        }
        var content = await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);

        if (content is null)
        {
            // the only time it'll be null, is if the API returns "null"
            // (see https://stackoverflow.com/questions/71162382/why-are-the-return-types-of-nets-system-text-json-jsonserializer-deserialize-m)
            // unlikely, but possibly (pass new MemoryStream(Encoding.UTF8.GetBytes("null")) to see it actually return null)
            // note we hard-code passing "null", rather than messing about trying to rewind the stream, as this is such a corner case and we want to let the deserializer take advantage of the async stream (in the happy case)
            throw new ServiceDirectoryClientServiceException(response, "null");
        }

        return content;
    }

    private static async Task ValidateResponse(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            // TODO : handle failures without throwing errors
            var failure = await response.Content.ReadFromJsonAsync<ApiExceptionResponse<ValidationError>>();
            if (failure != null)
            {
                throw new ApiException(failure);
            }

            response.EnsureSuccessStatusCode();
        }
    }

    public async Task<LocationDto> GetLocationById(long id, CancellationToken cancellationToken = default)
    {
        using var response = await Client.GetAsync($"{Client.BaseAddress}api/locations/{id}", cancellationToken);

        return await Read<LocationDto>(response, cancellationToken);
    }

    public async Task<long> CreateLocation(LocationDto location, CancellationToken cancellationToken = default)
    {
        using var response = await Client.PostAsJsonAsync($"{Client.BaseAddress}api/locations", location, cancellationToken);

        return await Read<long>(response, cancellationToken);
    }

    public async Task<long> UpdateLocation(LocationDto location, CancellationToken cancellationToken = default)
    {
        using var response = await Client.PutAsJsonAsync($"{Client.BaseAddress}api/locations/{location.Id}", location, cancellationToken);

        return await Read<long>(response, cancellationToken);
    }

    public async Task<PaginatedList<LocationDto>> GetLocations(bool isAscending, string orderByColumn, string? searchName, bool isFamilyHub, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        using var response = await Client.GetAsync($"{Client.BaseAddress}api/locations?pageNumber={pageNumber}&pageSize={pageSize}&isAscending={isAscending}&orderByColumn={orderByColumn}&searchName={searchName}&isFamilyHub={isFamilyHub}", cancellationToken);

        return await Read<PaginatedList<LocationDto>>(response, cancellationToken);
    }

    public async Task<PaginatedList<LocationDto>> GetLocationsByOrganisationId(long organisationId, bool? isAscending, string orderByColumn, string? searchName, bool? isFamilyHub, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        using var response = await Client.GetAsync($"{Client.BaseAddress}api/organisationlocations/{organisationId}?pageNumber={pageNumber}&pageSize={pageSize}&isAscending={isAscending}&orderByColumn={orderByColumn}&searchName={searchName}&isFamilyHub={isFamilyHub}", cancellationToken);

        return await Read<PaginatedList<LocationDto>>(response, cancellationToken);
    }

    public async Task DeleteService(long serviceId, CancellationToken cancellationToken = default)
    {
        HttpRequestMessage request = new()
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(Client.BaseAddress + $"api/services/{serviceId}")
        };

        using HttpResponseMessage response = await Client.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}