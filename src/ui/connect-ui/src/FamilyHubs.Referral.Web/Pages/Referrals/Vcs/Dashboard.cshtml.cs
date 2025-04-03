using FamilyHubs.Referral.Core.ApiClients;
using FamilyHubs.Referral.Web.Models.VcsDashboard;
using FamilyHubs.Referral.Web.Pages.Shared;
using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.ReferralService.Shared.Enums;
using FamilyHubs.ReferralService.Shared.Models;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Razor.Dashboard;
using FamilyHubs.SharedKernel.Razor.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FamilyHubs.SharedKernel.Identity.Models;

namespace FamilyHubs.Referral.Web.Pages.Referrals.Vcs;

//todo: make back button remember dashboard state?
//todo: most of this can go in a base class
[Authorize(Roles = $"{RoleGroups.VcsProfessionalOrDualRole}")]
public class DashboardModel : HeaderPageModel, IDashboard<ReferralDto>
{
    private static ColumnImmutable[] _columnImmutables = 
    {
        new("Contact in family", Column.ContactInFamily.ToString()),
        new("Date received", Column.DateReceived.ToString()),
        new("Request number"),
        new("Status", Column.Status.ToString())
    };

    private readonly IReferralDashboardClientService _referralClientService;
    private readonly IOrganisationClientService _organisationClientService;

    string IDashboard<ReferralDto>.TableClass => "app-vcs-dashboard";

    public IPagination Pagination { get; set; }
    public string Title => "My requests";
    public string SubTitle => "Connection requests received";
    public string? CaptionText { get; set; }
    public const int PageSize = 20;

    private IEnumerable<IColumnHeader> _columnHeaders = [];
    private IEnumerable<IRow<ReferralDto>> _rows = [];
    IEnumerable<IColumnHeader> IDashboard<ReferralDto>.ColumnHeaders => _columnHeaders;
    IEnumerable<IRow<ReferralDto>> IDashboard<ReferralDto>.Rows => _rows;

    public DashboardModel(
        IReferralDashboardClientService referralClientService,
        IOrganisationClientService organisationClientService) : base(false, true)
    {
        _referralClientService = referralClientService;
        _organisationClientService = organisationClientService;
        Pagination = IPagination.DontShow;
    }

    public async Task OnGet(string? columnName, SortOrder sort, int currentPage = 1)
    {
        var user = HttpContext.GetFamilyHubsUser();
        await SetPaginationResults(user, columnName, sort, currentPage);
        CaptionText = await GetOrganisationName(user);
    }

    private async Task SetPaginationResults(FamilyHubsUser user, string? columnName, SortOrder sort, int currentPage)
    {
        if (columnName == null|| !Enum.TryParse(columnName, true, out Column column))
        {
            // default when first load the page, or user has manually changed the url
            column = Column.DateReceived;
            sort = SortOrder.descending;
        }

        var vcsDashboardUrl = Url.Page("Dashboard");

        _columnHeaders = new ColumnHeaderFactory(_columnImmutables, vcsDashboardUrl, column.ToString(), sort)
            .CreateAll();
        
        var searchResults = await GetConnections(user.OrganisationId, currentPage, column, sort);

        _rows = searchResults.Items.Select(r => new VcsDashboardRow(r, Url.Page("RequestDetails", values: new { id = r.Id })));

        Pagination = new LargeSetLinkPagination<Column>(vcsDashboardUrl, searchResults.TotalPages, currentPage, column, sort);
    }
    
    private async Task<string> GetOrganisationName(FamilyHubsUser familyHubsUser)
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
        string organisationId,
        int currentPage,
        Column column,
        SortOrder sort)
    {
        var referralOrderBy = column switch
        {
            Column.ContactInFamily => ReferralOrderBy.RecipientName,
            //todo: check sent == received
            Column.DateReceived => ReferralOrderBy.DateSent,
            Column.Status => ReferralOrderBy.Status,
            _ => throw new InvalidOperationException($"Unexpected sort column {column}")
        };

        return await _referralClientService.GetRequestsForConnectionByOrganisationId(
            organisationId, referralOrderBy, sort == SortOrder.ascending, currentPage, PageSize);
    }
}
