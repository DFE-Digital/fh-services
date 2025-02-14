using System.Text.Json.Serialization;
using FamilyHubs.SharedKernel.OpenReferral.Converters;

namespace FamilyHubs.OpenReferral.Function.Models;

public class CostOptionDto : BaseHsdsDto
{
    [JsonPropertyName("valid_from")]
    [JsonConverter(typeof(StringToNullableTypeConverter))]
    public DateTime? ValidFrom { get; init; } // TODO: .NET 8 supports native DateOnly, so convert.

    [JsonPropertyName("valid_to")]
    [JsonConverter(typeof(StringToNullableTypeConverter))]
    public DateTime? ValidTo { get; init; } // TODO: .NET 8 supports native DateOnly, so convert.

    [JsonPropertyName("option")]
    public string? Option { get; init; }

    [JsonPropertyName("currency")]
    public string? Currency { get; init; }

    [JsonPropertyName("amount")]
    [JsonConverter(typeof(StringToNullableTypeConverter))]
    public decimal? Amount { get; init; }

    [JsonPropertyName("amount_description")]
    public string? AmountDescription { get; init; }
}