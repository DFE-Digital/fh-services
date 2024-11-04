
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
        string? AgeRange = null);
}
