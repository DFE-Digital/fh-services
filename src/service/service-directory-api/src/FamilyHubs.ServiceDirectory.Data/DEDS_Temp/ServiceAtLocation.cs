using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class ServiceAtLocation
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("service_id")]
    [JsonIgnore]
    public Guid? ServiceId { get; init; }

    [JsonPropertyName("location_id")]
    [JsonIgnore]
    public Guid? LocationId { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("contacts")]
    [NotMapped]
    public List<Contact> Contacts { get; init; } = new();

    [JsonPropertyName("phones")]
    [NotMapped]
    public List<Phone> Phones { get; init; } = new();

    [JsonPropertyName("schedules")]
    [NotMapped]
    public List<Schedule> Schedules { get; init; } = new();

    [JsonPropertyName("location")]
    [NotMapped]
    public Location? Location { get; init; }

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; init; } = new();
}