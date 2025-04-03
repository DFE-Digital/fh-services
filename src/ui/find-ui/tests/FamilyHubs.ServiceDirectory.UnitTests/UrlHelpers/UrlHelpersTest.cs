using FamilyHubs.ServiceDirectory.Core.UrlHelpers;

namespace FamilyHubs.ServiceDirectory.UnitTests.UrlHelpers;

public class UrlHelpersTest
{
    [Fact]
    private void AddOptionalQueryParams_WhenNull()
    {
        var queryParams = new Dictionary<string, string?>();
        queryParams.AddOptionalQueryParams("test", null);

        Assert.False(queryParams.ContainsKey("test"));
    }

    [Fact]
    private void AddOptionalQueryParams_WhenNotNull()
    {
        var queryParams = new Dictionary<string, string?>();
        queryParams.AddOptionalQueryParams("test", "null");

        Assert.Equal("null", queryParams["test"]);
    }
    
    [Fact]
    private void CreateUriWithQueryString()
    {
        var queryParams = new Dictionary<string, string?>
        {
            {"null", null}
        };
        queryParams.AddOptionalQueryParams("test", ["a", "b"]);
        queryParams.AddOptionalQueryParams("example", "value");

        var result = queryParams.CreateUriWithQueryString("https://example.com/?existing=key#10");

        Assert.Equal("https://example.com/?existing=key&test=a,b&example=value#10", result);
    }
}
