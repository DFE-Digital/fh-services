namespace FamilyHubs.SharedKernel.UnitTests.Identity.TestHelpers;

public sealed class TestHttpMessageHandler : HttpMessageHandler
{
    private readonly HttpResponseMessage _response;
    private readonly Uri _baseUrl;
    private readonly HttpMethod _httpMethod;

    public TestHttpMessageHandler(HttpResponseMessage response, Uri baseUrl, HttpMethod httpMethod)
    {
        _response = response;
        _baseUrl = baseUrl;
        _httpMethod = httpMethod;
    }
        
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Assert.True(ValidateMessage(request, _baseUrl, _httpMethod), "Test delegating handler not matched.");

        return Task.FromResult(_response);
    }
        
    private static bool ValidateMessage(HttpRequestMessage requestMessage, Uri baseUrl, HttpMethod httpMethod)
    {
        if (requestMessage.RequestUri is null) return false;
        if (requestMessage.RequestUri != baseUrl) return false;
        if (requestMessage.Method != httpMethod) return false;

        return true;
    }
}