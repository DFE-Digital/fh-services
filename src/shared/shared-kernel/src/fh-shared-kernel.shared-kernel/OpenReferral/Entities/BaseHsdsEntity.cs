using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public abstract class BaseHsdsEntity
{
    [JsonIgnore]
    public Guid Id { get; init; }

    [JsonPropertyName("id")]
    public required Guid OrId { get; init; }
}