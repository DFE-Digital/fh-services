using System.Net;
using System.Text.Json;
using FamilyHubs.OpenReferral.Function.Entities;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.OpenReferral.Function.ClientServices;

public class HsdaApiService(ILogger<HsdaApiService> logger, HttpClient httpClient) : IHsdaApiService
{
    public async Task<(HttpStatusCode, JsonElement.ArrayEnumerator?)> GetServices()
    {
        (HttpStatusCode httpStatusCode, string? jsonResponse) = await HttpGet("/services");

        if (httpStatusCode != HttpStatusCode.OK || jsonResponse is null)
        {
            logger.LogError(
                "Failed to get the service list from /services | Status Code = {httpStatusCode}, JSON = {jsonResponse}",
                httpStatusCode, jsonResponse);
            return (httpStatusCode, null);
        }

        JsonElement.ArrayEnumerator serviceList =
            JsonDocument.Parse(jsonResponse).RootElement.GetProperty("contents").EnumerateArray();

        if (!serviceList.Any())
        {
            logger.LogInformation("Query was OK, but no services were found");
            return (HttpStatusCode.NotFound, null);
        }

        logger.LogInformation("Found {serviceCount} service(s)", serviceList.Count());

        return (HttpStatusCode.OK, serviceList);
    }

    public async Task<List<ServiceJson>> GetServicesById(JsonElement.ArrayEnumerator services)
    {
        List<ServiceJson> servicesById = [];

        foreach (string serviceId in services.Select(service => service.GetProperty("id").ToString()))
        {
            (HttpStatusCode httpStatusCode, string? jsonResponse) = await HttpGet("/services/" + serviceId);

            if (httpStatusCode != HttpStatusCode.OK)
            {
                logger.LogWarning(
                    "Failed to get /service/{serviceId} | Status Code = {httpStatusCode}, JSON = {jsonResponse}",
                    serviceId, httpStatusCode, jsonResponse);
                continue;
            }

            servicesById.Add(new ServiceJson { Id = serviceId, Json = jsonResponse! });
        }

        return servicesById;
    }

    private async Task<(HttpStatusCode, string?)> HttpGet(string endpoint)
    {
        using HttpResponseMessage response = await httpClient.GetAsync(endpoint);
        logger.LogInformation("GET {endpoint} | Status Code = {httpStatusCode}", endpoint, response.StatusCode);
        return (response.StatusCode,
            response.StatusCode == HttpStatusCode.OK ? await response.Content.ReadAsStringAsync() : null);
    }
}