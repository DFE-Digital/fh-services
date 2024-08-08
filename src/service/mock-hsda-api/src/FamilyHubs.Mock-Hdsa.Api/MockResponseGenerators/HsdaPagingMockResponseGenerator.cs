using System.Text.Json;

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


public class HsdaPagingMockResponseGenerator : IMockResponseGenerator
{
    private readonly DbMockResponseGenerator _dbMockResponseGenerator;

    public HsdaPagingMockResponseGenerator(DbMockResponseGenerator dbMockResponseGenerator)
    {
        _dbMockResponseGenerator = dbMockResponseGenerator;
    }

    public async Task<(int, string?)> GetMockResponseAsync(string operationName, string? scenarioName, string? pathParams, string? queryParams)
    {
        var mockResponse = await _dbMockResponseGenerator.GetMockResponseAsync(operationName, scenarioName, pathParams, queryParams);

        if (operationName == "getPaginatedListOfServices")
        {
        }

        return mockResponse;
    }

    public PaginatedResult GetPagedResult(string jsonString, int page, int perPage)
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