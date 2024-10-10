namespace FamilyHubs.Mock_Hsda.Api.MockResponseGenerators;

public interface IMockResponseGenerator
{
    Task<(int, string?)> GetMockResponseAsync(
        string operationName, string? scenarioName, string? pathParams, string? queryParams);
}