using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Models;


public abstract class BaseHsdsDto : IExternalService
{
    [JsonPropertyName("id")]
    public required Guid OrId { get; init; }
    
    [JsonPropertyName("attributes")]
    public List<AttributeDto> Attributes { get; init; } = [];

    [JsonPropertyName("metadata")]
    public List<MetadataDto> Metadata { get; init; } = [];

    [JsonIgnore]
    public Guid ThirdPartyIdentifier => OrId;
}