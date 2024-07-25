using Microsoft.OpenApi.Models;

namespace FamilyHubs.Mock_Hdsa.Api;

public class MockResponseGenerator()
{
    public (int statusCode, string responseBody) GenerateResponse(OpenApiOperation operation, string customHeaderValue)
    {
        // Generate a mock response based on the operation and custom header value
        // For simplicity, we'll just return a 200 OK response with a simple JSON payload
        return (200, "{\"message\": \"Hello, world!\"}");
    }
}

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

        var customHeaderValue = context.Request.Headers["X-Custom-Header"].FirstOrDefault();
        var (statusCode, responseBody) = mockGenerator.GenerateResponse(operation, customHeaderValue);

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(responseBody);
    }
}