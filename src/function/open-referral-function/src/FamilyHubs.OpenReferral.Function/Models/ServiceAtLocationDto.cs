using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Models;

public class ServiceAtLocationDto  : BaseHsdsDto
{
    [JsonPropertyName("location")]
    public LocationDto? Location { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("contacts")]
    public List<ContactDto> Contacts { get; init; } = new();

    [JsonPropertyName("phones")]
    public List<PhoneDto> Phones { get; init; } = new();

    [JsonPropertyName("schedules")]
    public List<ScheduleDto> Schedules { get; init; } = new();
}