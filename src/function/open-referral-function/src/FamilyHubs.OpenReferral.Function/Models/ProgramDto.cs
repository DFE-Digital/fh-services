using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Models;

public class ProgramDto : BaseHsdsDto
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("alternate_name")]
    public string? AlternateName { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }
}