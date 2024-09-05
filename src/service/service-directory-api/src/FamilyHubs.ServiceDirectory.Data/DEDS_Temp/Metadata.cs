using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Metadata
{
    [Required]
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [Required]
    [JsonPropertyName("resource_id")]
    public required Guid ResourceId { get; set; }

    [Required]
    [JsonPropertyName("resource_type")]
    public required string ResourceType { get; set; }

    [Required]
    [JsonPropertyName("last_action_date")]
    public required DateTime LastActionDate { get; set; }

    [Required]
    [JsonPropertyName("last_action_type")]
    public required string LastActionType { get; set; }

    [Required]
    [JsonPropertyName("field_name")]
    public required string FieldName { get; set; }

    [Required]
    [JsonPropertyName("previous_value")]
    public required string PreviousValue { get; set; }

    [Required]
    [JsonPropertyName("replacement_value")]
    public required string ReplacementValue { get; set; }

    [Required]
    [JsonPropertyName("updated_by")]
    public required string UpdatedBy { get; set; }
}