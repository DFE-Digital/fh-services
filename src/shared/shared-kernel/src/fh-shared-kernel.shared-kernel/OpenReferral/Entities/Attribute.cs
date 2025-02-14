namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Attribute : BaseHsdsEntity
{
    public Guid? LinkId { get; init; }
    public Guid? TaxonomyTermId { get; init; }
    public TaxonomyTerm? TaxonomyTerm { get; init; }
    public string? LinkType { get; init; }
    public required string LinkEntity { get; init; }
    public string? Value { get; init; }
    public List<Metadata> Metadata { get; init; } = new();
}