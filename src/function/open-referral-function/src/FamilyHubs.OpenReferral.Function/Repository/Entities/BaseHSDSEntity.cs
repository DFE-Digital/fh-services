using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public abstract class BaseHsdsEntity
{
    [JsonIgnore]
    public Guid Id { get; init; }

    [JsonPropertyName("id")]
    public required Guid OrId { get; init; }
}