using Microsoft.OpenApi.Models;

namespace FamilyHubs.Mock_Hdsa.Api;

public static class RequestHandler
{
    public static async Task Handle(HttpContext context)
    {
        var openApiDoc = context.RequestServices.GetRequiredService<OpenApiDocument>();
        var mockGenerator = context.RequestServices.GetRequiredService<MockResponseGenerator>();

        var path = context.Request.Path.Value;
        var method = context.Request.Method;

        var pathItem = openApiDoc.Paths.FirstOrDefault(p => p.Key == path).Value;
        var operation = pathItem?.Operations.FirstOrDefault(o => o.Key.ToString().Equals(method, StringComparison.OrdinalIgnoreCase)).Value;

        if (operation == null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync("Endpoint not found in OpenAPI spec");
            return;
        }

        var customHeaderValue = context.Request.Headers["X-Mock-Response-Id"].FirstOrDefault();
        var (statusCode, responseBody) = mockGenerator.GenerateResponse(operation, customHeaderValue);

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(responseBody);
    }
}