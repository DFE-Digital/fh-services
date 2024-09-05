using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class RequiredDocument
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("service_id")]
    public Guid? ServiceId { get; set; }

    [JsonPropertyName("document")]
    public string? Document { get; set; }

    [JsonPropertyName("uri")]
    public string? Uri { get; set; }

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; set; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; set; } = new();
}