namespace FamilyHubs.ServiceDirectory.Web.Models.ServiceDetail;

public class ServiceDetailModel
{
    public string Name { get; init; } = null!;
    public string? Summary { get; init; }
    public string Eligibility { get; init; } = null!;
    public string Cost { get; init; } = null!;
    public string? MoreDetails { get; init; }
    public string AttendingTypes { get; init; } = null!;

    public string? OnlineTelephone { get; init; }
    public Schedule? Schedule { get; init; } = null!;

    public IEnumerable<string> Categories { get; init; } = [];
    public IEnumerable<string> Languages { get; init; } = [];

    public IList<Location> Locations { get; init; } = [];
    public Contact Contact { get; init; } = null!;

    private bool Equals(ServiceDetailModel other)
    {
        return Name == other.Name &&
               Summary == other.Summary &&
               Eligibility == other.Eligibility &&
               Cost == other.Cost &&
               MoreDetails == other.MoreDetails &&
               AttendingTypes == other.AttendingTypes &&
               OnlineTelephone == other.OnlineTelephone &&
               Equals(Schedule, other.Schedule) &&
               Categories.SequenceEqual(other.Categories) &&
               Languages.SequenceEqual(other.Languages) &&
               Locations.SequenceEqual(other.Locations) &&
               Contact.Equals(other.Contact);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ServiceDetailModel)obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(Name);
        hashCode.Add(Summary);
        hashCode.Add(Eligibility);
        hashCode.Add(Cost);
        hashCode.Add(MoreDetails);
        hashCode.Add(AttendingTypes);
        hashCode.Add(OnlineTelephone);
        hashCode.Add(Schedule);
        hashCode.Add(Categories);
        hashCode.Add(Languages);
        hashCode.Add(Locations);
        hashCode.Add(Contact);
        return hashCode.ToHashCode();
    }
}