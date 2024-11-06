using System.Net.Http.Headers;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Io;
using FamilyHubs.ServiceDirectory.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FamilyHubs.ServiceDirectory.UnitTests.Web;

public abstract class BaseWebTest : IDisposable
{
    protected const string BaseUrl = "https://localhost";
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    private class MyWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureHostConfiguration(config =>
            {
                config
                    .AddInMemoryCollection(new Dictionary<string, string?>
                        {
                            { "GovUkOidcConfiguration:StubAuthentication:UseStubAuthentication", "true" },
                            { "GovUkOidcConfiguration:StubAuthentication:UseStubClaims", "true" },
                            { "GovUkOidcConfiguration:Oidc:BaseUrl", "https://example.com" }
                        }
                    );
            });

            return base.CreateHost(builder);
        }
    }

    protected BaseWebTest()
    {
        _factory = new MyWebApplicationFactory();
        _client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(Configure);
            }
        ).CreateClient(
            new WebApplicationFactoryClientOptions {
                BaseAddress = new Uri(BaseUrl),
                HandleCookies = true
            }
        );
    }

    protected virtual void Configure(IServiceCollection services)
    {
        
    }

    protected async Task<IHtmlDocument> Navigate(string uri, Action<HttpResponseMessage>? responseValidation = null)
    {
        var response = await _client.GetAsync(uri);
        responseValidation?.Invoke(response);
        return await GetDocumentAsync(response);
    }

    private static async Task<IHtmlDocument> GetDocumentAsync(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        var document = await BrowsingContext.New(Configuration.Default.WithCss()).OpenAsync(ResponseFactory, CancellationToken.None);
        return (IHtmlDocument) document;

        void ResponseFactory(VirtualResponse htmlResponse)
        {
            htmlResponse
                .Address(response.RequestMessage!.RequestUri)
                .Status(response.StatusCode);

            MapHeaders(response.Headers);
            MapHeaders(response.Content.Headers);

            htmlResponse.Content(content);

            void MapHeaders(HttpHeaders headers)
            {
                foreach (var header in headers)
                {
                    foreach (var value in header.Value)
                    {
                        htmlResponse.Header(header.Key, value);
                    }
                }
            }
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        _client.Dispose();
        _factory.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
