namespace FamilyHubs.ServiceDirectory.Web.Models.ServiceDetail;

public class Location
{
    public string IsFamilyHub { get; init; } = null!; // "Yes" or "No"
    public string? Details { get; init; }

    public Schedule Schedule { get; init; } = null!;

    public IEnumerable<string> Address { get; init; } = [];
    public IEnumerable<string> Accessibilities { get; init; } = [];

    private bool Equals(Location other)
    {
        return IsFamilyHub == other.IsFamilyHub &&
               Details == other.Details &&
               Schedule.Equals(other.Schedule) &&
               Address.SequenceEqual(other.Address) &&
               Accessibilities.SequenceEqual(other.Accessibilities);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Location)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(IsFamilyHub, Details, Schedule, Address, Accessibilities);
    }
}