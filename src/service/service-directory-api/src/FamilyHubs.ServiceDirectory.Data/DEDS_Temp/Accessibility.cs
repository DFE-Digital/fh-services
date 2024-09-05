using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Accessibility
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("location_id")]
    public Guid? LocationId { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("details")]
    public string? Details { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; set; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; set; } = new();
}