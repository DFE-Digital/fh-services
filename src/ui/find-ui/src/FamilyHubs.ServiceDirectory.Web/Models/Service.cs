namespace FamilyHubs.ServiceDirectory.Web.Models;

public record Service(
    string Name,
    double? Distance,
    IEnumerable<string> Cost,
    IEnumerable<string> Where,
    IEnumerable<string> When,
    IEnumerable<string> Categories,
    string? AgeRange = null,
    string? Phone = null,
    string? Email = null,
    string? WebsiteName = null,
    string? WebsiteUrl = null) : ServiceDetail(Distance, ServiceDetailType.Service);