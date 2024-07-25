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

        //todo: how best to handle paths with params? we don't want to reinvent asp.net's routing
        //todo: handle query params
        // for individual items, we can use the id as the lookup for the response
        // for lists, do we use the header scenario name to retrieve an array of json and hand code the paging (using the incoming paging params)
        // for embedding options, do we combine the scenario name with the embedding param?
        // do we have ordered params for sub responses according to the scenario, or have custom tables for different operations/responses?
        // or one table with nullable columns or key-value params etc. and use the params applicable for the operation - could then make it generic

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