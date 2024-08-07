namespace FamilyHubs.Mock_Hdsa.Api.MockResponseGenerators;

public class HsdaPagingMockResponseGenerator : IMockResponseGenerator
{
    private readonly DbMockResponseGenerator _dbMockResponseGenerator;

    public HsdaPagingMockResponseGenerator(DbMockResponseGenerator dbMockResponseGenerator)
    {
        _dbMockResponseGenerator = dbMockResponseGenerator;
    }

    public async Task<(int, string?)> GetMockResponseAsync(string operationName, string? scenarioName, string? pathParams, string? queryParams)
    {
        var mockResponse = await _dbMockResponseGenerator.GetMockResponseAsync(operationName, scenarioName, pathParams, queryParams);

        return mockResponse;
    }
}