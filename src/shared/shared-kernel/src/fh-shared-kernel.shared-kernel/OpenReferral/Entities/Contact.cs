using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Contact : BaseHsdsEntity
{
    [JsonIgnore]
    public Guid? OrganizationId { get; init; }

    [JsonIgnore]
    public Guid? ServiceId { get; init; }

    [JsonIgnore]
    public Guid? ServiceAtLocationId { get; init; }

    [JsonIgnore]
    public Guid? LocationId { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("department")]
    public string? Department { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("phones")]
    public List<Phone> Phones { get; init; } = new();

    [JsonPropertyName("attributes")]
    public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public List<Metadata> Metadata { get; init; } = new();
}