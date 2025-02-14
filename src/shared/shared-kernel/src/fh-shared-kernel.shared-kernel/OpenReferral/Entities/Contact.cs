using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Contact : BaseHsdsEntity
{
    public Guid? OrganizationId { get; init; }
    public Guid? ServiceId { get; init; }
    public Guid? ServiceAtLocationId { get; init; }
    public Guid? LocationId { get; init; }
    public string? Name { get; init; }
    public string? Title { get; init; }
    public string? Department { get; init; }
    public string? Email { get; init; }
    public List<Phone> Phones { get; init; } = new();
    public List<Attribute> Attributes { get; init; } = new();
    public List<Metadata> Metadata { get; init; } = new();
}