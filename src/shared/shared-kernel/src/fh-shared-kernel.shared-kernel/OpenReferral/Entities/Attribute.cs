using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Attribute : BaseHsdsEntity
{
    [JsonPropertyName("link_id")] public Guid? LinkId { get; init; }

    [JsonIgnore]
    public Guid? TaxonomyTermId { get; init; }
    [JsonPropertyName("taxonomy_term")]
    public TaxonomyTerm? TaxonomyTerm { get; init; }

    [JsonPropertyName("link_type")]
    public string? LinkType { get; init; }

    [JsonPropertyName("link_entity")]
    public required string LinkEntity { get; init; }

    [JsonPropertyName("value")]
    public string? Value { get; init; }

    [JsonPropertyName("metadata")]
    public List<Metadata> Metadata { get; init; } = new();
}