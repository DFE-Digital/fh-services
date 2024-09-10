using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class Location
{
    [JsonIgnore]
    public Guid Id { get; init; }

    [JsonPropertyName("id")]
    public required Guid OrId { get; init; }

    [JsonPropertyName("location_type")]
    public required string LocationType { get; init; }

    [JsonPropertyName("url")]
    public string? Url { get; init; }

    [JsonPropertyName("organization_id")]
    [JsonIgnore]
    public Guid? OrganizationOrId { get; init; }
    [JsonIgnore]
    public virtual Organization? Organization { get; init; }

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
    public virtual List<Language> Languages { get; init; } = new();

    [JsonPropertyName("addresses")]
    public virtual List<Address> Addresses { get; init; } = new();

    [JsonPropertyName("contacts")]
    public virtual List<Contact> Contacts { get; init; } = new();

    [JsonPropertyName("accessibility")]
    public virtual List<Accessibility> Accessibilities { get; init; } = new();

    [JsonPropertyName("phones")]
    public virtual List<Phone> Phones { get; init; } = new();

    [JsonPropertyName("schedules")]
    public virtual List<Schedule> Schedules { get; init; } = new();

    [JsonIgnore]
    public virtual List<ServiceAtLocation> ServiceAtLocations { get; init; } = new();

    [JsonPropertyName("attributes")]
    public virtual List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public virtual List<Metadata> Metadata { get; init; } = new();
}