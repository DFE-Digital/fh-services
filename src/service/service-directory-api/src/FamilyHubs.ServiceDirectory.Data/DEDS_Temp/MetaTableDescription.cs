using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class MetaTableDescription
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("character_set")]
    public string? CharacterSet { get; set; }

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; set; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; set; } = new();
}