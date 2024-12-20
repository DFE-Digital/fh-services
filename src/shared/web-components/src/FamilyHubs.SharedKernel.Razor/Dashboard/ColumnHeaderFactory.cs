using System.Web;

namespace FamilyHubs.SharedKernel.Razor.Dashboard;

public class ColumnHeaderFactory
{
    private readonly IEnumerable<ColumnImmutable> _columnsImmutable;
    private readonly string _sortedColumnName;
    private readonly SortOrder _sort;
    private readonly string? _extraQueryParams;
    private readonly string? _pagePath;

    public ColumnHeaderFactory(
        IEnumerable<ColumnImmutable> columnsImmutable,
        string? pagePath,
        string sortedColumnName,
        SortOrder sort,
        IReadOnlyDictionary<string, string> extraQueryParams)
    : this(columnsImmutable, pagePath, sortedColumnName, sort,
        string.Join('&', extraQueryParams.Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}")))
    {
    }

    public ColumnHeaderFactory(
        IEnumerable<ColumnImmutable> columnsImmutable,
        string? pagePath,
        string sortedColumnName,
        SortOrder sort,
        string? extraQueryParams = null)
    {
        _columnsImmutable = columnsImmutable;
        _sortedColumnName = sortedColumnName;
        _sort = sort;
        _extraQueryParams = extraQueryParams;
        _pagePath = pagePath;
    }

    private IColumnHeader Create(ColumnImmutable columnImmutable)
    {
        //todo: here, or in ctor?

        SortOrder? sort = null;
        if (columnImmutable.SortName != null)
        {
            sort = columnImmutable.SortName == _sortedColumnName ? _sort : SortOrder.none;
        }

        return new ColumnHeader(columnImmutable, sort, _pagePath, _extraQueryParams);
    }

    public IColumnHeader[] CreateAll()
    {
        return _columnsImmutable.Select(Create).ToArray();
    }
}