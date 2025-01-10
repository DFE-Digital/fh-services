
namespace FamilyHubs.SharedKernel.Razor.Dashboard;

//todo: rename to ColumnInfo? or Column, or ColumnMetadata or something, as not just header related anymore
internal class ColumnHeader : IColumnHeader
{
    private readonly ColumnImmutable _columnImmutable;
    private readonly string _pagePath;
    private readonly string? _extraQueryParams;

    public ColumnHeader(
        ColumnImmutable columnImmutable,
        SortOrder? sort,
        string pagePath,
        string? extraQueryParams = null)
    {
        Sort = sort;
        _columnImmutable = columnImmutable;
        _pagePath = pagePath;
        _extraQueryParams = extraQueryParams;

        switch (_columnImmutable.ColumnType)
        {
            case ColumnType.AlignedRight:
                HeaderClasses = CellClasses = "govuk-!-text-align-right";
                break;
            case ColumnType.Numeric:
                HeaderClasses = "govuk-table__header--numeric";
                CellClasses = "govuk-table__cell--numeric";
                break;
        }
    }

    public string ContentAsHtml
    {
        get
        {
            if (Sort == null)
            {
                return _columnImmutable.DisplayName;
            }

            SortOrder clickSort = Sort switch
            {
                SortOrder.ascending => SortOrder.descending,
                _ => SortOrder.ascending
            };

            string url = $"{_pagePath}?columnName={_columnImmutable.SortName}&sort={clickSort}";
            if (_extraQueryParams != null)
            {
                url += $"&{_extraQueryParams}";
            }
            return $"<a href = \"{url}\">{_columnImmutable.DisplayName}<span aria-hidden=\"true\"></span></a>";
        }
    }

    public SortOrder? Sort { get; }

    public string? HeaderClasses { get; }
    public string? CellClasses { get; }
}
