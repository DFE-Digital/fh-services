using System.Net;
using System.Text.Json;
using FamilyHubs.SharedKernel.OpenReferral.Entities;
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
            logger.LogWarning("Query was OK, but no services were found");
            return (HttpStatusCode.NoContent, null);
        }

        logger.LogInformation("Found {serviceCount} service(s)", serviceList.Count());

        return (HttpStatusCode.OK, serviceList);
    }

    public async Task<(HttpStatusCode, List<Service>)> GetServicesById(JsonElement.ArrayEnumerator services)
    {
        List<Service> servicesById = [];

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

            try
            {
                Service? service = JsonSerializer.Deserialize<Service>(jsonResponse!);

                if (service is null)
                {
                    logger.LogWarning(
                        "After attempting to deserialise the incoming JSON, the Service is null | JSON = {jsonResponse}",
                        jsonResponse);
                    continue;
                }

                servicesById.Add(service);
            }
            catch (Exception e)
            {
                logger.LogWarning(e,
                    "Incoming JSON cannot be deserialised into a type of Service | JSON = {jsonResponse}",
                    jsonResponse);
            }
        }

        bool gotResults = servicesById.Count > 0;

        if (!gotResults) logger.LogWarning("Getting Services by ID returned no results");

        return (gotResults ? HttpStatusCode.OK : HttpStatusCode.NoContent, servicesById);
    }

    private async Task<(HttpStatusCode, string?)> HttpGet(string endpoint)
    {
        using HttpResponseMessage response = await httpClient.GetAsync(endpoint);
        logger.LogInformation("GET {endpoint} | Status Code = {httpStatusCode}", endpoint, response.StatusCode);
        return (response.StatusCode,
            response.StatusCode == HttpStatusCode.OK ? await response.Content.ReadAsStringAsync() : null);
    }
}