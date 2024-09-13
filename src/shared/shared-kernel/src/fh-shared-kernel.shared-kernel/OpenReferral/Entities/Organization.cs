using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Organization : BaseHsdsEntity
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("alternate_name")]
    public string? AlternateName { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("website")]
    public string? Website { get; init; }

    [JsonPropertyName("year_incorporated")]
    public required short YearIncorporated { get; init; }

    [JsonPropertyName("legal_status")]
    public required string LegalStatus { get; init; }

    [JsonPropertyName("logo")]
    public string? Logo { get; init; }

    [JsonPropertyName("uri")]
    public string? Uri { get; init; }

    [JsonPropertyName("parent_organization_id")]
    public Guid? ParentOrganizationId { get; init; }

    [JsonPropertyName("funding")]
    public List<Funding> Funding { get; init; } = new();

    [JsonPropertyName("contacts")]
    public List<Contact> Contacts { get; init; } = new();

    [JsonPropertyName("phones")]
    public List<Phone> Phones { get; init; } = new();

    [JsonPropertyName("locations")]
    public List<Location> Locations { get; init; } = new();

    [JsonPropertyName("programs")]
    public List<Program> Programs { get; init; } = new();

    [JsonPropertyName("organization_identifiers")]
    public List<OrganizationIdentifier> OrganizationIdentifiers { get; init; } = new();

    [JsonPropertyName("attributes")]
    public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public List<Metadata> Metadata { get; init; } = new();
}