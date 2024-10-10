using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Address : BaseHsdsEntity
{
    [JsonIgnore]
    public Guid? LocationId { get; init; }

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
    public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public List<Metadata> Metadata { get; init; } = new();
}