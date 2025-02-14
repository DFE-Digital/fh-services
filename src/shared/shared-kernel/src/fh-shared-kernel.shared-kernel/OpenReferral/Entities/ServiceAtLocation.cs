namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class ServiceAtLocation : BaseHsdsEntity
{
    public Guid? ServiceId { get; init; }
    public Guid? LocationId { get; init; }
    public Location? Location { get; init; }
    public string? Description { get; init; }
    public List<Contact> Contacts { get; init; } = new();
    public List<Phone> Phones { get; init; } = new();
    public List<Schedule> Schedules { get; init; } = new();
    public List<Attribute> Attributes { get; init; } = new();
    public List<Metadata> Metadata { get; init; } = new();
}