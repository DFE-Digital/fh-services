namespace FamilyHubs.Mock_Hdsa.Api.MockResponseGenerators;

public class MockResponseGenerator()
{
    //public (int statusCode, string responseBody) GenerateResponse(OpenApiOperation operation, string customHeaderValue)
    public Task<(int statusCode, string responseBody)> GetMockResponseAsync(
        string operationName, string? scenarioName, string pathTemplate, string? pathParams, string? queryParams)
    {
        //todo: load response from db according to operation and customHeaderValue
        //todo: return data according to request params, such as embedding flags, page info, format etc.

        //if (operation.OperationId == "getAPIMetaInformation")
        if (operationName == "getAPIMetaInformation")
        {
            return Task.FromResult((200, "{\r\n  \"version\": \"3.0\",\r\n  \"profile\": \"https://todo/put/our/profile/uri/here\",\r\n  \"openapi_url\": \"https://raw.githubusercontent.com/openreferral/specification/3.0/schema/openapi.json\"\r\n}\r\n"));
        }

        // Generate a mock response based on the operation and custom header value
        // For simplicity, we'll just return a 200 OK response with a simple JSON payload
        return Task.FromResult((200, "{\"message\": \"Hello, world!\"}"));
    }
}