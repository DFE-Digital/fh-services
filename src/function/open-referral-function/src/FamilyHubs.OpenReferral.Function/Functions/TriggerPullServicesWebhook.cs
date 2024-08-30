using System.Net;
using System.Text.Json;
using FamilyHubs.OpenReferral.Function.ClientServices;
using FamilyHubs.OpenReferral.Function.Entities;
using FamilyHubs.OpenReferral.Function.Repository;
using FamilyHubs.ServiceDirectory.Data.Entities.Staging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;

namespace FamilyHubs.OpenReferral.Function.Functions;

public class TriggerPullServicesWebhook(
    ILogger<TriggerPullServicesWebhook> logger,
    IHsdaApiService hsdaApiService,
    IFunctionDbContext functionDbContext)
{
    [Function("TriggerPullServicesWebhook")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "POST")] HttpRequestData req)
    {
        logger.LogInformation("[ApiReceiver] HTTP Trigger Function Started");

        (HttpStatusCode httpStatusCode, JsonElement.ArrayEnumerator? services) = await hsdaApiService.GetServices();

        if (httpStatusCode != HttpStatusCode.OK) return req.CreateResponse(httpStatusCode);

        List<ServiceJson> servicesById = await hsdaApiService.GetServicesById(services!.Value);

        if (servicesById.Count == 0)
        {
            logger.LogInformation("Getting the services by ID returned no results!");
            return req.CreateResponse(HttpStatusCode.NotFound);
        }

        try
        {
            await UpdateDatabase(servicesById);
        }
        catch (Exception e)
        {
            logger.LogError("{exception}", e.Message);
            return req.CreateResponse(HttpStatusCode.InternalServerError);
        }

        return req.CreateResponse(HttpStatusCode.OK);
    }

    private async Task UpdateDatabase(List<ServiceJson> serviceJsonList)
    {
        logger.LogInformation("Truncating database before inserting services..");
        await functionDbContext.TruncateServicesTempAsync();

        foreach (ServiceJson serviceJson in serviceJsonList)
        {
            logger.LogInformation("Adding service with ID {serviceId} to the database..", serviceJson.Id);
            functionDbContext.AddServiceTemp(new ServicesTemp
            {
                Id = Guid.Parse(serviceJson.Id),
                Json = serviceJson.Json,
                LastModified = DateTime.UtcNow
            });
        }

        logger.LogInformation("Saving changes to the database..");
        await functionDbContext.SaveChangesAsync();
    }
}