using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Models;

public class ServiceAreaDto  : BaseHsdsDto
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("extent")]
    public string? Extent { get; init; }

    [JsonPropertyName("extent_type")]
    public string? ExtentType { get; init; }

    [JsonPropertyName("uri")]
    public string? Uri { get; init; }
}