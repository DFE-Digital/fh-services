using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class CostOption: BaseHSDSEntity
{

    [JsonPropertyName("service_id")]
    [JsonIgnore]
    public Guid ServiceId { get; init; }
    [JsonIgnore]
    public virtual Service? Service { get; init; }

    [JsonPropertyName("valid_from")]
    public DateTime? ValidFrom { get; init; } // TODO: .NET 8 supports native DateOnly, so convert.

    [JsonPropertyName("valid_to")]
    public DateTime? ValidTo { get; init; } // TODO: .NET 8 supports native DateOnly, so convert.

    [JsonPropertyName("option")]
    public string? Option { get; init; }

    [JsonPropertyName("currency")]
    public string? Currency { get; init; }

    [JsonPropertyName("amount")]
    public decimal? Amount { get; init; }

    [JsonPropertyName("amount_description")]
    public string? AmountDescription { get; init; }

    [JsonPropertyName("attributes")]
    public virtual List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public virtual List<Metadata> Metadata { get; init; } = new();
}