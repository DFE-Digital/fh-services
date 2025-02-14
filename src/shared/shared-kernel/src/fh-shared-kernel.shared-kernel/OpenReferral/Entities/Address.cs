using System.Text.Json.Serialization;

namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class Address : BaseHsdsEntity
{
    public Guid? LocationId { get; init; }
    public string? Attention { get; init; }
    public required string Address1 { get; init; }
    public string? Address2 { get; init; }
    public required string City { get; init; }
    public string? Region { get; init; }
    public required string StateProvince { get; init; }
    public required string PostalCode { get; init; }
    public required string Country { get; init; }
    public required string AddressType { get; init; }
    public List<Attribute> Attributes { get; init; } = new();
    public List<Metadata> Metadata { get; init; } = new();
}