namespace FamilyHubs.ServiceDirectory.Web.Models;

public class Location
{
    public string IsFamilyHub { get; init; } = null!; // "Yes" or "No"
    public string? Details { get; init; }
    public string DaysAvailable { get; init; } = null!;
    public string? ExtraAvailabilityDetails { get; init; }

    public IEnumerable<string> Address { get; init; } = [];
    public IEnumerable<string> Accessibilities { get; init; } = [];
}