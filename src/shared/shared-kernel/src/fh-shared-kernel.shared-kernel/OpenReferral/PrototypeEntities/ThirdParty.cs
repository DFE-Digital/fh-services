namespace FamilyHubs.SharedKernel.OpenReferral.PrototypeEntities;

public class ThirdParty
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? OpenReferralUrl { get; set; } // OpenReferralUrl
}