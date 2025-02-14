using System.Text.Json.Serialization;
using FamilyHubs.SharedKernel.OpenReferral.Converters;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Phone : BaseHsdsEntity
{
    public Guid? LocationId { get; init; }
    public Guid? ServiceId { get; init; }
    public Guid? OrganizationId { get; init; }
    public Guid? ContactId { get; init; }
    public Guid? ServiceAtLocationId { get; init; }
    public required string Number { get; init; }
    public short? Extension { get; init; }
    public string? Type { get; init; }
    public string? Description { get; init; }
    public List<Language> Languages { get; init; } = new();
    public List<Attribute> Attributes { get; init; } = new();
    public List<Metadata> Metadata { get; init; } = new();
}