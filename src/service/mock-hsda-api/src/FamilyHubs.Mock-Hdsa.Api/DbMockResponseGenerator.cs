using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Mock_Hdsa.Api;

public class MockDbContext : DbContext
{
    public MockDbContext(DbContextOptions<MockDbContext> options) : base(options) { }

    public DbSet<MockResponse> MockResponses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MockResponse>(mockResponse =>
        {
            mockResponse.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
        });

        // Seed initial data
        modelBuilder.Entity<MockResponse>().HasData(
        new MockResponse (1, "getAPIMetaInformation", ResponseBody: "{\r\n  \"version\": \"3.0\",\r\n  \"profile\": \"https://todo/put/our/profile/uri/here\",\r\n  \"openapi_url\": \"https://raw.githubusercontent.com/openreferral/specification/3.0/schema/openapi.json\"\r\n}\r\n"),
        new MockResponse (2, "getFullyNestedServiceById"),
        new MockResponse (3, "getPaginatedListOfServices"),
        new MockResponse (4, "getTaxonomyById"),
        new MockResponse (5, "getPaginatedListOfTaxonomies"),
        new MockResponse (6, "getPaginatedListOfTaxonomyTerms"),
        new MockResponse (7, "getTaxonomyTermById"),
        new MockResponse (8, "getOrganizationById"),
        new MockResponse (9, "getPaginatedListOfOrganizations"),
        new MockResponse (10, "getServiceAtLocationWithNestedDataById"),
        new MockResponse (11, "getPaginatedListOfServiceAtLocation")
        );
    }
}

public record MockResponse(
    int Id,
    string OperationName,
    string? ScenarioName = null,
    string? PathParams = null,
    string? QueryParams = null,
    int StatusCode = 200,
    //todo: either the direct json response, or for lists, an array and we handle the paging with code
    string? ResponseBody = "");
//{
//    public int Id { get; set; }
//    public string OperationName { get; set; }
//    public string? ScenarioName { get; set; }
//    //todo: think operationname is enough, don't need this as well
//    //public string PathTemplate { get; set; }
//    public string? PathParams { get; set; }
//    public string? QueryParams { get; set; }
//    //todo: either the direct json response, or for lists, an array and we handle the paging with code
//    public int StatusCode { get; set; }
//    public string? ResponseBody { get; set; }
//}

public class DbMockResponseGenerator(MockDbContext context)
{
    public async Task<(int, string?)> GetMockResponseAsync(
        string operationName, string? scenarioName, string pathTemplate, string? pathParams, string? queryParams)
    {
        var response = await context.MockResponses
            .Where(r => r.OperationName == operationName &&
                        (r.ScenarioName == scenarioName || r.ScenarioName == null) &&
                        //sr.PathTemplate == pathTemplate &&
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
