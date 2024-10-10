using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Mock_Hsda.Api.MockResponseGenerators;

public class MockDbContext(DbContextOptions<MockDbContext> options) : DbContext(options)
{
    public DbSet<MockResponse> MockResponses { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MockResponse>(mockResponse =>
        {
            mockResponse.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
        });
    }
}

// curl "https://localhost:7298/services?page=2&per_page=10" -H "accept: application/json" -H "X-Mock-Response-Id: Pagination"
// curl -w "\nHTTP Status Code: %{http_code}\n" "https://localhost:7298/services" -H "accept: application/json" -H "X-Mock-Response-Id: 500"

public record MockResponse(
    int Id,
    string OperationName,
    string? ScenarioName = null,
    string? PathParams = null,
    string? QueryParams = null,
    int StatusCode = 200,
    //todo: either the direct json response, or for lists, an array and we handle the paging with code
    string? ResponseBody = "");

/// <summary>
/// A generic mock response generator that gets the mocked responses from a DB
/// </summary>
public class DbMockResponseGenerator(MockDbContext context) : IMockResponseGenerator
{
    public async Task<(int, string?)> GetMockResponseAsync(
        string operationName, string? scenarioName, string? pathParams, string? queryParams)
    {
        var response = await context.MockResponses
            .Where(r => r.OperationName == operationName &&
                (scenarioName != null ? r.ScenarioName == scenarioName : r.ScenarioName == null) &&
                (pathParams != null ? r.PathParams == pathParams : r.PathParams == null) &&
                (queryParams != null ? r.QueryParams == queryParams : r.QueryParams == null))
            .FirstOrDefaultAsync();

        if (response == null)
        {
            return (404, "");
        }

        return (response.StatusCode, response.ResponseBody);
    }
}
