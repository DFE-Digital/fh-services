using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Taxonomy
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("uri")]
    public string? Uri { get; set; }

    [JsonPropertyName("version")]
    public string? Version { get; set; }

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; set; } = new();
}