using FamilyHubs.SharedKernel.Razor.Pagination;
using System.Web;

namespace FamilyHubs.SharedKernel.Razor.Dashboard;

/// <summary>
/// Creates a GDS pagination control for use when:
/// * there are a large number of pages <see href="https://design-system.service.gov.uk/components/pagination#for-larger-numbers-of-pages"/>,
/// * the page links are links (as opposed to submit buttons)
/// * the Pagination control is being used to page through a Dashboard.
/// </summary>
/// <typeparam name="TColumn"></typeparam>
public class LargeSetLinkPagination<TColumn> : LargeSetPagination, ILinkPagination
    where TColumn : struct, Enum
{
    private readonly string? _dashboardPath;
    private readonly TColumn _column;
    private readonly SortOrder _sort;
    private readonly string? _extraQueryParams;

    public LargeSetLinkPagination(
        string dashboardPath, int totalPages, int currentPage,
        TColumn column, SortOrder sort,
        //todo: or IQueryCollection? or extra constructor
        IReadOnlyDictionary<string, string> extraQueryParams)

        : this(dashboardPath, totalPages, currentPage, column, sort,
            string.Join('&', extraQueryParams.Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}")))
    {
    }

    public LargeSetLinkPagination(
        string? dashboardPath,
        int totalPages,
        int currentPage,
        TColumn column,
        SortOrder sort,
        string? extraQueryParams = null)

        : base(totalPages, currentPage)
    {
        _dashboardPath = dashboardPath;
        _column = column;
        _sort = sort;
        _extraQueryParams = extraQueryParams;
    }

    public string GetUrl(int page)
    {
        var url = $"{_dashboardPath}?columnName={_column}&sort={_sort}&currentPage={page}";
        return _extraQueryParams == null ? url : $"{url}&{_extraQueryParams}";
    }
}