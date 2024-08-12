using FamilyHubs.Mock_Hsda.Api.MockResponseGenerators;
using Microsoft.OpenApi.Models;

namespace FamilyHubs.Mock_Hsda.Api;

public static class RequestHandler
{
    public static async Task Handle(HttpContext context)
    {
        var openApiDoc = context.RequestServices.GetRequiredService<OpenApiDocument>();
        var mockGenerator = context.RequestServices.GetRequiredService<IMockResponseGenerator>();

        var path = context.Request.Path.Value;
        var method = context.Request.Method;

        var (pathTemplate, pathParameters) = FindMatchingPathTemplate(openApiDoc, path!);
        if (pathTemplate == null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync("Endpoint not found in OpenAPI spec");
            return;
        }

        var operation = openApiDoc.Paths[pathTemplate].Operations
            .FirstOrDefault(o => o.Key.ToString().Equals(method, StringComparison.OrdinalIgnoreCase)).Value;

        if (operation == null)
        {
            context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
            await context.Response.WriteAsync("Method not allowed for this endpoint");
            return;
        }

        var operationName = operation.OperationId ?? $"{method}_{pathTemplate.Replace("/", "_")}";
        string? scenarioName = context.Request.Headers["X-Mock-Response-Id"].FirstOrDefault();

        string? pathParams = pathParameters?.Any() == true
            ? string.Join("&", pathParameters.OrderBy(pp => pp.Key)
                .Select(pp => $"{pp.Key}={pp.Value}"))
            : null;

        string? queryParams = context.Request.Query.Any()
            ? string.Join("&", context.Request.Query.OrderBy(q => q.Key).Select(q => $"{q.Key}={q.Value}"))
            : null;

        var (statusCode, responseBody) = await mockGenerator.GetMockResponseAsync(operationName, scenarioName, pathParams, queryParams);

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        context.Response.Headers.Append("Access-Control-Allow-Origin", "*");

        await context.Response.WriteAsync(responseBody!);
    }

    private static (string? PathTemplate, Dictionary<string, string>? Parameters) FindMatchingPathTemplate(
        OpenApiDocument openApiDoc, string actualPath)
    {
        foreach (var path in openApiDoc.Paths.Keys)
        {
            var templateParts = path.Split('/');
            var actualParts = actualPath.Split('/');

            if (templateParts.Length != actualParts.Length)
                continue;

            var parameters = new Dictionary<string, string>();
            var isMatch = true;

            for (int i = 0; i < templateParts.Length; i++)
            {
                if (templateParts[i].StartsWith("{") && templateParts[i].EndsWith("}"))
                {
                    var paramName = templateParts[i].Trim('{', '}');
                    parameters[paramName] = actualParts[i];
                }
                else if (templateParts[i] != actualParts[i])
                {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch)
                return (path, parameters);
        }

        return (null, null);
    }
}
