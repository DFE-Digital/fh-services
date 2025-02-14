using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Models;

public class TaxonomyDto : BaseHsdsDto
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("uri")]
    public string? Uri { get; init; }

    [JsonPropertyName("version")]
    public string? Version { get; init; }
}