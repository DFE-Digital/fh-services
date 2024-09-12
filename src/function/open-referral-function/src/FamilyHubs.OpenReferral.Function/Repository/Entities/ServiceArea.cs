using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class ServiceArea: BaseHsdsEntity
{

    [JsonPropertyName("service_id")]
    [JsonIgnore]
    public Guid? ServiceId { get; init; }
    [JsonIgnore]
    public virtual Service? Service { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("extent")]
    public string? Extent { get; init; }

    [JsonPropertyName("extent_type")]
    public string? ExtentType { get; init; }

    [JsonPropertyName("uri")]
    public string? Uri { get; init; }

    [JsonPropertyName("attributes")]
    public virtual List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public virtual List<Metadata> Metadata { get; init; } = new();
}