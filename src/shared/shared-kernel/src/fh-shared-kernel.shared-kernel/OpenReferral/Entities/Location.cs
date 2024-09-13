using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Location : BaseHsdsEntity
{
    [JsonPropertyName("location_type")]
    public required string LocationType { get; init; }

    [JsonPropertyName("url")]
    public string? Url { get; init; }

    [JsonIgnore]
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
    public List<Language> Languages { get; init; } = new();

    [JsonPropertyName("addresses")]
    public List<Address> Addresses { get; init; } = new();

    [JsonPropertyName("contacts")]
    public List<Contact> Contacts { get; init; } = new();

    [JsonPropertyName("accessibility")]
    public List<Accessibility> Accessibilities { get; init; } = new();

    [JsonPropertyName("phones")]
    public List<Phone> Phones { get; init; } = new();

    [JsonPropertyName("schedules")]
    public List<Schedule> Schedules { get; init; } = new();

    [JsonPropertyName("attributes")]
    public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public List<Metadata> Metadata { get; init; } = new();
}