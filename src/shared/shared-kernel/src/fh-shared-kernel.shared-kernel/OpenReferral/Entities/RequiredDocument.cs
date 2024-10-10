using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class RequiredDocument : BaseHsdsEntity
{
    [JsonIgnore]
    public Guid? ServiceId { get; init; }

    [JsonPropertyName("document")]
    public string? Document { get; init; }

    [JsonPropertyName("uri")]
    public string? Uri { get; init; }

    [JsonPropertyName("attributes")]
    public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public List<Metadata> Metadata { get; init; } = new();
}