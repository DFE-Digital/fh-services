using System.Net;
using System.Text.Json;
using FamilyHubs.OpenReferral.Function.ClientServices;
using FamilyHubs.OpenReferral.Function.Services;
using FamilyHubs.SharedKernel.OpenReferral.PrototypeEntities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;

namespace FamilyHubs.OpenReferral.Function.Functions;

public class TriggerPullServicesWebhook(
    ILogger<TriggerPullServicesWebhook> logger,
    IHsdaApiService hsdaApiService,
    IDedsPrototypeService dedsPrototypeService)
{
    [Function("TriggerPullServicesWebhook")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "POST")] HttpRequestData req)
    {
        logger.LogInformation("[ApiReceiver] HTTP Trigger Function Started");

        (HttpStatusCode HttpStatusCode, JsonElement.ArrayEnumerator? Result) services = await hsdaApiService.GetServices();
        if (services.HttpStatusCode != HttpStatusCode.OK) return req.CreateResponse(services.HttpStatusCode);

        (HttpStatusCode HttpStatusCode, Dictionary<string,string> Result) servicesById = await hsdaApiService.GetServicesById(services.Result!.Value);
        if (servicesById.HttpStatusCode != HttpStatusCode.OK) return req.CreateResponse(servicesById.HttpStatusCode);

        try
        {
            foreach (var service in servicesById.Result)
            {
                await dedsPrototypeService.UpsertService(1, service.Key, service.Value,  StandardDocumentVersions.OpenReferralInternationalV3);
            }
        }
        catch (Exception e)
        {
            logger.LogError("{exception}", e.Message);
            return req.CreateResponse(HttpStatusCode.InternalServerError);
        }

        return req.CreateResponse(HttpStatusCode.OK);
    }
}