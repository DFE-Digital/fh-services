using System.Dynamic;
using EnumsNET;
using FamilyHubs.Referral.Core.ApiClients;
using FamilyHubs.Referral.Core.Helper;
using FamilyHubs.Referral.Core.Models;
using FamilyHubs.Referral.Web.Pages.Shared;
using FamilyHubs.ServiceDirectory.Shared.Display;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.ServiceDirectory.Shared.Models;
using FamilyHubs.ServiceDirectory.Shared.ReferenceData;
using FamilyHubs.ServiceDirectory.Shared.ReferenceData.ICalendar;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Identity.Models;
using FamilyHubs.SharedKernel.Razor.Pagination;
using FamilyHubs.SharedKernel.Services.Postcode.Interfaces;
using FamilyHubs.SharedKernel.Services.Postcode.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace FamilyHubs.Referral.Web.Pages.ProfessionalReferral;

public class LocalOfferResultsModel : HeaderPageModel
{
    private readonly IPostcodeLookup _postcodeLookup;
    private readonly IOrganisationClientService _organisationClientService;
    private readonly ILogger<LocalOfferResultsModel> _logger;

    public List<KeyValuePair<TaxonomyDto, List<TaxonomyDto>>> NestedCategories { get; set; } = default!;
    public List<TaxonomyDto> Categories { get; set; } = default!;
    public double? CurrentLatitude { get; set; }
    public double? CurrentLongitude { get; set; }
    public PaginatedList<ServiceDto> SearchResults { get; set; } = new();
    
    [BindProperty]
    public string? SelectedDistance { get; set; }

    private bool _isInitialSearch = true;

    public static List<SelectListItem> AgeRange { get; } = 
    [
        new() { Value = "0", Text = "0 to 2 years"},
        new() { Value = "1", Text = "3 to 5 years"},
        new() { Value = "2", Text = "6 to 11 years"},
        new() { Value = "3", Text = "12 to 15 years"},
        new() { Value = "4", Text = "16 to 18 years"},
        new() { Value = "5", Text = "19 to 25 years with SEND"}
    ];
    
    private static readonly Dictionary<string, int[]> AgeRangeMap = new()
    {
        { "0", [0, 2] },
        { "1", [3, 5] },
        { "2", [6, 11] },
        { "3", [12, 15] },
        { "4", [16, 18] },
        { "5", [19, AgeDisplayExtensions.TwentyFivePlus] }
    };
    
    public static List<SelectListItem> DistanceRange { get; } = 
    [
        new() { Value = "1", Text = "1 mile"},
        new() { Value = "2", Text = "2 miles"},
        new() { Value = "5", Text = "5 miles"},
        new() { Value = "10", Text = "10 miles"},
        new() { Value = "20", Text = "20 miles"}
    ];

    private static readonly int MinimumValidDistance = int.Parse(DistanceRange[0].Value);
    private static readonly int MaximumValidDistance = int.Parse(DistanceRange[^1].Value);

    public const string AllLanguagesValue = "all";

    public static SelectListItem[] LanguageOptions { get; set; }
//#pragma warning disable S2325
//    public IEnumerable<SelectListItem> LanguageOptions => StaticLanguageOptions;
//#pragma warning restore S2325

    static LocalOfferResultsModel()
    {
        LanguageOptions = Languages.FilterCodes
            .Select(c => new SelectListItem(Languages.CodeToName[c], c))
            .OrderBy(kv => kv.Text)
            .Prepend(new SelectListItem("All languages", AllLanguagesValue, true))
            .ToArray();
    }
    
    [BindProperty]
    public bool OnlyShowFreeServices { get; set; }

    [BindProperty]
    public List<string>? DaysAvailable { get; set; }

    [BindProperty]
    public List<string>? CategorySelection { get; set; }

    [BindProperty]
    public List<string>? SubcategorySelection { get; set; }
    
    [BindProperty]
    public List<string>? SelectedAges { get; set; }

    [BindProperty]
    public string? SelectedLanguage { get; set; }

    [BindProperty]
    public string Postcode { get; set; } = string.Empty;

    [BindProperty]
    public int PageNum { get; set; } = 1;

    [BindProperty]
    public Guid CorrelationId { get; set; }

    public int PageSize { get; set; } = 10;
    public IPagination Pagination { get; set; }
    public int TotalResults { get; set; }
    public string? DistrictCode { get; set; }

    public bool InitialLoad { get; set; } = true;

    public LocalOfferResultsModel(
        IPostcodeLookup postcodeLookup,
        IOrganisationClientService organisationClientService,
        ILogger<LocalOfferResultsModel> logger)
    {
        _postcodeLookup = postcodeLookup;
        _organisationClientService = organisationClientService;
        _logger = logger;
        
        Pagination = new DontShowPagination();
    }

