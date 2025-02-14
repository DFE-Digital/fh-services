using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Program : BaseHsdsEntity
{
    public Guid? OrganizationId { get; init; }
    public string? Name { get; init; }
    public string? AlternateName { get; init; }
    public string? Description { get; init; }
    public List<Attribute> Attributes { get; init; } = new();
    public List<Metadata> Metadata { get; init; } = new();
}