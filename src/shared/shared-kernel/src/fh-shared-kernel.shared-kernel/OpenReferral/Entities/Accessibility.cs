namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Accessibility : BaseHsdsEntity
{
    public Guid? LocationId { get; init; }
    public string? Description { get; init; }
    public string? Details { get; init; }
    public string? Url { get; init; }
    public List<Attribute> Attributes { get; init; } = new();
    public List<Metadata> Metadata { get; init; } = new();
}