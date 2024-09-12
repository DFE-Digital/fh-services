using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class Contact: BaseHsdsEntity
{

    [JsonPropertyName("organization_id")]
    [JsonIgnore]
    public Guid? OrganizationId { get; init; }
    [JsonIgnore]
    public virtual Organization? Organization { get; init; }

    [JsonPropertyName("service_id")]
    [JsonIgnore]
    public Guid? ServiceId { get; init; }
    [JsonIgnore]
    public virtual Service? Service { get; init; }

    [JsonPropertyName("service_at_location_id")]
    [JsonIgnore]
    public Guid? ServiceAtLocationId { get; init; }
    [JsonIgnore]
    public virtual ServiceAtLocation? ServiceAtLocation { get; init; }

    [JsonPropertyName("location_id")]
    [JsonIgnore]
    public Guid? LocationId { get; init; }
    [JsonIgnore]
    public virtual Location? Location { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("department")]
    public string? Department { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("phones")]
    public virtual List<Phone> Phones { get; init; } = new();

    [JsonPropertyName("attributes")]
    public virtual List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public virtual List<Metadata> Metadata { get; init; } = new();
}