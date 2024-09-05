using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Contact
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("organization_id")]
    public Guid? OrganizationId { get; init; }

    [JsonPropertyName("service_id")]
    public Guid? ServiceId { get; init; }

    [JsonPropertyName("service_at_location_id")]
    public Guid? ServiceAtLocationId { get; init; }

    [JsonPropertyName("location_id")]
    public Guid? LocationId { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("department")]
    public string? Department { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("phones")]
    [NotMapped]
    public List<Phone> Phones { get; init; } = new();

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; init; } = new();
}