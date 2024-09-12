using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class Funding: BaseHsdsEntity
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

    [JsonPropertyName("source")]
    public string? Source { get; init; }

    [JsonPropertyName("attributes")]
    public virtual List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public virtual List<Metadata> Metadata { get; init; } = new();
}