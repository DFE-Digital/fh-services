using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Models;

public class LanguageDto  : BaseHsdsDto
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("code")]
    public string? Code { get; init; }

    [JsonPropertyName("note")]
    public string? Note { get; init; }
}