using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class MetaTableDescription : BaseHsdsEntity
{
    public string? Name { get; init; }
    public string? Language { get; init; }
    public string? CharacterSet { get; init; }
    public List<Attribute> Attributes { get; init; } = new();
    public List<Metadata> Metadata { get; init; } = new();
}