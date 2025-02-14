using System.Net;
using System.Text.Json;
using FamilyHubs.OpenReferral.Function.Models;
using FamilyHubs.SharedKernel.OpenReferral.Entities;

namespace FamilyHubs.OpenReferral.Function.ClientServices;

public interface IHsdaApiService
{
    public Task<(HttpStatusCode, JsonElement.ArrayEnumerator?)> GetServices();

    public Task<(HttpStatusCode, List<ServiceDto>)> GetServicesById(JsonElement.ArrayEnumerator services);
}