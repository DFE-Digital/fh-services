using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Models;

public class FundingDto : BaseHsdsDto
{
    [JsonPropertyName("source")]
    public string? Source { get; init; }
}