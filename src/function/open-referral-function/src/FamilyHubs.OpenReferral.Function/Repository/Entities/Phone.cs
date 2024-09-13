using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class Phone : BaseHsdsEntity
{
    [JsonPropertyName("location_id")]
    [JsonIgnore]
    public Guid? LocationId { get; init; }

    [JsonPropertyName("service_id")]
    [JsonIgnore]
    public Guid? ServiceId { get; init; }

    [JsonPropertyName("organization_id")]
    [JsonIgnore]
    public Guid? OrganizationId { get; init; }

    [JsonPropertyName("contact_id")]
    [JsonIgnore]
    public Guid? ContactId { get; init; }

    [JsonPropertyName("service_at_location_id")]
    [JsonIgnore]
    public Guid? ServiceAtLocationId { get; init; }

    [JsonPropertyName("number")] public required string Number { get; init; }

    [JsonPropertyName("extension")] public short? Extension { get; init; }

    [JsonPropertyName("type")] public string? Type { get; init; }

    [JsonPropertyName("description")] public string? Description { get; init; }

    [JsonPropertyName("languages")] public List<Language> Languages { get; init; } = new();

    [JsonPropertyName("attributes")] public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")] public List<Metadata> Metadata { get; init; } = new();
}