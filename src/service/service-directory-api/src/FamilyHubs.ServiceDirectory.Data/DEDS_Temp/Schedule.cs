using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Schedule
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("service_id")]
    [JsonIgnore]
    public Guid? ServiceId { get; init; }

    [JsonPropertyName("location_id")]
    [JsonIgnore]
    public Guid? LocationId { get; init; }

    [JsonPropertyName("service_at_location_id")]
    [JsonIgnore]
    public Guid? ServiceAtLocationId { get; init; }

    [JsonPropertyName("valid_from")]
    public DateTime? ValidFrom { get; init; }

    [JsonPropertyName("valid_to")]
    public DateTime? ValidTo { get; init; }

    [JsonPropertyName("dtstart")]
    public DateTime? Dtstart { get; init; }

    [JsonPropertyName("timezone")]
    public byte? Timezone { get; init; }

    [JsonPropertyName("until")]
    public DateTime? Until { get; init; }

    [JsonPropertyName("count")]
    public short? Count { get; init; }

    [JsonPropertyName("wkst")]
    public string? Wkst { get; init; }

    [JsonPropertyName("freq")]
    public string? Freq { get; init; }

    [JsonPropertyName("interval")]
    public short? Interval { get; init; }

    [JsonPropertyName("byday")]
    public string? Byday { get; init; }

    [JsonPropertyName("byweekno")]
    public string? Byweekno { get; init; }

    [JsonPropertyName("bymonthday")]
    public string? Bymonthday { get; init; }

    [JsonPropertyName("byyearday")]
    public string? Byyearday { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("opens_at")]
    public TimeOnly? OpensAt { get; init; }

    [JsonPropertyName("closes_at")]
    public TimeOnly? ClosesAt { get; init; }

    [JsonPropertyName("schedule_link")]
    public string? ScheduleLink { get; init; }

    [JsonPropertyName("attending_type")]
    public string? AttendingType { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; init; } = new();
}