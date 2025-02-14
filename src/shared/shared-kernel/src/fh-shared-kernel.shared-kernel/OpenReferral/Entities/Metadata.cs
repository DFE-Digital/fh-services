using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Metadata : BaseHsdsEntity
{
    public Guid? ResourceId { get; init; }
    public required string ResourceType { get; init; }
    public required DateTime LastActionDate { get; init; }
    public required string LastActionType { get; init; }
    public required string FieldName { get; init; }
    public required string PreviousValue { get; init; }
    public required string ReplacementValue { get; init; }
    public required string UpdatedBy { get; init; }
}