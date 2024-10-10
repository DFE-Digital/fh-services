using System.Net;
using System.Text.Json;
using FamilyHubs.SharedKernel.Services.Postcode.Model;
using FamilyHubs.SharedKernel.Services.PostcodesIo;
using FamilyHubs.SharedKernel.UnitTests.Identity.TestHelpers;
using NSubstitute;

namespace FamilyHubs.SharedKernel.UnitTests.Services.Postcodesio;

public class PostcodeLookupTest
{
    private const string NotFoundResponse = "{\"status\":404,\"error\":\"Invalid postcode\"}";

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData(" \t \t")]
    public async Task Get_NullOrWhitespacePostcode_ReturnsNoPostcode(string? postcode)
    {
        var postcodeLookup = CreatePostcodesIoLookup(postcode);
        
        var (postcodeError, _) = await postcodeLookup.Get(postcode);

        Assert.Equal(PostcodeError.NoPostcode, postcodeError);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData(" \t \t")]
    public async Task Get_NullOrWhitespacePostcode_ReturnsNullPostcodeInfo(string? postcode)
    {
        var postcodeLookup = CreatePostcodesIoLookup(postcode);
        
        var (_, postcodeInfo) = await postcodeLookup.Get(postcode);

        Assert.Null(postcodeInfo);
    }

    [Theory]
    [InlineData("X")]
    [InlineData("not a postcode")]
    public async Task Get_InvalidPostcode_ReturnsInvalidPostcode(string? postcode)
    {
        var postcodeLookup = CreatePostcodesIoLookup(postcode);
        
        var (postcodeError, _) = await postcodeLookup.Get(postcode);

        Assert.Equal(PostcodeError.InvalidPostcode, postcodeError);
    }

    [Theory]
    [InlineData("X")]
    [InlineData("not a postcode")]
    public async Task Get_InvalidPostcode_ReturnsNullPostcodeInfo(string? postcode)
    {
        var postcodeLookup = CreatePostcodesIoLookup(postcode);
        
        var (_, postcodeInfo) = await postcodeLookup.Get(postcode);

        Assert.Null(postcodeInfo);
    }
    
    [Fact]
    public async Task Get_NotFoundPostcode_ReturnsPostcodeNotFound()
    {
        var postcodeLookup = CreatePostcodesIoLookup("B11BB", SerializeUnsuccessfulLookup("Postcode not found"));
        
        var (postcodeError, _) = await postcodeLookup.Get("B11BB");

        Assert.Equal(PostcodeError.PostcodeNotFound, postcodeError);
    }
    
    [Fact]
    public async Task Get_InvalidPostcode_ReturnsPostcodeInvalidPostcode()
    {
        var postcodeLookup = CreatePostcodesIoLookup("B11BB", SerializeUnsuccessfulLookup("Invalid postcode"));
        
        var (postcodeError, _) = await postcodeLookup.Get("B11BB");

        Assert.Equal(PostcodeError.InvalidPostcode, postcodeError);
    }

    [Fact]
    public async Task Get_NotFoundPostcode_ReturnsNullPostcodeInfo()
    {
        var postcodeLookup = CreatePostcodesIoLookup("B11BB");
        
        var (_, postcodeInfo) = await postcodeLookup.Get("B11BB");

        Assert.Null(postcodeInfo);
    }
    
    [Fact]
    public async Task Get_ValidPostcode_ReturnsPostcodeInfo()
    {
        var postcodeLookup = CreatePostcodesIoLookup("E1 5LX", SerializeSuccessfulLookup("E1 5LX"));
        
        var (_, postcodeInfo) = await postcodeLookup.Get("E1 5LX");

        Assert.NotNull(postcodeInfo);
    }

    private static string SerializeSuccessfulLookup(string postcode)
    {
        return JsonSerializer.Serialize(new PostcodesIoResponse
        {
            PostcodeInfo = new PostcodeInfo(postcode, 51.519437f, -0.06992f, new Codes("E09000030", "E99999999"))
        });
    }
    
    private static string SerializeUnsuccessfulLookup(string error)
    {
        return JsonSerializer.Serialize(new PostcodesIoResponse { Error = error });
    }
    
    private static PostcodesIoLookup CreatePostcodesIoLookup(string? postcode, string? resultJson = NotFoundResponse)
    {
        var httpClientFactory = Substitute.For<IHttpClientFactory>();

        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound,
            Content = new StringContent(resultJson!)
        };
        var expectedUrl = new Uri($"https://example.com/{postcode}");
            
        var client = new HttpClient(new TestHttpMessageHandler(response, expectedUrl, HttpMethod.Get))
        {
            BaseAddress = new Uri("https://example.com/")
        };

        httpClientFactory.CreateClient("postcodesio").Returns(client);

        return new PostcodesIoLookup(httpClientFactory);
    }
}