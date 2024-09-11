using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FamilyHubs.OpenReferral.Function.Repository.Entities;

public class Organization: BaseHSDSEntity
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
    //[JsonIgnore]
    //public virtual Organization? ParentOrganization { get; init; }
    //[JsonIgnore]
    //public virtual List<Organization> ChildOrganizations { get; init; } = new();

    [JsonIgnore]
    public virtual List<Service> Services { get; init; } = new();

    [JsonPropertyName("funding")]
    public virtual List<Funding> Funding { get; init; } = new();

    [JsonPropertyName("contacts")]
    public virtual List<Contact> Contacts { get; init; } = new();

    [JsonPropertyName("phones")]
    public virtual List<Phone> Phones { get; init; } = new();

    [JsonPropertyName("locations")]
    public virtual List<Location> Locations { get; init; } = new();

    [JsonPropertyName("programs")]
    public virtual List<Program> Programs { get; init; } = new();

    [JsonPropertyName("organization_identifiers")]
    public virtual List<OrganizationIdentifier> OrganizationIdentifiers { get; init; } = new();

    [JsonPropertyName("attributes")]
    public virtual List<Attribute> Attributes { get; init; } = new();

    [JsonPropertyName("metadata")]
    public virtual List<Metadata> Metadata { get; init; } = new();
}