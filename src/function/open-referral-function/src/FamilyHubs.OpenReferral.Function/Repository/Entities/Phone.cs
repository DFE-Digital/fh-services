using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class Phone: BaseHsdsEntity
{

    [JsonPropertyName("location_id")]
    [JsonIgnore]
    public Guid? LocationId { get; init; }
    [JsonIgnore]
    public virtual Location? Location { get; init; }

    [JsonPropertyName("service_id")]
    [JsonIgnore]
    public Guid? ServiceId { get; init; }
    [JsonIgnore]
    public virtual Service? Service { get; init; }

    [JsonPropertyName("organization_id")]
    [JsonIgnore]
    public Guid? OrganizationId { get; init; }
    [JsonIgnore]
    public virtual Organization? Organization { get; init; }

    [JsonPropertyName("contact_id")]
    [JsonIgnore]
    public Guid? ContactId { get; init; }
    [JsonIgnore]
    public virtual Contact? Contact { get; init; }

    [JsonPropertyName("service_at_location_id")]
    [JsonIgnore]
    public Guid? ServiceAtLocationId { get; init; }
    [JsonIgnore]
    public virtual ServiceAtLocation? ServiceAtLocation { get; init; }

    [JsonPropertyName("number")]
    public required string Number { get; init; }

    [JsonPropertyName("extension")]
    public short? Extension { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("languages")]
    public virtual List<Language> Languages { get; init; } = new();

    [JsonPropertyName("attributes")]
    public virtual List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public virtual List<Metadata> Metadata { get; init; } = new();
}