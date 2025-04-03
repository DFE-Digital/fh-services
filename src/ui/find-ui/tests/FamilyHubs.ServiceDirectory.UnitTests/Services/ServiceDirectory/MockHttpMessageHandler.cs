using System.Net;

namespace FamilyHubs.ServiceDirectory.UnitTests.Services.ServiceDirectory;
public class MockHttpMessageHandler(string response, HttpStatusCode statusCode) : HttpMessageHandler
{
    public string Input { get; private set; } = "";
    public int NumberOfCalls { get; private set; }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        NumberOfCalls++;

        // Could be a GET-request without a body
        if (request.Content is not null)
            Input = await request.Content.ReadAsStringAsync(cancellationToken);

        return new HttpResponseMessage
        {
            RequestMessage = request,
            StatusCode = statusCode,
            Content = new StringContent(response)
        };
    }
}