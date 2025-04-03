
namespace FamilyHubs.ServiceDirectory.Web.Models
{
    //todo: type hierarchy, rather than type? or just null what we don't have?
    // when / opening hours will show regular schedule only. holiday schedule will be ignored for mvp (probably just show the description field)
    public sealed record Service(
        long ServiceId,
        string Name,
        //todo: what's actually mandatory?
        double? Distance,
        IEnumerable<string> Cost,
        IEnumerable<string> Where,
        IEnumerable<string> Categories,
        IEnumerable<string> DeliveryMethods,
        string? AgeRange = null
    )
    {
        public bool Equals(Service? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return ServiceId == other.ServiceId &&
                   Name == other.Name &&
                   Nullable.Equals(Distance, other.Distance) &&
                   Cost.SequenceEqual(other.Cost) &&
                   Where.SequenceEqual(other.Where) &&
                   Categories.SequenceEqual(other.Categories) &&
                   DeliveryMethods.SequenceEqual(other.DeliveryMethods) &&
                   AgeRange == other.AgeRange;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ServiceId, Name, Distance, Cost, Where, Categories, DeliveryMethods, AgeRange);
        }
    }
}
