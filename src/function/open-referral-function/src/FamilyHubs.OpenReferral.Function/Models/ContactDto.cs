using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Models;

public class ContactDto : BaseHsdsDto
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("department")]
    public string? Department { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("phones")]
    public List<PhoneDto> Phones { get; init; } = new();
}