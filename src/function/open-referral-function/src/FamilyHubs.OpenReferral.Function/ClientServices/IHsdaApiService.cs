using System.Net;
using System.Text.Json;
using Service = FamilyHubs.OpenReferral.Function.Repository.Entities.Service;

namespace FamilyHubs.OpenReferral.Function.ClientServices;

public interface IHsdaApiService
{
    public Task<(HttpStatusCode, JsonElement.ArrayEnumerator?)> GetServices();

    public Task<(HttpStatusCode, List<Service>)> GetServicesById(JsonElement.ArrayEnumerator services);
}