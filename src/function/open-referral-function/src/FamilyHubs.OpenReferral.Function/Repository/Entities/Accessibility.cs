using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class Accessibility : BaseHsdsEntity
{
    [JsonPropertyName("location_id")]
    [JsonIgnore]
    public Guid? LocationId { get; init; }

    [JsonIgnore] public Location? Location { get; init; }

    [JsonPropertyName("description")] public string? Description { get; init; }

    [JsonPropertyName("details")] public string? Details { get; init; }

    [JsonPropertyName("url")] public string? Url { get; init; }

    [JsonPropertyName("attributes")] public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")] public List<Metadata> Metadata { get; init; } = new();
}