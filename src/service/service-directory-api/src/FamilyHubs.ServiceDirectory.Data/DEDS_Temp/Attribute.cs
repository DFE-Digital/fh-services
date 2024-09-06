using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Attribute
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("link_id")]
    [JsonIgnore]
    public Guid LinkId { get; init; }

    [JsonPropertyName("taxonomy_term_id")]
    [JsonIgnore]
    public Guid TaxonomyTermId { get; init; }

    [JsonPropertyName("link_type")]
    public string? LinkType { get; init; }

    [JsonPropertyName("link_entity")]
    public required string LinkEntity { get; init; }

    [JsonPropertyName("value")]
    public string? Value { get; init; }

    [JsonPropertyName("taxonomy_term")]
    [NotMapped]
    public TaxonomyTerm? TaxonomyTerm { get; init; }

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; init; } = new();
}