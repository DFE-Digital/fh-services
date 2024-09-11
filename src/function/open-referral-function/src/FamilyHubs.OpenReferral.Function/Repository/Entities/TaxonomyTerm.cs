using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class TaxonomyTerm: BaseHSDSEntity
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

    [JsonPropertyName("version")]
    public string? Version { get; init; }

    [JsonPropertyName("taxonomy_detail")]
    public virtual Taxonomy? TaxonomyDetail { get; init; }

    [JsonPropertyName("language")]
    public string? Language { get; init; }

    [JsonPropertyName("taxonomy_id")]
    [JsonIgnore]
    public Guid? TaxonomyId { get; init; }

    [JsonPropertyName("term_uri")]
    public string? TermUri { get; init; }

    [JsonPropertyName("metadata")]
    public virtual List<Metadata> Metadata { get; init; } = new();
}