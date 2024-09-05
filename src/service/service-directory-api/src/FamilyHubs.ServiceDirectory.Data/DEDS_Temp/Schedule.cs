using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Schedule
{
    [Required]
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("service_id")]
    public Guid? ServiceId { get; set; }

    [JsonPropertyName("location_id")]
    public Guid? LocationId { get; set; }

    [JsonPropertyName("service_at_location_id")]
    public Guid? ServiceAtLocationId { get; set; }

    [JsonPropertyName("valid_from")]
    public DateTime? ValidFrom { get; set; }

    [JsonPropertyName("valid_to")]
    public DateTime? ValidTo { get; set; }

    [JsonPropertyName("dtstart")]
    public DateTime? Dtstart { get; set; }

    [JsonPropertyName("timezone")]
    public byte? Timezone { get; set; }

    [JsonPropertyName("until")]
    public DateTime? Until { get; set; }

    [JsonPropertyName("count")]
    public short? Count { get; set; }

    [JsonPropertyName("wkst")]
    public string? Wkst { get; set; }

    [JsonPropertyName("freq")]
    public string? Freq { get; set; }

    [JsonPropertyName("interval")]
    public short? Interval { get; set; }

    [JsonPropertyName("byday")]
    public string? Byday { get; set; }

    [JsonPropertyName("byweekno")]
    public string? Byweekno { get; set; }

    [JsonPropertyName("bymonthday")]
    public string? Bymonthday { get; set; }

    [JsonPropertyName("byyearday")]
    public string? Byyearday { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("opens_at")]
    public DateTime? OpensAt { get; set; }

    [JsonPropertyName("closes_at")]
    public DateTime? ClosesAt { get; set; }

    [JsonPropertyName("schedule_link")]
    public string? ScheduleLink { get; set; }

    [JsonPropertyName("attending_type")]
    public string? AttendingType { get; set; }

    [JsonPropertyName("notes")]
    public string? Notes { get; set; }

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; set; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; set; } = new();
}