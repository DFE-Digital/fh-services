namespace FamilyHubs.SharedKernel.OpenReferral.Entities;

public class ThirdParty
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? OpenReferralUrl { get; set; } // OpenReferralUrl
}