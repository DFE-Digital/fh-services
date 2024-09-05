using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Metadata
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("resource_id")]
    public required Guid ResourceId { get; set; }

    [JsonPropertyName("resource_type")]
    public required string ResourceType { get; set; }

    [JsonPropertyName("last_action_date")]
    public required DateTime LastActionDate { get; set; }

    [JsonPropertyName("last_action_type")]
    public required string LastActionType { get; set; }

    [JsonPropertyName("field_name")]
    public required string FieldName { get; set; }

    [JsonPropertyName("previous_value")]
    public required string PreviousValue { get; set; }

    [JsonPropertyName("replacement_value")]
    public required string ReplacementValue { get; set; }

    [JsonPropertyName("updated_by")]
    public required string UpdatedBy { get; set; }
}