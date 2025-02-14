namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Taxonomy : BaseHsdsEntity
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string? Uri { get; init; }
    public string? Version { get; init; }
    public List<Metadata> Metadata { get; init; } = new();
}