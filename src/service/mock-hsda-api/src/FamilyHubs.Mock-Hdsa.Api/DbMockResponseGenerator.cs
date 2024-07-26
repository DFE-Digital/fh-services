using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Mock_Hdsa.Api;

public static class Responses
{
    public const string GetPaginatedListOfServicesDefault = """
    {
      "total_items": 10,
      "total_pages": 10,
      "page_number": 1,
      "size": 1,
      "first_page": true,
      "last_page": false,
      "empty": false,
      "contents": [
        {
          "id": "ac148810-d857-441c-9679-408f346de14b",
          "name": "Community Counselling",
          "alternate_name": "MyCity Counselling Services",
          "description": "Counselling Services provided by trained professionals. Suitable for people with mental health conditions such as anxiety, depression, or eating disorders as well as people experiencing difficult life events and circumstances. ",
          "url": "http://example.com/counselling",
          "email": "email@example.com",
          "status": "active",
          "interpretation_services": "Interpretation services are available in Urdu, Polish, and Slovak",
          "application_process": "If you are an NHS patient please ask your GP for a referral letter, we will then be in touch with you directly. If you are not an NHS patient you should ring our reception to arrange an appointment",
          "fees_description": "Non-NHS patients are expected to pay for their counselling sessions. We charge a flat rate per hour of counselling. The current rate is \u00a350 per hour. Please see our website for up to date prices.",
          "wait_time": "wait_time",
          "fees": "fees_description",
          "accreditations": "All of our practitioners are accredited by the BASC, UKCP, and the Professional Standards Body",
          "eligibility_description": "This service is intended for all people aged 12 and over who require counselling services in the MyCity area",
          "minimum_age": 12,
          "maximum_age": 100,
          "assured_date": "2005-01-01",
          "assurer_email": "email@example.com",
          "licenses": "licences",
          "alert": "Following COVID-19 we have moved most of our counselling sessions online. Please contact the reception if you require further information.",
          "last_modified": "2023-03-15T10:30:45.123Z",
          "organization": {
            "id": "d9d5e0f5-d3ce-4f73-9a2f-4dd0ecc6c610",
            "name": "Example Organization Inc.",
            "alternate_name": "Example Org",
            "description": "Example Org is a non-profit organization dedicated to providing services to qualified beneficiaries",
            "email": "email@example.com",
            "website": "http://example.com",
            "tax_status": "tax_status",
            "year_incorporated": 2011,
            "legal_status": "Limited Company",
            "logo": "https://openreferral.org/wp-content/uploads/2018/02/OpenReferral_Logo_Green-4-1.png",
            "uri": "http://example.com",
            "parent_organization_id": "cd09a387-91f4-4555-94ec-e799c35344cd"
          },
          "program": {
            "id": "e7ec2e57-4540-43fa-b2c7-6be5a0ef7f42",
            "name": "Community Mental Health Support",
            "alternate_name": "MyCity Mental Health Group",
            "description": "Comprehensive Mental Health Services available to residents of MyCity including CBT and Counselling. This is not an emergency service and should not be used as an alternative to hospital and GP services."
          }
        }
      ]
    }
""";
}

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
        new MockResponse (3, "getPaginatedListOfServices", ResponseBody: Responses.GetPaginatedListOfServicesDefault),
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
