using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Address
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("location_id")]
    public Guid? LocationId { get; set; }

    [JsonPropertyName("attention")]
    public string? Attention { get; set; }

    [JsonPropertyName("address_1")]
    public required string Address1 { get; set; }

    [JsonPropertyName("address_2")]
    public string? Address2 { get; set; }

    [JsonPropertyName("city")]
    public required string City { get; set; }

    [JsonPropertyName("region")]
    public string? Region { get; set; }

    [JsonPropertyName("state_province")]
    public required string StateProvince { get; set; }

    [JsonPropertyName("postal_code")]
    public required string PostalCode { get; set; }

    [JsonPropertyName("country")]
    public required string Country { get; set; }

    [JsonPropertyName("address_type")]
    public required string AddressType { get; set; }

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; set; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; set; } = new();
}