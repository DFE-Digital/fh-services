using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class Address
{
    [JsonIgnore]
    public Guid Id { get; init; }

    [JsonPropertyName("id")]
    public required Guid OrId { get; init; }

    [JsonPropertyName("location_id")]
    [JsonIgnore]
    public Guid? LocationOrId { get; init; }
    [JsonIgnore]
    public virtual Location? Location { get; init; }

    [JsonPropertyName("attention")]
    public string? Attention { get; init; }

    [JsonPropertyName("address_1")]
    public required string Address1 { get; init; }

    [JsonPropertyName("address_2")]
    public string? Address2 { get; init; }

    [JsonPropertyName("city")]
    public required string City { get; init; }

    [JsonPropertyName("region")]
    public string? Region { get; init; }

    [JsonPropertyName("state_province")]
    public required string StateProvince { get; init; }

    [JsonPropertyName("postal_code")]
    public required string PostalCode { get; init; }

    [JsonPropertyName("country")]
    public required string Country { get; init; }

    [JsonPropertyName("address_type")]
    public required string AddressType { get; init; }

    [JsonPropertyName("attributes")]
    public virtual List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public virtual List<Metadata> Metadata { get; init; } = new();
}