using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class ServiceAtLocation : BaseHsdsEntity
{
    [JsonPropertyName("service_id")]
    [JsonIgnore]
    public Guid? ServiceId { get; init; }

    [JsonPropertyName("location_id")]
    [JsonIgnore]
    public Guid? LocationId { get; init; }
    [JsonPropertyName("location")] public Location? Location { get; init; }

    [JsonPropertyName("description")] public string? Description { get; init; }

    [JsonPropertyName("contacts")] public List<Contact> Contacts { get; init; } = new();

    [JsonPropertyName("phones")] public List<Phone> Phones { get; init; } = new();

    [JsonPropertyName("schedules")] public List<Schedule> Schedules { get; init; } = new();

    [JsonPropertyName("attributes")] public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")] public List<Metadata> Metadata { get; init; } = new();
}