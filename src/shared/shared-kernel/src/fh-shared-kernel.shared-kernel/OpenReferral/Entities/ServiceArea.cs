using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class ServiceArea : BaseHsdsEntity
{
    public Guid? ServiceId { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? Extent { get; init; }
    public string? ExtentType { get; init; }
    public string? Uri { get; init; }
    public List<Attribute> Attributes { get; init; } = new();
    public List<Metadata> Metadata { get; init; } = new();
}