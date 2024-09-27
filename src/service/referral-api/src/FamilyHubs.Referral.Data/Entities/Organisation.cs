namespace FamilyHubs.Referral.Data.Entities;

public class Organisation : EntityBase<long>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}
