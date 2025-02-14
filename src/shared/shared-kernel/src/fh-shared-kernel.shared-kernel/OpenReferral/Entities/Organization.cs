namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Organization : BaseHsdsEntity
{
    public required string Name { get; init; }
    public string? AlternateName { get; init; }
    public required string Description { get; init; }
    public string? Email { get; init; }
    public string? Website { get; init; }
    public short? YearIncorporated { get; init; }
    public string? LegalStatus { get; init; }
    public string? Logo { get; init; }
    public string? Uri { get; init; }
    public Guid? ParentOrganizationId { get; init; }
    public List<Funding> Funding { get; init; } = new();
    public List<Contact> Contacts { get; init; } = new();
    public List<Phone> Phones { get; init; } = new();
    public List<Location> Locations { get; init; } = new();
    public List<Program> Programs { get; init; } = new();
    public List<OrganizationIdentifier> OrganizationIdentifiers { get; set; } = new();
    public List<Attribute> Attributes { get; init; } = new();
    public List<Metadata> Metadata { get; init; } = new();
}