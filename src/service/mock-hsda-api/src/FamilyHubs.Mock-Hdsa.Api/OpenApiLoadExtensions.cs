using Microsoft.OpenApi.Readers;

namespace FamilyHubs.Mock_Hdsa.Api;

public static class OpenApiLoadExtensions
{
    public static async Task AddOpenApiSpec(this IServiceCollection services, string requestUri)
    {
        //todo: get from file instead
        // Load the OpenAPI document
        using var httpClient = new HttpClient();
        var openApiJson = await httpClient.GetStringAsync(requestUri);
        var reader = new OpenApiStringReader();
        var openApiDoc = reader.Read(openApiJson, out var diagnostic);

        services.AddSingleton(openApiDoc);
    }
}