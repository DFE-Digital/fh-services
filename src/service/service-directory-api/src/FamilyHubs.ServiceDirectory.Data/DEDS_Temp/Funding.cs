using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Funding
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("organization_id")]
    public Guid? OrganizationId { get; set; }

    [JsonPropertyName("service_id")]
    public Guid? ServiceId { get; set; }

    [JsonPropertyName("source")]
    public string? Source { get; set; }

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; set; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; set; } = new();
}