using System.Net.Http.Headers;

namespace FamilyHubs.ServiceDirectory.Api.AcceptanceTests.Builders.Http;

public class HttpRequestBuilder(HttpMethod method)
{
    private string? _path;
    private string? _baseUrl;
    private HttpContent? _content;
    private string? _bearerToken;
    private Dictionary<string, string>? _parameters = null;
    private Dictionary<string, string>? _headers = null;

    public HttpRequestBuilder AddRequestUri(string baseUrl, string requestUri)
    {
        _baseUrl = baseUrl;
        _path = requestUri;

        return this;
    }

    public HttpRequestBuilder AddParameters(Dictionary<string, string> parameters)
    {
        _parameters = parameters;
        return this;
    }

    public HttpRequestBuilder AddContent(HttpContent content)
    {
        _content = content;
        return this;
    }

    public HttpRequestBuilder AddCustomHeaders(Dictionary<string, string> headers)
    {
        _headers = headers;
        return this;
    }

    public HttpRequestBuilder AddBearerToken(string bearerToken)
    {
        _bearerToken = bearerToken;
        return this;
    }

    public async Task<HttpResponseMessage> SendAsync()
    {
        //Create the request message based on the request in the builder
        var request = new HttpRequestMessage
        {
            Method = method,
            RequestUri = new Uri($"{_baseUrl}{_path}")
        };

        //Add parameters to Uri
        if (_parameters is not null)
        {
            request.RequestUri = new Uri($"{_baseUrl}{_path}?{CreateQueryString(_parameters)}");
        }


        //Add any custom headers
        if (_headers != null)
        {
            foreach (var header in _headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }


        //Add content if present in the request
        if (_content != null)
        {
            request.Content = _content;
        }

        //Add bearer token if present in the request
        if (!string.IsNullOrEmpty(_bearerToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
        }

        //Creates or Gets an existing HttpClient for the BaseUrl being used
        var httpClient = HttpClientFactory.GetHttpClientInstance(_baseUrl ?? "");

        return await httpClient.SendAsync(request);
    }

    //Creates a querystring. E.g. dictKey1=dictValue1&dictKey2=dictValue2
    private static string CreateQueryString(IDictionary<string, string> dict)
    {
        var list = new List<string>();
        foreach (var item in dict)
        {
            list.Add($"{item.Key}={item.Value}");
        }

        return string.Join("&", list);
    }
}