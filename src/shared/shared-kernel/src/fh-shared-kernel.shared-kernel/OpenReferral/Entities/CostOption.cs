using System.Text.Json.Serialization;
using FamilyHubs.SharedKernel.OpenReferral.Converters;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class CostOption : BaseHsdsEntity
{
    public Guid ServiceId { get; init; }
    public DateTime? ValidFrom { get; init; } // TODO: .NET 8 supports native DateOnly, so convert.
    public DateTime? ValidTo { get; init; } // TODO: .NET 8 supports native DateOnly, so convert.
    public string? Option { get; init; }
    public string? Currency { get; init; }
    public decimal? Amount { get; init; }
    public string? AmountDescription { get; init; }
    public List<Attribute> Attributes { get; init; } = new();
    public List<Metadata> Metadata { get; init; } = new();
}