using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Organization
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

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
    [NotMapped]
    public List<Funding> Funding { get; init; } = new();

    [JsonPropertyName("contacts")]
    [NotMapped]
    public List<Contact> Contacts { get; init; } = new();

    [JsonPropertyName("phones")]
    [NotMapped]
    public List<Phone> Phones { get; init; } = new();

    [JsonPropertyName("locations")]
    [NotMapped]
    public List<Location> Locations { get; init; } = new();

    [JsonPropertyName("programs")]
    [NotMapped]
    public List<Program> Programs { get; init; } = new();

    [JsonPropertyName("organization_identifiers")]
    [NotMapped]
    public List<OrganizationIdentifier> OrganizationIdentifiers { get; init; } = new();

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; init; } = new();
}