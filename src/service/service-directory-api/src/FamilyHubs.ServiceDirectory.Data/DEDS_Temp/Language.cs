using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Language
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("service_id")]
    public Guid? ServiceId { get; set; }

    [JsonPropertyName("location_id")]
    public Guid? LocationId { get; set; }

    [JsonPropertyName("phone_id")]
    public Guid? PhoneId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("note")]
    public string? Note { get; set; }

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; set; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; set; } = new();
}