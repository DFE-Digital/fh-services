using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Models;

public class OrganizationIdentifierDto : BaseHsdsDto
{
    [JsonPropertyName("identifier_scheme")]
    public string? IdentifierScheme { get; init; }

    [JsonPropertyName("identifier_type")]
    public string? IdentifierType { get; init; }

    [JsonPropertyName("identifier")]
    public string? Identifier { get; init; }
}