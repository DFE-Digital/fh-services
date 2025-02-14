using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class OrganizationIdentifier : BaseHsdsEntity
{
    public Guid? OrganizationId { get; init; }
    public string? IdentifierScheme { get; init; }
    public string? IdentifierType { get; init; }
    public string? Identifier { get; init; }
    public List<Attribute> Attributes { get; init; } = new();
    public List<Metadata> Metadata { get; init; } = new();
}