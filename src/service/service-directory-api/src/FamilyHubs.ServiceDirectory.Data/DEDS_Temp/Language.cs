using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Language
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("service_id")]
    public Guid? ServiceId { get; init; }

    [JsonPropertyName("location_id")]
    public Guid? LocationId { get; init; }

    [JsonPropertyName("phone_id")]
    public Guid? PhoneId { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("code")]
    public string? Code { get; init; }

    [JsonPropertyName("note")]
    public string? Note { get; init; }

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; init; } = new();
}