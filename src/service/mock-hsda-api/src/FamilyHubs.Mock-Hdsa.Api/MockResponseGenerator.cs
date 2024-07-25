using Microsoft.OpenApi.Models;

namespace FamilyHubs.Mock_Hdsa.Api;

public class MockResponseGenerator()
{
    public (int statusCode, string responseBody) GenerateResponse(OpenApiOperation operation, string customHeaderValue)
    {
        //todo: load response from db according to operation and customHeaderValue
        //todo: return data according to request params, such as embedding flags, page info, format etc.

        // Generate a mock response based on the operation and custom header value
        // For simplicity, we'll just return a 200 OK response with a simple JSON payload
        return (200, "{\"message\": \"Hello, world!\"}");
    }
}