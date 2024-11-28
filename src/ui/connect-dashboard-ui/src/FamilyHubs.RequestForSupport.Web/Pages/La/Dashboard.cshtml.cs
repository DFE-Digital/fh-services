using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.ReferralService.Shared.Enums;
using FamilyHubs.ReferralService.Shared.Models;
using FamilyHubs.RequestForSupport.Core.ApiClients;
using FamilyHubs.RequestForSupport.Web.LaDashboard;
using FamilyHubs.RequestForSupport.Web.Models;
using FamilyHubs.RequestForSupport.Web.Pages.Shared;
using FamilyHubs.RequestForSupport.Web.Security;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Identity.Models;
using FamilyHubs.SharedKernel.Razor.Dashboard;
using FamilyHubs.SharedKernel.Razor.FamilyHubsUi.Options;
using FamilyHubs.SharedKernel.Razor.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using IReferralClientService = FamilyHubs.RequestForSupport.Core.ApiClients.IReferralClientService;

namespace FamilyHubs.RequestForSupport.Web.Pages.La;

//todo: make back button remember dashboard state?
//todo: check AccountStatus on claim? is it done auto?
[Authorize(Roles = Roles.LaProfessionalOrDualRole)]
public class DashboardModel : HeaderPageModel, IDashboard<ReferralDto>
{
    private static ColumnImmutable[] _columnImmutables =
    {
        new("Contact in family", Column.ContactInFamily.ToString()),
        new("Service", Column.Service.ToString()),
        new("Date updated", Column.DateUpdated.ToString()),
        new("Date sent", Column.DateSent.ToString()),
        new("Request number"),
        new("Status", Column.Status.ToString())
    };

    private readonly IReferralClientService _referralClientService;
    private readonly FamilyHubsUiOptions _familyHubsUiOptions;
    private readonly IOrganisationClientService _organisationClientService;

    string? IDashboard<ReferralDto>.TableClass => "app-la-dashboard";

    public string Title => "My requests";
    public string SubTitle => "Connection requests sent to services";
    public string? CaptionText { get; set; }
    public IPagination Pagination { get; set; }

    public const int PageSize = 20;

    private IEnumerable<IColumnHeader> _columnHeaders = [];
    private IEnumerable<IRow<ReferralDto>> _rows = [];
    IEnumerable<IColumnHeader> IDashboard<ReferralDto>.ColumnHeaders => _columnHeaders;
    IEnumerable<IRow<ReferralDto>> IDashboard<ReferralDto>.Rows => _rows;

    public DashboardModel(
        IReferralClientService referralClientService,
        IOptions<FamilyHubsUiOptions> familyHubsUiOptions,
        IOrganisationClientService organisationClientService)
    {
        _referralClientService = referralClientService;
        _familyHubsUiOptions = familyHubsUiOptions.Value;
        _organisationClientService = organisationClientService;
        Pagination = IPagination.DontShow;
    }

    public async Task OnGet(string? columnName, SortOrder sort, int? currentPage = 1)
    {
        var user = HttpContext.GetFamilyHubsUser();
        await SetPaginationResults(user, columnName, sort, currentPage);
        
        CaptionText = await GetLocalAuthorityName(user);
    }

    private async Task SetPaginationResults(FamilyHubsUser user, string? columnName, SortOrder sort, int? currentPage)
    {
        if (columnName == null || !Enum.TryParse(columnName, true, out Column column))
        {
            // default when first load the page, or user has manually changed the url
            column = Column.DateUpdated;
            sort = SortOrder.descending;
        }

        var thisWebBaseUrl = _familyHubsUiOptions.Url(UrlKeys.ThisWeb);
        var laDashboardUrl = $"{thisWebBaseUrl}La/Dashboard";

        _columnHeaders = new ColumnHeaderFactory(_columnImmutables, laDashboardUrl, column.ToString(), sort)
            .CreateAll();

        
        var searchResults = await GetConnections(user.AccountId, currentPage!.Value, column, sort);

        _rows = searchResults.Items.Select(r => new LaDashboardRow(r, thisWebBaseUrl));

        Pagination = new LargeSetLinkPagination<Column>(laDashboardUrl, searchResults.TotalPages, currentPage.Value, column, sort);
    }
    
    private async Task<string> GetLocalAuthorityName(FamilyHubsUser familyHubsUser)
    {
        var parseOrgId = long.TryParse(familyHubsUser.OrganisationId, out var organisationId);
        if (!parseOrgId)
        {
            throw new InvalidOperationException($"Could not parse OrganisationId from claim: {organisationId}");
        }

        var org = await _organisationClientService.GetOrganisationDtoByIdAsync(organisationId);
        return org?.Name ?? "";
    }

    private async Task<PaginatedList<ReferralDto>> GetConnections(
        string laProfessionalAccountId,
        int currentPage,
        Column column,
        SortOrder sort)
    {
        var referralOrderBy = column switch
        {
            Column.ContactInFamily => ReferralOrderBy.RecipientName,
            Column.Service => ReferralOrderBy.ServiceName,
            Column.DateUpdated => ReferralOrderBy.DateUpdated,
            Column.DateSent => ReferralOrderBy.DateSent,
            Column.Status => ReferralOrderBy.Status,
            _ => throw new InvalidOperationException($"Unexpected sort column {column}")
        };

        return await _referralClientService.GetRequestsByLaProfessional(
            laProfessionalAccountId, referralOrderBy, sort == SortOrder.ascending, currentPage, PageSize);
    }
}