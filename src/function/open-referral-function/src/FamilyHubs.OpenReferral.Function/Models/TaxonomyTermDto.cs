using System.Text.Json.Serialization;
using FamilyHubs.SharedKernel.OpenReferral.Converters;

namespace FamilyHubs.OpenReferral.Function.Models;

public class TaxonomyTermDto : BaseHsdsDto
{
    [JsonPropertyName("code")]
    public required string Code { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("parent_id")]
    [JsonConverter(typeof(StringToNullableTypeConverter))]
    public Guid? ParentId { get; init; }

    [JsonPropertyName("taxonomy")]
    public string? Taxonomy { get; init; }

    [JsonPropertyName("language")]
    public string? Language { get; init; }

    [JsonIgnore]
    public Guid? TaxonomyId { get; init; }
    [JsonPropertyName("taxonomy_detail")]
    public TaxonomyDto? TaxonomyDetail { get; init; }

    [JsonPropertyName("term_uri")]
    public string? TermUri { get; init; }
}