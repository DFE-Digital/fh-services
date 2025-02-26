using System.Net;
using System.Text.Json;

namespace FamilyHubs.OpenReferral.Function.ClientServices;

public interface IHsdaApiService
{
    public Task<(HttpStatusCode, JsonElement.ArrayEnumerator?)> GetServices();

    public Task<(HttpStatusCode, Dictionary<string, string>)> GetServicesById(JsonElement.ArrayEnumerator services);
}