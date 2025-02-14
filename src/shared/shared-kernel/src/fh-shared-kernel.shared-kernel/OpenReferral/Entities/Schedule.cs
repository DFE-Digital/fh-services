using System.Text.Json.Serialization;
using FamilyHubs.SharedKernel.OpenReferral.Converters;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Schedule : BaseHsdsEntity
{
    public Guid? ServiceId { get; init; }
    public Guid? LocationId { get; init; }
    public Guid? ServiceAtLocationId { get; init; }
    public DateTime? ValidFrom { get; init; }
    public DateTime? ValidTo { get; init; }
    public DateTime? DtStart { get; init; }
    public byte? Timezone { get; init; }
    public DateTime? Until { get; init; }
    public short? Count { get; init; }
    public string? Wkst { get; init; }
    public string? Freq { get; init; }
    public short? Interval { get; init; }
    public string? ByDay { get; init; }
    public string? ByWeekNo { get; init; }
    public string? ByMonthDay { get; init; }
    public string? ByYearDay { get; init; }
    public string? Description { get; init; }
    public TimeSpan? OpensAt { get; init; } // TODO: // TODO: .NET 8 supports native TimeOnly, so convert.
    public TimeSpan? ClosesAt { get; init; } // TODO: .NET 8 supports native TimeOnly, so convert.
    public string? ScheduleLink { get; init; }
    public string? AttendingType { get; init; }
    public string? Notes { get; init; }
    public List<Attribute> Attributes { get; init; } = new();
    public List<Metadata> Metadata { get; init; } = new();
}