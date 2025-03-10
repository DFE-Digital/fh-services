using System.Dynamic;
using FamilyHubs.ServiceDirectory.Core.ServiceDirectory.Interfaces;
using FamilyHubs.ServiceDirectory.Core.ServiceDirectory.Models;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.ServiceDirectory.Shared.Models;
using FamilyHubs.ServiceDirectory.Web.Content;
using FamilyHubs.ServiceDirectory.Web.Filtering.Filters;
using FamilyHubs.ServiceDirectory.Web.Filtering.Interfaces;
using FamilyHubs.ServiceDirectory.Web.Mappers;
using FamilyHubs.ServiceDirectory.Web.Models;
using FamilyHubs.SharedKernel.Razor.Pagination;
using FamilyHubs.SharedKernel.Services.Postcode.Interfaces;
using FamilyHubs.SharedKernel.Services.Postcode.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;

namespace FamilyHubs.ServiceDirectory.Web.Pages.ServiceFilter;

public class ServiceFilterModel : PageModel
{
    // simpler than asking all the filters to remove themselves
    private static HashSet<string> _parametersWhitelist = new()
    {
        "postcode",
        "adminarea",
        "latitude",
        "longitude",
    };

    public IEnumerable<IFilter> Filters { get; set; } = null!;
    public string? Postcode { get; set; }
    public string? AdminArea { get; set; }
    public float? Latitude { get; set; }
    public float? Longitude { get; set; }
    public IEnumerable<Service> Services { get; set; }
    public bool FromPostcodeSearch { get; set; }
    public int CurrentPage { get; set; }
    public IPagination Pagination { get; set; }
    public int TotalResults { get; set; }
    public Guid CorrelationId { get; set; }

    public byte? SelectedFilterDistance 
    {
        get
        {
            string? selectedFilterDistanceStr = Filters
                .FirstOrDefault(f => f.Name == SearchWithinFilter.SEARCH_WITHIN_FILTER_NAME)?.SelectedAspects
                .FirstOrDefault()?.Value;

            if (selectedFilterDistanceStr is not null)
            {
                return byte.Parse(selectedFilterDistanceStr);
            }

            return null;
        }
    }

    private readonly IServiceDirectoryClient _serviceDirectoryClient;
    private readonly IPostcodeLookup _postcodeLookup;
    private readonly IPageFilterFactory _pageFilterFactory;
    private readonly ILogger<ServiceFilterModel> _logger;
    private const int PageSize = 10;

    public ServiceFilterModel(
        IServiceDirectoryClient serviceDirectoryClient,
        IPostcodeLookup postcodeLookup,
        IPageFilterFactory pageFilterFactory,
        ILogger<ServiceFilterModel> logger
    )
    {
        _serviceDirectoryClient = serviceDirectoryClient;
        _postcodeLookup = postcodeLookup;
        _pageFilterFactory = pageFilterFactory;
        _logger = logger;

        Services = Enumerable.Empty<Service>();
        Pagination = new DontShowPagination();
    }

    public async Task<IActionResult> OnPost(string? postcode, string? adminArea, [FromForm] IFormCollection form)
    {
        dynamic routeValues;

        if (adminArea == null)
        {
            var (postcodeError, postcodeInfo) = await _postcodeLookup.Get(postcode);
            if (postcodeError != PostcodeError.None)
            {
                return RedirectToPage("/PostcodeSearch/Index", new { postcodeError });
            }

            routeValues = GetFromPostcodeSearchRouteValues(postcodeInfo!);
        }
        else
        {
            var remove = GetRemove(form);

            // remove key/values we don't want to keep
            var filteredForm = form
                .Where(kvp => KeepParam(kvp.Key, remove.Key))
                .ToList();

            //todo: hacky: ask optional filters (or all filters), to manipulate form
            if (filteredForm.TrueForAll(kvp => kvp.Key != "children_and_young-option-selected"))
            {
                filteredForm = filteredForm.Where(keyValuePair => keyValuePair.Key != "children_and_young").ToList();
            }

            if (remove.Value != null)
            {
                // remove values we don't want to keep
                filteredForm = filteredForm.Select(kvp => RemoveFilterValue(kvp, remove)).ToList();
            }

            routeValues = ToRouteValues(filteredForm);
        }

        return RedirectToPage("/ServiceFilter/Index", routeValues);
    }

    private static dynamic GetFromPostcodeSearchRouteValues(IPostcodeInfo postcodeInfo)
    {
        dynamic routeValues = new
        {
            postcode = postcodeInfo.Postcode,
            adminarea = postcodeInfo.AdminArea,
            latitude = postcodeInfo.Latitude,
            longitude = postcodeInfo.Longitude,
            frompostcodesearch = true
        };
        return routeValues;
    }

    private static dynamic ToRouteValues(IEnumerable<KeyValuePair<string, StringValues>> values)
    {
        dynamic routeValues = new ExpandoObject();
        var routeValuesDictionary = (IDictionary<string, object>)routeValues;

        foreach (var keyValuePair in values)
        {
            // ToString() stops RedirectToPage() using key=foo&key=bar, and instead we get key=foo,bar which we unpick on the GET
            routeValuesDictionary[keyValuePair.Key] = keyValuePair.Value.ToString();
        }

        return routeValues;
    }

