using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class TaxonomyTerm
{
    [Required]
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [Required]
    [JsonPropertyName("code")]
    public required string Code { get; set; }

    [Required]
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [Required]
    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("parent_id")]
    public Guid? ParentId { get; set; }

    [JsonPropertyName("taxonomy")]
    public string? Taxonomy { get; set; }

    [JsonPropertyName("taxonomy_detail")]
    [NotMapped]
    public Taxonomy? TaxonomyDetail { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("taxonomy_id")]
    public Guid? TaxonomyId { get; set; }

    [JsonPropertyName("term_uri")]
    public string? TermUri { get; set; }

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; set; } = new();
}