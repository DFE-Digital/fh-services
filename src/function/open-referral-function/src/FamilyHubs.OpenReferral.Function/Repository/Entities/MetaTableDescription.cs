using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class MetaTableDescription: BaseHsdsEntity
{

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("language")]
    public string? Language { get; init; }

    [JsonPropertyName("character_set")]
    public string? CharacterSet { get; init; }

    [JsonPropertyName("attributes")]
    public virtual List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public virtual List<Metadata> Metadata { get; init; } = new();
}