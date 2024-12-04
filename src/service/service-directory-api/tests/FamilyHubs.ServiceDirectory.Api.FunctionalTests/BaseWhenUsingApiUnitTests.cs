﻿using FamilyHubs.ServiceDirectory.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace FamilyHubs.ServiceDirectory.Api.FunctionalTests;

public abstract class BaseWhenUsingApiUnitTests : IDisposable
{
    protected readonly HttpClient Client;
    private readonly CustomWebApplicationFactory? _webAppFactory;
    private static string? _bearerTokenSigningKey;

    protected BaseWhenUsingApiUnitTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                new Dictionary<string, string?> {
                    {"GovUkOidcConfiguration:BearerTokenSigningKey", "StubPrivateKey123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"},
                }
            )
            .AddUserSecrets<Program>()
            .Build();

        _webAppFactory = new CustomWebApplicationFactory();
        _webAppFactory.SetupTestDatabaseAndSeedData();

        Client = _webAppFactory.CreateDefaultClient();
        Client.BaseAddress = new Uri("https://localhost:7128/");

        _bearerTokenSigningKey = configuration["GovUkOidcConfiguration:BearerTokenSigningKey"]!;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_webAppFactory != null)
        {
            using var scope = _webAppFactory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureDeleted();
        }

        Client.Dispose();
        _webAppFactory?.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Creates HttpRequestMessage
    /// </summary>
    /// <param name="content"></param>
    /// <param name="role">If left blank request will not have bearer Token</param>
    /// <param name="path"></param>
    protected HttpRequestMessage CreatePostRequest(string path, object content, string role = "")
    {
        var request = CreateHttpRequestMessage(HttpMethod.Post, path, role);
        request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        return request;
    }

    /// <summary>
    /// Creates HttpRequestMessage
    /// </summary>
    /// <param name="content"></param>
    /// <param name="role">If left blank request will not have bearer Token</param>
    /// <param name="path"></param>
    protected HttpRequestMessage CreatePutRequest(string path, object content, string role = "")
    {
        var request = CreateHttpRequestMessage(HttpMethod.Put, path, role);
        request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        return request;
    }

    /// <summary>
    /// Creates HttpRequestMessage
    /// </summary>
    /// <param name="content"></param>
    /// <param name="role">If left blank request will not have bearer Token</param>
    /// <param name="path"></param>
    protected HttpRequestMessage CreateDeleteRequest(string path, object content, string role = "")
    {
        var request = CreateHttpRequestMessage(HttpMethod.Delete, path, role);
        request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        return request;
    }

    /// <summary>
    /// Creates HttpRequestMessage
    /// </summary>
    /// <param name="path"></param>
    /// <param name="role">If left blank request will not have bearer Token</param>
    public HttpRequestMessage CreateGetRequest(string path, string role = "")
    {
        var request = CreateHttpRequestMessage(HttpMethod.Get, path, role);
        return request;
    }

    private HttpRequestMessage CreateHttpRequestMessage(HttpMethod verb, string path, string role = "")
    {
        var request = new HttpRequestMessage
        {
            Method = verb,
            RequestUri = new Uri($"{Client?.BaseAddress}{path}"),
        };

        if (!string.IsNullOrEmpty(role))
        {
            request.Headers.Add("Authorization", $"Bearer {TestDataProvider.CreateBearerToken(role, _bearerTokenSigningKey)}");
        }

        return request;
    }
}
