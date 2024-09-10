using System.Net;
using System.Text.Json;
using FamilyHubs.OpenReferral.Function.ClientServices;
using FamilyHubs.OpenReferral.Function.Repository;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using Service = FamilyHubs.OpenReferral.Function.Repository.Entities.Service;

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

        (HttpStatusCode HttpStatusCode, JsonElement.ArrayEnumerator? Result) services = await hsdaApiService.GetServices();
        if (services.HttpStatusCode != HttpStatusCode.OK) return req.CreateResponse(services.HttpStatusCode);

        (HttpStatusCode HttpStatusCode, List<Service> Result) servicesById = await hsdaApiService.GetServicesById(services.Result!.Value);
        if (servicesById.HttpStatusCode != HttpStatusCode.OK) return req.CreateResponse(servicesById.HttpStatusCode);

        try
        {
            await UpdateDatabase(servicesById.Result);
        }
        catch (Exception e)
        {
            logger.LogError("{exception}", e.Message);
            return req.CreateResponse(HttpStatusCode.InternalServerError);
        }

        return req.CreateResponse(HttpStatusCode.OK);
    }

    private async Task UpdateDatabase(List<Service> serviceList)
    {
        logger.LogInformation("Truncating database before inserting services");
        await functionDbContext.TruncateServicesTempAsync();

        foreach (Service service in serviceList)
        {
            logger.LogInformation("Adding service with ID {serviceId} to the database", service.Id);
            functionDbContext.AddService(service);
        }

        logger.LogInformation("Saving changes to the database");
        await functionDbContext.SaveChangesAsync();
    }
}