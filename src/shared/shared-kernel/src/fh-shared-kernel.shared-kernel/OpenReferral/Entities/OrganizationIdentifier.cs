using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class OrganizationIdentifier : BaseHsdsEntity
{
    [JsonIgnore]
    public Guid? OrganizationId { get; init; }

    [JsonPropertyName("identifier_scheme")]
    public string? IdentifierScheme { get; init; }

    [JsonPropertyName("identifier_type")]
    public string? IdentifierType { get; init; }

    [JsonPropertyName("identifier")]
    public string? Identifier { get; init; }

    [JsonPropertyName("attributes")]
    public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public List<Metadata> Metadata { get; init; } = new();
}