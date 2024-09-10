using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class Funding
{
    [JsonIgnore]
    public Guid Id { get; init; }

    [JsonPropertyName("id")]
    public required Guid OrId { get; init; }

    [JsonPropertyName("organization_id")]
    [JsonIgnore]
    public Guid? OrganizationOrId { get; init; }
    [JsonIgnore]
    public virtual Organization? Organization { get; init; }

    [JsonPropertyName("service_id")]
    [JsonIgnore]
    public Guid? ServiceOrId { get; init; }
    [JsonIgnore]
    public virtual Service? Service { get; init; }

    [JsonPropertyName("source")]
    public string? Source { get; init; }

    [JsonPropertyName("attributes")]
    public virtual List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public virtual List<Metadata> Metadata { get; init; } = new();
}