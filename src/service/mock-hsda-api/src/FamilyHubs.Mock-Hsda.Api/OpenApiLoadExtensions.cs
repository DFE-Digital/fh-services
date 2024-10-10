using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace FamilyHubs.Mock_Hsda.Api;

public static class OpenApiLoadExtensions
{
    public static OpenApiDocument AddOpenApiSpecFromFile(this IServiceCollection services)
    {
        var openApiPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Spec", "openapi.json");
        using var stream = File.OpenRead(openApiPath);
        var reader = new OpenApiStreamReader();
        var openApiDoc = reader.Read(stream, out _);

        services.AddSingleton(openApiDoc);

        return openApiDoc;
    }
}