using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Attribute
{
    [Required]
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [Required]
    [JsonPropertyName("link_id")]
    public required Guid LinkId { get; set; }

    [Required]
    [JsonPropertyName("taxonomy_term_id")]
    public required Guid TaxonomyTermId { get; set; }

    [JsonPropertyName("link_type")]
    public string? LinkType { get; set; }

    [Required]
    [JsonPropertyName("link_entity")]
    public required string LinkEntity { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }

    [JsonPropertyName("taxonomy_term")]
    [NotMapped]
    public TaxonomyTerm? TaxonomyTerm { get; set; }

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; set; } = new();
}