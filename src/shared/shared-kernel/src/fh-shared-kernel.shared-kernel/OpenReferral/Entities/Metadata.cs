using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Metadata : BaseHsdsEntity
{
    [JsonPropertyName("resource_id")]
    public Guid? ResourceId { get; init; }

    [JsonPropertyName("resource_type")]
    public required string ResourceType { get; init; }

    [JsonPropertyName("last_action_date")]
    public required DateTime LastActionDate { get; init; }

    [JsonPropertyName("last_action_type")]
    public required string LastActionType { get; init; }

    [JsonPropertyName("field_name")]
    public required string FieldName { get; init; }

    [JsonPropertyName("previous_value")]
    public required string PreviousValue { get; init; }

    [JsonPropertyName("replacement_value")]
    public required string ReplacementValue { get; init; }

    [JsonPropertyName("updated_by")]
    public required string UpdatedBy { get; init; }
}