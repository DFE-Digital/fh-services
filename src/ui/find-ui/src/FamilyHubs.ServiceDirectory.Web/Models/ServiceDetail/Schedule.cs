namespace FamilyHubs.ServiceDirectory.Web.Models.ServiceDetail;

public class Schedule
{
    public string? DaysAvailable { get; init; }
    public string? ExtraAvailabilityDetails { get; init; }

    private bool Equals(Schedule other)
    {
        return DaysAvailable == other.DaysAvailable &&
               ExtraAvailabilityDetails == other.ExtraAvailabilityDetails;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Schedule)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(DaysAvailable, ExtraAvailabilityDetails);
    }
}