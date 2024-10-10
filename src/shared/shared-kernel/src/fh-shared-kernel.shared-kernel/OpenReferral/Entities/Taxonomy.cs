using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Taxonomy : BaseHsdsEntity
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("uri")]
    public string? Uri { get; init; }

    [JsonPropertyName("version")]
    public string? Version { get; init; }

    [JsonPropertyName("metadata")]
    public List<Metadata> Metadata { get; init; } = new();
}