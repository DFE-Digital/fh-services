using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Location
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("location_type")]
    public required string LocationType { get; init; }

    [JsonPropertyName("url")]
    public string? Url { get; init; }

    [JsonPropertyName("organization_id")]
    public Guid? OrganizationId { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("alternate_name")]
    public string? AlternateName { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("transportation")]
    public string? Transportation { get; init; }

    [JsonPropertyName("latitude")]
    public decimal? Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public decimal? Longitude { get; init; }

    [JsonPropertyName("external_identifier")]
    public string? ExternalIdentifier { get; init; }

    [JsonPropertyName("external_identifier_type")]
    public string? ExternalIdentifierType { get; init; }

    [JsonPropertyName("languages")]
    [NotMapped]
    public List<Language> Languages { get; init; } = new();

    [JsonPropertyName("addresses")]
    [NotMapped]
    public List<Address> Addresses { get; init; } = new();

    [JsonPropertyName("contacts")]
    [NotMapped]
    public List<Contact> Contacts { get; init; } = new();

    [JsonPropertyName("accessibility")]
    [NotMapped]
    public List<Accessibility> Accessibility { get; init; } = new();

    [JsonPropertyName("phones")]
    [NotMapped]
    public List<Phone> Phones { get; init; } = new();

    [JsonPropertyName("schedules")]
    [NotMapped]
    public List<Schedule> Schedules { get; init; } = new();

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; init; } = new();
}