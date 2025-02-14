using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Funding : BaseHsdsEntity
{
    public Guid? OrganizationId { get; init; }
    public Guid? ServiceId { get; init; }
    public string? Source { get; init; }
    public List<Attribute> Attributes { get; init; } = new();
    public List<Metadata> Metadata { get; init; } = new();
}