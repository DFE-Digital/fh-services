using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Contact
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("organization_id")]
    public Guid? OrganizationId { get; set; }

    [JsonPropertyName("service_id")]
    public Guid? ServiceId { get; set; }

    [JsonPropertyName("service_at_location_id")]
    public Guid? ServiceAtLocationId { get; set; }

    [JsonPropertyName("location_id")]
    public Guid? LocationId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("department")]
    public string? Department { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("phones")]
    [NotMapped]
    public List<Phone> Phones { get; set; } = new();

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; set; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; set; } = new();
}