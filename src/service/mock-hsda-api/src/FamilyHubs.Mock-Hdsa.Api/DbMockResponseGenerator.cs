using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Mock_Hdsa.Api;

public class MockDbContext : DbContext
{
    public MockDbContext(DbContextOptions<MockDbContext> options) : base(options) { }

    public DbSet<MockResponse> MockResponses { get; set; }
}

public class MockResponse
{
    public int Id { get; set; }
    public string OperationName { get; set; }
    public string? ScenarioName { get; set; }
    //todo: think operationname is enough, don't need this as well
    public string PathTemplate { get; set; }
    public string? PathParams { get; set; }
    public string? QueryParams { get; set; }
    //todo: either the direct json response, or for lists, an array and we handle the paging with code
    public int StatusCode { get; set; }
    public string? ResponseBody { get; set; }
}

public class DbMockResponseGenerator(MockDbContext context)
{
    public async Task<(int, string?)> GetMockResponseAsync(
        string operationName, string? scenarioName, string pathTemplate, string? pathParams, string? queryParams)
    {
        var response = await context.MockResponses
            .Where(r => r.OperationName == operationName &&
                        (r.ScenarioName == scenarioName || r.ScenarioName == null) &&
                        r.PathTemplate == pathTemplate &&
                        (r.PathParams == pathParams || r.PathParams == null) &&
                        (r.QueryParams == queryParams || r.QueryParams == null))
            .FirstOrDefaultAsync();

        //todo: what to return if no response found, 500?
        if (response == null)
        {
            return (500, null);
        }

        return (response.StatusCode, response.ResponseBody);
    }
}
