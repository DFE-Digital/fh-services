namespace FamilyHubs.ServiceDirectory.Web.Models.ServiceDetail;

public class Location
{
    public string IsFamilyHub { get; init; } = null!; // "Yes" or "No"
    public string? Details { get; init; }

    public Schedule Schedule { get; init; } = null!;

    public IEnumerable<string> Address { get; init; } = [];
    public IEnumerable<string> Accessibilities { get; init; } = [];
}