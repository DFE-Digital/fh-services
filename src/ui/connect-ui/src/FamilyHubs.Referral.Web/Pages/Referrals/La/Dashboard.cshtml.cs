using FamilyHubs.Referral.Core.ApiClients;
using FamilyHubs.Referral.Web.Models.LaDashboard;
using FamilyHubs.Referral.Web.Pages.Shared;
using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.ReferralService.Shared.Enums;
using FamilyHubs.ReferralService.Shared.Models;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Razor.Dashboard;
using FamilyHubs.SharedKernel.Razor.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyHubs.Referral.Web.Pages.Referrals.La;

//todo: make back button remember dashboard state?
//todo: check AccountStatus on claim? is it done auto?
[Authorize(Roles = $"{RoleGroups.LaProfessionalOrDualRole}")]
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

    private readonly IReferralDashboardClientService _referralClientService;

    string? IDashboard<ReferralDto>.TableClass => "app-la-dashboard";

    public IPagination Pagination { get; set; }

    public const int PageSize = 20;

    private IEnumerable<IColumnHeader> _columnHeaders = Enumerable.Empty<IColumnHeader>();
    private IEnumerable<IRow<ReferralDto>> _rows = Enumerable.Empty<IRow<ReferralDto>>();
    IEnumerable<IColumnHeader> IDashboard<ReferralDto>.ColumnHeaders => _columnHeaders;
    IEnumerable<IRow<ReferralDto>> IDashboard<ReferralDto>.Rows => _rows;

    public DashboardModel(
        IReferralDashboardClientService referralClientService) : base(false, true)
    {
        _referralClientService = referralClientService;
        Pagination = IPagination.DontShow;
    }

    public async Task OnGet(string? columnName, SortOrder sort, int? currentPage = 1)
    {
        if (columnName == null || !Enum.TryParse(columnName, true, out Column column))
        {
            // default when first load the page, or user has manually changed the url
            column = Column.DateUpdated;
            sort = SortOrder.descending;
        }

        var laDashboardUrl = Url.Page("Dashboard");

        _columnHeaders = new ColumnHeaderFactory(_columnImmutables, laDashboardUrl, column.ToString(), sort)
            .CreateAll();

        var user = HttpContext.GetFamilyHubsUser();
        var searchResults = await GetConnections(user.AccountId, currentPage!.Value, column, sort);

        _rows = searchResults.Items.Select(r => new LaDashboardRow(r, Url.Page("RequestDetails", values: new { id = r.Id })));

        Pagination = new LargeSetLinkPagination<Column>(laDashboardUrl, searchResults.TotalPages, currentPage.Value, column, sort);
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