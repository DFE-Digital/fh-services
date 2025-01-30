using System.Net;
using System.Text.Json;
using FamilyHubs.OpenReferral.Function.ClientServices;
using FamilyHubs.OpenReferral.Function.Repository;
using FamilyHubs.SharedKernel.Factories;
using FamilyHubs.SharedKernel.OpenReferral.Entities;
using FamilyHubs.SharedKernel.Utilities;
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

        (HttpStatusCode HttpStatusCode, JsonElement.ArrayEnumerator? Result) services = await hsdaApiService.GetServices();
        if (services.HttpStatusCode != HttpStatusCode.OK) return req.CreateResponse(services.HttpStatusCode);

        (HttpStatusCode HttpStatusCode, List<Service> Result) servicesById = await hsdaApiService.GetServicesById(services.Result!.Value);
        if (servicesById.HttpStatusCode != HttpStatusCode.OK) return req.CreateResponse(servicesById.HttpStatusCode);

        try
        {
            await ClearDatabase();
            await UpdateDatabase(servicesById.Result);
        }
        catch (Exception e)
        {
            logger.LogError("{exception}", e.Message);
            return req.CreateResponse(HttpStatusCode.InternalServerError);
        }

        return req.CreateResponse(HttpStatusCode.OK);
    }

    /*
     * As clearing the Db is a temporary measure until we implement checking for existing IDs, I think this is OK even
     * if it's the slower way of doing it.
     */
    private async Task ClearDatabase()
    {
        logger.LogInformation("Removing all services from the database");
        var serviceListFromDb = await functionDbContext.ToListAsync(functionDbContext.Services());

        foreach (var service in serviceListFromDb)
        {
            logger.LogInformation("Removing service from the database, Internal ID = {iId} | Open Referral ID = {oId}",
                service.Id, service.OrId);
            functionDbContext.DeleteService(service);
        }

        await functionDbContext.SaveChangesAsync();
    }

    private async Task UpdateDatabase(List<Service> serviceListFromApi)
    {
        foreach (var service in serviceListFromApi)
        {
            var hasProfanity = ProfanityChecker.HasProfanity(service);
            if (hasProfanity)
            {
                logger.LogWarning("Service with OR ID {OrId} contains profanity and will not be added to the database", service.OrId);
                continue;
            }
            
            var textSanitizer = SanitizerFactory.CreateDedsTextSanitizer();
            logger.LogInformation("Sanitizing service with ID {serviceId}", service.OrId);
            var sanitizedService = textSanitizer.Sanitize(service);
            
            
            
            logger.LogInformation("Adding service with ID {serviceId} to the database", service.OrId);
            functionDbContext.AddService(sanitizedService);
        }

        logger.LogInformation("Saving changes to the database");
        await functionDbContext.SaveChangesAsync();
    }
}