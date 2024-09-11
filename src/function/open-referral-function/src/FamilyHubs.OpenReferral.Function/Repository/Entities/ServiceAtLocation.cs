using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class ServiceAtLocation: BaseHSDSEntity
{

    [JsonPropertyName("service_id")]
    [JsonIgnore]
    public Guid? ServiceId { get; init; }

    [JsonPropertyName("location_id")]
    [JsonIgnore]
    public Guid? LocationId { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("contacts")]
    public virtual List<Contact> Contacts { get; init; } = new();

    [JsonPropertyName("phones")]
    public virtual List<Phone> Phones { get; init; } = new();

    [JsonPropertyName("schedules")]
    public virtual List<Schedule> Schedules { get; init; } = new();

    [JsonPropertyName("service")]
    public virtual Service? Service { get; init; }

    [JsonPropertyName("location")]
    public virtual Location? Location { get; init; }

    [JsonPropertyName("attributes")]
    public virtual List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public virtual List<Metadata> Metadata { get; init; } = new();
}