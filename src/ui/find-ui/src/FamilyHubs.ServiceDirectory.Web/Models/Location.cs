namespace FamilyHubs.ServiceDirectory.Web.Models;

public record Location(string? Name, IEnumerable<string?> Address, string? MoreDetails, double? Distance)
    : ServiceDetail(Distance, ServiceDetailType.Location);