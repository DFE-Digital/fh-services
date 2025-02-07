using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Models;

public class AddressDto : BaseHsdsDto
{
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
}