using System.Text.Json.Serialization;
using FamilyHubs.SharedKernel.OpenReferral.Entities;

namespace FamilyHubs.OpenReferral.Function.Models;

public class AccessibilityDto : BaseHsdsDto
{
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("details")]
    public string? Details { get; init; }

    [JsonPropertyName("url")]
    public string? Url { get; init; }
    
}