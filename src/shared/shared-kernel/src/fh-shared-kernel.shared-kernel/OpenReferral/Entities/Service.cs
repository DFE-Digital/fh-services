using System.Text.Json.Serialization;
using FamilyHubs.SharedKernel.OpenReferral.Converters;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Service : BaseHsdsEntity
{
    public Guid? OrganizationId { get; init; }
    public Organization? Organization { get; init; }
    public Guid? ProgramId { get; init; }
    public Program? Program { get; init; }
    public required string Name { get; init; }
    public string? AlternateName { get; init; }
    public string? Description { get; init; }
    public string? Url { get; init; }
    public string? Email { get; init; }
    public required string Status { get; init; }
    public string? InterpretationServices { get; init; }
    public string? ApplicationProcess { get; init; }
    public string? FeesDescription { get; init; }
    public string? Accreditations { get; init; }
    public string? EligibilityDescription { get; init; }
    public byte? MinimumAge { get; init; }
    public byte? MaximumAge { get; init; }
    public DateTime? AssuredDate { get; init; }
    public string? AssurerEmail { get; init; }
    public string? Alert { get; init; }
    public DateTime? LastModified { get; init; }
    public List<Phone> Phones { get; init; } = new();
    public List<Schedule> Schedules { get; init; } = new();
    public List<ServiceArea> ServiceAreas { get; init; } = new();
    public List<ServiceAtLocation> ServiceAtLocations { get; init; } = new();
    public List<Language> Languages { get; init; } = new();
    public List<Funding> Funding { get; init; } = new();
    public List<CostOption> CostOptions { get; init; } = new();
    public List<RequiredDocument> RequiredDocuments { get; init; } = new();
    public List<Contact> Contacts { get; init; } = new();
    public List<Attribute> Attributes { get; init; } = new();
    public List<Metadata> Metadata { get; init; } = new();
    public long Checksum { get; set; }
}