using System.Net;
using System.Text.Json;
using FamilyHubs.OpenReferral.Function.Entities;

namespace FamilyHubs.OpenReferral.Function.ClientServices;

public interface IHsdaApiService
{
    public Task<(HttpStatusCode, JsonElement.ArrayEnumerator?)> GetServices();

    public Task<(HttpStatusCode, List<ServiceJson>)> GetServicesById(JsonElement.ArrayEnumerator services);
}