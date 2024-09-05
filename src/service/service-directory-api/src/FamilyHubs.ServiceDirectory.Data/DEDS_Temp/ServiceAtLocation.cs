using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class ServiceAtLocation
{
    [Required]
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("service_id")]
    public Guid? ServiceId { get; set; }

    [JsonPropertyName("location_id")]
    public Guid? LocationId { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("contacts")]
    [NotMapped]
    public List<Contact> Contacts { get; set; } = new();

    [JsonPropertyName("phones")]
    [NotMapped]
    public List<Phone> Phones { get; set; } = new();

    [JsonPropertyName("schedules")]
    [NotMapped]
    public List<Schedule> Schedules { get; set; } = new();

    [JsonPropertyName("location")]
    [NotMapped]
    public Location? Location { get; set; }

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; set; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; set; } = new();
}