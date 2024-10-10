using System.Net;

namespace FamilyHubs.ReferralUi.UnitTests.Helpers;

public static class TestHelpers
{
    public static HttpClient GetMockClient(string content, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var mockHttpMessageHandler = new CustomHttpMessageHandler(content, statusCode);

        var client = new HttpClient(mockHttpMessageHandler)
        {
            BaseAddress = new Uri("https://localhost")
        };
        return client;
    }
    
    /// <summary>
    /// Custom HttpMessageHandler to return a response message with the content
    /// </summary>
    private class CustomHttpMessageHandler(string content, HttpStatusCode statusCode) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(content),
                RequestMessage = request
            };
        
            return Task.FromResult(responseMessage);
        }
    }
}