    public async Task<IActionResult> OnGetAsync(
        string postcode,
        string? subcategorySelection,
        bool onlyShowFreeServices,
        string? daysAvailable,
        string? selectedAges,
        string? selectedLanguage,
        string? selectedDistance,
        int? pageNum,
        Guid? correlationId
        )
    {
        Postcode = postcode;

        if (correlationId is null)
        {
            CorrelationId = Guid.NewGuid();
            // If no correlation ID exists, then treat this search as a
            // new search.
            _isInitialSearch = true;
        }
        else
        {
            CorrelationId = correlationId.Value;
            _isInitialSearch = false;
        }

        OnlyShowFreeServices = onlyShowFreeServices;
        SelectedAges = selectedAges?.Split(",").ToList();
        SelectedLanguage = selectedLanguage == AllLanguagesValue ? null : selectedLanguage;
        SelectedDistance = selectedDistance;
        PageNum = pageNum ?? 1;
        SubcategorySelection = subcategorySelection?.Split(",").ToList();
        DaysAvailable = daysAvailable?.Split(",").Where(x => Enum.TryParse(x, out DayCode _)).ToList();

        await GetLocationDetails(Postcode);

        //todo: it does this every request!
        await GetCategoriesTreeAsync();

        DateTime requestTimestamp = DateTime.UtcNow;
        HttpResponseMessage? response = await SearchServices();
        DateTime? responseTimestamp = DateTime.UtcNow;

        try
        {
            if (Postcode is not null)
            {
                // If the user is coming from the initial postcode search page,
                // FromPostCodeSearch will be true, and we can use this to differentiate
                // between initial searches, and subsequent search query changes.
                var eventType = _isInitialSearch ? ServiceDirectorySearchEventType.ServiceDirectoryInitialSearch
                    : ServiceDirectorySearchEventType.ServiceDirectorySearchFilter;

                FamilyHubsUser familyHubsUser = HttpContext.GetFamilyHubsUser();
                
                await _organisationClientService.RecordServiceSearch(
                    eventType,
                    Postcode,
                    long.TryParse(familyHubsUser.AccountId, out var familyHubsUserId) ? familyHubsUserId : null,
                    SearchResults.Items,
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

    private int ConvertSelectedDistanceToMeters()
    {
        bool isInteger = int.TryParse(SelectedDistance, out int distanceInMiles);
        bool isWithinValidRange = distanceInMiles >= MinimumValidDistance && distanceInMiles <= MaximumValidDistance;
        
        if (isInteger && isWithinValidRange) return DistanceConverter.MilesToMeters(distanceInMiles);
        
        _logger.LogWarning("Selected distance has an unexpected value: {SelectedDistance}", SelectedDistance);
        
        SelectedDistance = null;
        
        const int maxPracticalDistanceInMeters = 212892;
        return maxPracticalDistanceInMeters;
    }
    
    private List<int[]>? GetAgeRangeList()
    {
        if (SelectedAges is null || SelectedAges.Count == 0) return null;

        List<int[]> ageRangeList = [];

        foreach (string selectedAge in SelectedAges)
        {
            bool isContained = AgeRangeMap.TryGetValue(selectedAge, out int[]? ageRange);
            if (!isContained || ageRange is null)
            {
                _logger.LogWarning("Selected ages has an unexpected value: {SelectedAge}", SelectedAges);
                SelectedAges = null;
                return null;
            }
            ageRangeList.Add(ageRange);
        }

        return ageRangeList;
    }

    private async Task<HttpResponseMessage?> SearchServices()
    {
        var localOfferFilter = new LocalOfferFilter
        {
            CanFamilyChooseLocation = false,
            ServiceType = "InformationSharing",
            Status = "Active",
            PageSize = PageSize,
            IsPaidFor = OnlyShowFreeServices ? false : null,
            PageNumber = PageNum,
            Text = null,
            DistrictCode = DistrictCode ?? null,
            Latitude = CurrentLatitude,
            Longitude = CurrentLongitude,
            Proximity = ConvertSelectedDistanceToMeters(),
            AgeRangeList = GetAgeRangeList(),
            TaxonomyIds = SubcategorySelection is not null && SubcategorySelection.Any() ? string.Join(",", SubcategorySelection) : null,
            LanguageCode = SelectedLanguage != null && SelectedLanguage != AllLanguagesValue ? SelectedLanguage : null,
            DaysAvailable = DaysAvailable?.Any() == true ? string.Join(",", DaysAvailable) : null
        };
        
        (SearchResults, HttpResponseMessage? response) = await _organisationClientService.GetLocalOffers(localOfferFilter);
        Pagination = new LargeSetPagination(SearchResults.TotalPages, PageNum);
        TotalResults = SearchResults.TotalCount;

        return response;
    }

    public IActionResult OnPostAsync(
        bool removeFilter,
        string? removeCategories, 
        string? removeCost, 
        string? removeDaysAvailable, 
        string? removeAge,
        string? removeLanguage,
        string? removeSearchWithin)
    {
        var routeValues = ToRouteValuesWithRemovedFilters(
            removeFilter, removeCategories,
            removeCost, removeDaysAvailable, removeAge, removeLanguage, removeSearchWithin);

        InitialLoad = false;
        ModelState.Clear();

        return RedirectToPage("/ProfessionalReferral/LocalOfferResults", routeValues);
    }

    private dynamic ToRouteValuesWithRemovedFilters(
        bool removeFilter,
        string? removeCategories, 
        string? removeCost, 
        string? removeDaysAvailable, 
        string? removeAge,
        string? removeLanguage,
        string? removeSearchWithin)
    {
        dynamic routeValues = new ExpandoObject();
        var routeValuesDictionary = (IDictionary<string, object>)routeValues;

        foreach (var keyValuePair in Request.Form.Where(f => !f.Key.Contains("__") && !f.Key.Contains("remove")))
        {
            if (removeFilter)
            {
                if (removeCategories is not null && keyValuePair.Key is nameof(SubcategorySelection))
                {
                    routeValuesDictionary[keyValuePair.Key] = string.Join(",", keyValuePair.Value.ToString()
                        .Split(",").Where(s => s != removeCategories));
                    continue;
                }

                if (removeCost is not null && keyValuePair.Key is nameof(OnlyShowFreeServices))
                {
                    continue;
                }
                
                if (removeDaysAvailable is not null && keyValuePair.Key is nameof(DaysAvailable))
                {
                    routeValuesDictionary[keyValuePair.Key] = string.Join(",", keyValuePair.Value.ToString()
                        .Split(",").Where(s => s != removeDaysAvailable));
                    continue;
                }
                
                if (removeAge is not null && keyValuePair.Key is nameof(SelectedAges))
                {
                    routeValuesDictionary[keyValuePair.Key] = string.Join(",", keyValuePair.Value.ToString()
                        .Split(",").Where(s => s != removeAge));
                    continue;
                }
                
                if (removeLanguage is not null && keyValuePair.Key is nameof(SelectedLanguage))
                {
                    continue;
                }
                
                if (removeSearchWithin is not null && keyValuePair.Key is nameof(SelectedDistance))
                {
                    continue;
                }
            }

            routeValuesDictionary[keyValuePair.Key] = keyValuePair.Value.ToString();
        }

        return routeValues;
    }

    public static string GetDeliveryMethodsAsString(ICollection<ServiceDeliveryDto> serviceDeliveries)
    {
        return serviceDeliveries.Count == 0
            ? string.Empty
            : string.Join(Environment.NewLine, serviceDeliveries.Select(serviceDelivery => serviceDelivery.Name.AsString(EnumFormat.Description)));
    }

    public static string GetAddressAsString(ServiceDto serviceDto)
    {
        return serviceDto.Locations.Count == 1
            ? string.Join(Environment.NewLine, serviceDto.Locations.First().GetAddress())
            : $"Available at {serviceDto.Locations.Count} locations";
    }

    public static string GetLanguagesAsString(ICollection<LanguageDto> languageDtos)
    {
        return languageDtos.Count == 0
            ? string.Empty
            : string.Join(Environment.NewLine, languageDtos.Select(serviceDelivery => serviceDelivery.Name));
    }

    private async Task GetLocationDetails(string postCode)
    {
        var (postcodeError, postcodeInfo) = await _postcodeLookup.Get(postCode);

        //todo: we shouldn't ignore the error, but this is what it's always done
        //todo: what we should really do is pass this info on from the postcode search page
        if (postcodeError == PostcodeError.None)
        {
            CurrentLatitude = postcodeInfo!.Latitude;
            CurrentLongitude = postcodeInfo.Longitude;
            DistrictCode = postcodeInfo.AdminArea;
        }
    }

    private async Task GetCategoriesTreeAsync()
    {
        var categories = await _organisationClientService.GetCategories();
        NestedCategories = new(categories);

        Categories = new();

        foreach (var category in NestedCategories)
        {
            Categories.Add(category.Key);
            foreach (var subcategory in category.Value)
                Categories.Add(subcategory);
        }
    }
}
