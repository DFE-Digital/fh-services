using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class RequiredDocument : BaseHsdsEntity
{
    public Guid? ServiceId { get; init; }
    public string? Document { get; init; }
    public string? Uri { get; init; }
    public List<Attribute> Attributes { get; init; } = new();
    public List<Metadata> Metadata { get; init; } = new();
}