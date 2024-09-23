using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class TaxonomyTerm : BaseHsdsEntity
{
    [JsonPropertyName("code")]
    public required string Code { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("parent_id")]
    public Guid? ParentId { get; init; }

    [JsonPropertyName("taxonomy")]
    public string? Taxonomy { get; init; }

    [JsonPropertyName("language")]
    public string? Language { get; init; }

    [JsonIgnore]
    public Guid? TaxonomyId { get; init; }
    [JsonPropertyName("taxonomy_detail")]
    public Taxonomy? TaxonomyDetail { get; init; }

    [JsonPropertyName("term_uri")]
    public string? TermUri { get; init; }

    [JsonPropertyName("metadata")]
    public List<Metadata> Metadata { get; init; } = new();
}