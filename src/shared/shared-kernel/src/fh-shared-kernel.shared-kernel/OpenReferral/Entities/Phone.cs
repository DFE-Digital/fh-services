using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Phone : BaseHsdsEntity
{
    [JsonIgnore]
    public Guid? LocationId { get; init; }

    [JsonIgnore]
    public Guid? ServiceId { get; init; }

    [JsonIgnore]
    public Guid? OrganizationId { get; init; }

    [JsonIgnore]
    public Guid? ContactId { get; init; }

    [JsonIgnore]
    public Guid? ServiceAtLocationId { get; init; }

    [JsonPropertyName("number")]
    public required string Number { get; init; }

    [JsonPropertyName("extension")]
    public short? Extension { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("languages")]
    public List<Language> Languages { get; init; } = new();

    [JsonPropertyName("attributes")]
    public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public List<Metadata> Metadata { get; init; } = new();
}