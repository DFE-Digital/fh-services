using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class CostOption
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("service_id")]
    public required Guid ServiceId { get; init; }

    [JsonPropertyName("valid_from")]
    public DateTime? ValidFrom { get; init; }

    [JsonPropertyName("valid_to")]
    public DateTime? ValidTo { get; init; }

    [JsonPropertyName("option")]
    public string? Option { get; init; }

    [JsonPropertyName("currency")]
    public string? Currency { get; init; }

    [JsonPropertyName("amount")]
    public decimal? Amount { get; init; }

    [JsonPropertyName("amount_description")]
    public string? AmountDescription { get; init; }

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; init; } = new();
}