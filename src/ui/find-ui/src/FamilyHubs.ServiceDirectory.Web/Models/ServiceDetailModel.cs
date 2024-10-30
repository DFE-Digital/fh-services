namespace FamilyHubs.ServiceDirectory.Web.Models;

public class ServiceDetailModel
{
    public string Name { get; init; } = null!;
    public string? Summary { get; init; }
    public string Eligibility { get; init; } = null!;
    public string Cost { get; init; } = null!;
    public string? MoreDetails { get; init; }
    public string Deliveries { get; init; } = null!;

    public string? OnlineTelephone { get; init; }
    public string? DaysAvailable { get; init; }
    public string? ExtraAvailabilityDetails { get; init; }

    public IEnumerable<string> Categories { get; init; } = [];
    public IEnumerable<string> Languages { get; init; } = [];

    public IList<Location> Locations { get; init; } = [];
    public Contact Contact { get; init; } = null!;
}