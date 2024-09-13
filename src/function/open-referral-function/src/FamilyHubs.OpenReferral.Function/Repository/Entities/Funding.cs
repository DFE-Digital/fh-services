using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class Funding : BaseHsdsEntity
{
    [JsonPropertyName("organization_id")]
    [JsonIgnore]
    public Guid? OrganizationId { get; init; }

    [JsonPropertyName("service_id")]
    [JsonIgnore]
    public Guid? ServiceId { get; init; }

    [JsonPropertyName("source")] public string? Source { get; init; }

    [JsonPropertyName("attributes")] public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")] public List<Metadata> Metadata { get; init; } = new();
}