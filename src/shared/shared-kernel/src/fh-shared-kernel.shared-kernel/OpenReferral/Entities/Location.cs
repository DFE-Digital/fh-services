using System.Text.Json.Serialization;
using FamilyHubs.SharedKernel.OpenReferral.Converters;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Location : BaseHsdsEntity
{
    public required string LocationType { get; init; }
    public string? Url { get; init; }
    public Guid? OrganizationId { get; init; }
    public string? Name { get; init; }
    public string? AlternateName { get; init; }
    public string? Description { get; init; }
    public string? Transportation { get; init; }
    public decimal? Latitude { get; init; }
    public decimal? Longitude { get; init; }
    public string? ExternalIdentifier { get; init; }
    public string? ExternalIdentifierType { get; init; }
    public List<Language> Languages { get; init; } = new();
    public List<Address> Addresses { get; init; } = new();
    public List<Contact> Contacts { get; init; } = new();
    public List<Accessibility> Accessibilities { get; init; } = new();
    public List<Phone> Phones { get; init; } = new();
    public List<Schedule> Schedules { get; init; } = new();
    public List<Attribute> Attributes { get; init; } = new();
    public List<Metadata> Metadata { get; init; } = new();
}