    private static KeyValuePair<string?, string?> GetRemove(IFormCollection form)
    {
        string? remove = form[IFilter.RemoveKey];
        switch (remove)
        {
            case null:
                return default;
            case IFilter.RemoveAllValue:
                return new KeyValuePair<string?, string?>(IFilter.RemoveAllValue, null);
        }

        int filterNameEndPos = remove.IndexOf("--", StringComparison.Ordinal);
        if (filterNameEndPos == -1)
            return default;

        return new KeyValuePair<string?, string?>(remove[..filterNameEndPos], remove[(filterNameEndPos + 2)..]);
    }

    private static KeyValuePair<string, StringValues> RemoveFilterValue(KeyValuePair<string, StringValues> kvp, KeyValuePair<string?, string?> remove)
    {
        if (kvp.Key != remove.Key)
            return kvp;

        var values = kvp.Value.ToList();
        values.Remove(remove.Value);
        return new KeyValuePair<string, StringValues>(kvp.Key, new StringValues(values.ToArray()));
    }

    private static bool KeepParam(string key, string? removeKey)
    {
        if (key is "__RequestVerificationToken" or IFilter.RemoveKey)
            return false;

        if (removeKey == null)
            return true;

        //todo: move this logic out of here?
        if (removeKey == IFilter.RemoveAllValue)
        {
            return _parametersWhitelist.Contains(key.ToLowerInvariant());
        }

        // if we're removing a filter, go back to page 1
        if (key == QueryParamKeys.PageNum)
            return false;

        //todo: we don't want knowledge of the internals of the optional filter here
        if (key.EndsWith("-option-selected") && key.StartsWith(removeKey))
            return false;

        // keep, but we might remove the only value later
        return true;
    }

    public Task<IActionResult> OnGet(string? postcode, string? adminArea, float? latitude, float? longitude, int? pageNum, bool? fromPostcodeSearch, Guid? correlationId)
    {
        if (AnyParametersMissing(postcode, adminArea, latitude, longitude))
        {
            // handle cases:
            // * when user goes filter page => cookie page => back link from success banner
            // * user manually removes query parameters from url
            // * user goes directly to page by typing it into the address bar
            return Task.FromResult<IActionResult>(RedirectToPage("/PostcodeSearch/Index"));
        }

        return HandleGet(postcode!, adminArea!, latitude!.Value, longitude!.Value, pageNum, fromPostcodeSearch, correlationId ?? Guid.NewGuid());
    }

    private static bool AnyParametersMissing(string? postcode, string? adminArea, float? latitude, float? longitude)
    {
        return string.IsNullOrEmpty(postcode)
            || string.IsNullOrEmpty(adminArea)
            || latitude == null
            || longitude == null;
    }

    private async Task<IActionResult> HandleGet(string postcode, string adminArea, float latitude, float longitude, int? pageNum, bool? fromPostcodeSearch, Guid correlationId)
    {
        FromPostcodeSearch = fromPostcodeSearch == true;
        Postcode = postcode;
        AdminArea = adminArea;
        Latitude = latitude;
        Longitude = longitude;
        CurrentPage = pageNum ?? 1;
        CorrelationId = correlationId;

        // before each page load we need to initialise default filter options
        Filters = await _pageFilterFactory.GetDefaultFilters();

        // if we got here from PostCode search, then just used above Default filters else apply the filters from the query parameters
        if (!FromPostcodeSearch)
        {
            Filters = Filters.Select(fd => fd.Apply(Request.Query));
        }

        try
        {
            DateTime requestTimestamp = DateTime.UtcNow;
            
            (PaginatedList<ServiceDto> paginatedServices, Pagination, HttpResponseMessage? response) 
                = await GetServicesAndPagination(adminArea, latitude, longitude);
            UpdateServicesPagination(paginatedServices);

            DateTime? responseTimestamp = response is not null ? DateTime.UtcNow : null;

            if (Postcode is not null)
            {
                // If the user is coming from the initial postcode search page,
                // FromPostCodeSearch will be true, and we can use this to differentiate
                // between initial searches, and subsequent search query changes.
                var eventType = FromPostcodeSearch ? ServiceDirectorySearchEventType.ServiceDirectoryInitialSearch
                    : ServiceDirectorySearchEventType.ServiceDirectorySearchFilter;

                await _serviceDirectoryClient.RecordServiceSearch(
                    eventType,
                    Postcode!,
                    AdminArea,
                    SelectedFilterDistance,
                    paginatedServices.Items,
                    requestTimestamp,
                    responseTimestamp,
                    response?.StatusCode,
                    CorrelationId
                );

            }
                
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred storing service search metric. {ExceptionMessage}",
                ex.Message);
        }

        return Page();
    }

    private async Task<(
        PaginatedList<ServiceDto> services,
        IPagination pagination,
        HttpResponseMessage? response
    )> GetServicesAndPagination(string adminArea, float latitude, float longitude)
    {
        var serviceParams = new ServicesParams(adminArea, latitude, longitude)
        {
            PageNumber = CurrentPage,
            PageSize = PageSize
        };

        foreach (var filter in Filters)
        {
            filter.AddFilterCriteria(serviceParams);
        }

        var (services, response) = await _serviceDirectoryClient.GetServices(serviceParams);

        var pagination = new LargeSetPagination(services.TotalPages, CurrentPage);

        return (services, pagination, response);
    }

    private void UpdateServicesPagination(PaginatedList<ServiceDto> paginatedServices) 
    {
        TotalResults = paginatedServices.TotalCount;
        Services = ServiceMapper.ToViewModel(paginatedServices.Items);
    }
}