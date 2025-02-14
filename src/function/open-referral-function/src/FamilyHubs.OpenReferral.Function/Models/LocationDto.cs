using System.Text.Json.Serialization;
using FamilyHubs.SharedKernel.OpenReferral.Converters;

namespace FamilyHubs.OpenReferral.Function.Models;

public class LocationDto : BaseHsdsDto
{
    [JsonPropertyName("location_type")]
    public required string LocationType { get; init; }

    [JsonPropertyName("url")]
    public string? Url { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("alternate_name")]
    public string? AlternateName { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("transportation")]
    public string? Transportation { get; init; }

    [JsonPropertyName("latitude")]
    [JsonConverter(typeof(StringToNullableTypeConverter))]
    public decimal? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    [JsonConverter(typeof(StringToNullableTypeConverter))]
    public decimal? Longitude { get; init; }

    [JsonPropertyName("external_identifier")]
    public string? ExternalIdentifier { get; init; }

    [JsonPropertyName("external_identifier_type")]
    public string? ExternalIdentifierType { get; init; }

    [JsonPropertyName("languages")]
    public List<LanguageDto> Languages { get; init; } = new();

    [JsonPropertyName("addresses")]
    public List<AddressDto> Addresses { get; init; } = new();

    [JsonPropertyName("contacts")]
    public List<ContactDto> Contacts { get; init; } = new();

    [JsonPropertyName("accessibility")]
    public List<AccessibilityDto> Accessibilities { get; init; } = new();

    [JsonPropertyName("phones")]
    public List<PhoneDto> Phones { get; init; } = new();

    [JsonPropertyName("schedules")]
    public List<ScheduleDto> Schedules { get; init; } = new();
}