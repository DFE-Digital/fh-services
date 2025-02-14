using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Language : BaseHsdsEntity
{
    public Guid? ServiceId { get; init; }
    public Guid? LocationId { get; init; }
    public Guid? PhoneId { get; init; }
    public string? Name { get; init; }
    public string? Code { get; init; }
    public string? Note { get; init; }
    public List<Attribute> Attributes { get; init; } = new();
    public List<Metadata> Metadata { get; init; } = new();
}