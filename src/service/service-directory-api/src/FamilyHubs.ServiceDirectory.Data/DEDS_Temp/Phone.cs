using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Phone
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("location_id")]
    public Guid? LocationId { get; init; }

    [JsonPropertyName("service_id")]
    public Guid? ServiceId { get; init; }

    [JsonPropertyName("organization_id")]
    public Guid? OrganizationId { get; init; }

    [JsonPropertyName("contact_id")]
    public Guid? ContactId { get; init; }

    [JsonPropertyName("service_at_location_id")]
    public Guid? ServiceAtLocationId { get; init; }

    [JsonPropertyName("number")]
    public required string Number { get; init; }

    [JsonPropertyName("extension")]
    public short? Extension { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("languages")]
    [NotMapped]
    public List<Language> Languages { get; init; } = new();

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; init; } = new();
}