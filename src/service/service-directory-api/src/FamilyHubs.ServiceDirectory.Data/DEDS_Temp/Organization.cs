using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.ServiceDirectory.Data.DEDS_Temp;

public class Organization
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("alternate_name")]
    public string? AlternateName { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("website")]
    public string? Website { get; set; }

    [JsonPropertyName("year_incorporated")]
    public required short YearIncorporated { get; set; }

    [JsonPropertyName("legal_status")]
    public required string LegalStatus { get; set; }

    [JsonPropertyName("logo")]
    public string? Logo { get; set; }

    [JsonPropertyName("uri")]
    public string? Uri { get; set; }

    [JsonPropertyName("parent_organization_id")]
    public Guid? ParentOrganizationId { get; set; }

    [JsonPropertyName("funding")]
    [NotMapped]
    public List<Funding> Funding { get; set; } = new();

    [JsonPropertyName("contacts")]
    [NotMapped]
    public List<Contact> Contacts { get; set; } = new();

    [JsonPropertyName("phones")]
    [NotMapped]
    public List<Phone> Phones { get; set; } = new();

    [JsonPropertyName("locations")]
    [NotMapped]
    public List<Location> Locations { get; set; } = new();

    [JsonPropertyName("programs")]
    [NotMapped]
    public List<Program> Programs { get; set; } = new();

    [JsonPropertyName("organization_identifiers")]
    [NotMapped]
    public List<OrganizationIdentifier> OrganizationIdentifiers { get; set; } = new();

    [JsonPropertyName("attributes")]
    [NotMapped]
    public List<Attribute> Attributes { get; set; } = new();

    [JsonPropertyName("metadata")]
    [NotMapped]
    public List<Metadata> Metadata { get; set; } = new();
}