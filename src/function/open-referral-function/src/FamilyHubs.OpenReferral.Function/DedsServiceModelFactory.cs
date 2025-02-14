using System.Text.Json;
using FamilyHubs.OpenReferral.Function.Models;

namespace FamilyHubs.OpenReferral.Function;

public class DedsServiceModelFactory
{
    public static ServiceDto? CreateFromInternationalSpec3(string json)
    {
        // Direct mapping as our schema is based off this for now.
        var service = JsonSerializer.Deserialize<ServiceDto>(json);
        return service;
    }
    
    public static ServiceDto? CreateFromOrUkSpec(string json)
    {
        // TODO: We will need a manual mapper for this as the UK spec is not the same as the international spec 3
        throw new NotImplementedException();
    }
}