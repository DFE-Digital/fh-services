using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class Program: BaseHsdsEntity
{

    [JsonPropertyName("organization_id")]
    [JsonIgnore]
    public Guid? OrganizationId { get; init; }
    [JsonIgnore]
    public virtual Organization? Organization { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("alternate_name")]
    public string? AlternateName { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonIgnore]
    public virtual List<Service> Services { get; init; } = new();

    [JsonPropertyName("attributes")]
    public virtual List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public virtual List<Metadata> Metadata { get; init; } = new();
}