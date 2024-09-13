using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class Program : BaseHsdsEntity
{
    [JsonPropertyName("organization_id")]
    [JsonIgnore]
    public Guid? OrganizationId { get; init; }

    [JsonPropertyName("name")] public string? Name { get; init; }

    [JsonPropertyName("alternate_name")] public string? AlternateName { get; init; }

    [JsonPropertyName("description")] public string? Description { get; init; }

    [JsonPropertyName("attributes")] public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")] public List<Metadata> Metadata { get; init; } = new();
}