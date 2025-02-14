using System.Text.Json.Serialization;
using FamilyHubs.SharedKernel.OpenReferral.Converters;

namespace FamilyHubs.OpenReferral.Function.Models;

public class PhoneDto  : BaseHsdsDto
{
    [JsonPropertyName("number")]
    public required string Number { get; init; }

    [JsonPropertyName("extension")]
    [JsonConverter(typeof(StringToNullableTypeConverter))]
    public short? Extension { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("languages")]
    public List<LanguageDto> Languages { get; init; } = new();
}