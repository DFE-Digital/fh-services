using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class Attribute
{
    [JsonIgnore]
    public Guid Id { get; init; }

    [JsonPropertyName("id")]
    public required Guid OrId { get; init; }

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
    public virtual TaxonomyTerm? TaxonomyTerm { get; init; }

    [JsonPropertyName("metadata")]
    public virtual List<Metadata> Metadata { get; init; } = new();
}