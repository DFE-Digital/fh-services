using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace FamilyHubs.Mock_Hdsa.Api;

public static class OpenApiLoadExtensions
{
    public static OpenApiDocument AddOpenApiSpecFromFile(this IServiceCollection services)
    {
        // Load the OpenAPI document from a local file
        var openApiPath = Path.Combine(Directory.GetCurrentDirectory(), @"../../spec/schema/openapi.json");
        using var stream = File.OpenRead(openApiPath);
        var reader = new OpenApiStreamReader();
        var openApiDoc = reader.Read(stream, out var diagnostic);

        services.AddSingleton(openApiDoc);

        return openApiDoc;
    }
}