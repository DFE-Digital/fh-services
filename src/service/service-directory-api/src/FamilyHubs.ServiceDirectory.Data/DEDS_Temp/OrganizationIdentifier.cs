using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class OrganizationIdentifier
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("organization_id")]
    public Guid? OrganizationId { get; set; }

    [JsonPropertyName("identifier_scheme")]
    public string? IdentifierScheme { get; set; }

    [JsonPropertyName("identifier_type")]
    public string? IdentifierType { get; set; }

    [JsonPropertyName("identifier")]
    public string? Identifier { get; set; }

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; set; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; set; } = new();
}