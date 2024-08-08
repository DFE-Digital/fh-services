using System.Collections.Specialized;
using System.Text.Json;
using System.Web;

namespace FamilyHubs.Mock_Hdsa.Api.MockResponseGenerators;

public class PaginatedResult
{
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public int PageNumber { get; set; }
    public int Size { get; set; }
    public bool FirstPage { get; set; }
    public bool LastPage { get; set; }
    public bool Empty { get; set; }
    public List<dynamic> Contents { get; set; }
}

/// <summary>
/// Builds on the generic DB mock response generator but adds custom handling for HSDA paging
/// </summary>
public class HsdaPagingMockResponseGenerator : IMockResponseGenerator
{
    private readonly DbMockResponseGenerator _dbMockResponseGenerator;

    public HsdaPagingMockResponseGenerator(DbMockResponseGenerator dbMockResponseGenerator)
    {
        _dbMockResponseGenerator = dbMockResponseGenerator;
    }

    //todo: reorder path params from db to be in alphabetical order, so 
    public async Task<(int, string?)> GetMockResponseAsync(string operationName, string? scenarioName, string? pathParams, string? queryParams)
    {
        bool isListOperation = operationName.StartsWith("getPaginatedListOf");

        int page = 0, perPage = 0;

        if (isListOperation)
        {
            // if page and per_page have been supplied, take them out of queryParams used for the db search
            // as we'll handle the paging ourselves
            if (!string.IsNullOrEmpty(queryParams))
            {
                var queryStringParams = HttpUtility.ParseQueryString(queryParams);

                if (int.TryParse(queryStringParams["page"], out page)
                    && int.TryParse(queryStringParams["per_page"], out perPage))
                {
                    queryParams = CreateQueryStringWithoutPageParams(queryStringParams);
                    if (queryParams == "")
                    {
                        queryParams = null;
                    }
                }
            }
        }

        (int statusCode, string? responseBody) = await _dbMockResponseGenerator.GetMockResponseAsync(
            operationName, scenarioName, pathParams, queryParams);

        //todo: handle page and/or perPage being 0
        if (isListOperation && page != 0 && perPage != 0 && responseBody != null)
        {
            responseBody = GetPagedResultJson(responseBody, page, perPage);
        }

        return (statusCode, responseBody);
    }

    private static string CreateQueryStringWithoutPageParams(NameValueCollection queryStringParams)
    {
        // Exclude "page" and "per_page" and sort the keys
        var filteredParams = queryStringParams.AllKeys
            .Where(key => key != "page" && key != "per_page")
            .OrderBy(key => key)
            .SelectMany(key => queryStringParams.GetValues(key)!
                .Select(value => $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(value)}"));

        // Join the key-value pairs into a query string
        return string.Join("&", filteredParams);
    }

    private string GetPagedResultJson(string jsonString, int page, int perPage)
    {
        var result = GetPagedResult(jsonString, page, perPage);
        return JsonSerializer.Serialize(result, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }

    private PaginatedResult GetPagedResult(string jsonString, int page, int perPage)
    {
        // Parse the JSON document
        var jsonDoc = JsonDocument.Parse(jsonString);

        // Extract the contents array
        var contents = jsonDoc.RootElement.GetProperty("contents").EnumerateArray().ToList();

        // Calculate total items and pages
        int totalItems = contents.Count;
        int totalPages = (int)Math.Ceiling(totalItems / (double)perPage);

        // Determine if the requested page is out of range
        if (page < 1 || page > totalPages)
        {
            //todo: what does the spec say about responses in this situation?
            throw new ArgumentOutOfRangeException(nameof(page), "Page number is out of range.");
        }

        // Get the items for the requested page
        //var pagedContents = contents.Skip((page - 1) * perPage).Take(perPage).ToList();

        var pagedContents = contents.Skip((page - 1) * perPage).Take(perPage)
            .Select(x => JsonSerializer.Deserialize<dynamic>(x.GetRawText()))
            .ToList();

        // Create the paginated result
        var result = new PaginatedResult
        {
            TotalItems = totalItems,
            TotalPages = totalPages,
            PageNumber = page,
            Size = pagedContents.Count,
            FirstPage = page == 1,
            LastPage = page == totalPages,
            Empty = pagedContents.Count == 0,
            Contents = pagedContents
        };

        return result;
    }
}