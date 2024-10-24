using FamilyHubs.ServiceDirectory.Web.Models;
using System.Diagnostics;
using FamilyHubs.ServiceDirectory.Core.Distance;
using FamilyHubs.ServiceDirectory.Shared.Display;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.ServiceDirectory.Shared.Extensions;

namespace FamilyHubs.ServiceDirectory.Web.Mappers;

//todo: use extension methods?
public static class ServiceMapper
{
    public static IEnumerable<Service> ToModel(IEnumerable<ServiceDto> services)
    {
        return services.Select(ToModel);
    }

    private static Service ToModel(ServiceDto service)
    {
        Debug.Assert(service.ServiceType == ServiceType.FamilyExperience); // TODO: FHB-805 What is this doing here???

        var location = service.Locations.First();
        var eligibility = service.Eligibilities.FirstOrDefault();

        var name = service.Name;
        var contact = service.GetContact();

        return new Service(
            name,
            service.Distance != null ? DistanceConverter.MetersToMiles(service.Distance.Value) : null,
            GetCost(service),
            location.GetAddress(),
            service.GetServiceAvailability(),
            GetCategories(service),
            GetAgeRange(eligibility),
            contact?.Telephone,
            contact?.Email,
            name,
            GetWebsiteUrl(contact?.Url));
    }

    private static string? GetAgeRange(EligibilityDto? eligibility)
    {
        return eligibility == null ? null : $"{AgeDisplayExtensions.AgeToString(eligibility.MinimumAge)} to {AgeDisplayExtensions.AgeToString(eligibility.MaximumAge)}";
    }

    // private static bool IsFamilyHub(LocationDto location) // TODO : FHB-805 Will be useful later... .... ..
    // {
    //     return location.LocationTypeCategory == LocationTypeCategory.FamilyHub;
    // }

    private static string? GetWebsiteUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return default;

        if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            return url;

        // assume http! (UriBuilder interprets a single string as a host and insists on adding a '/' on the end, which doesn't work if the url contains query params)
        return $"http://{url}";
    }

    private static IEnumerable<string> GetCategories(ServiceDto service)
    {
        return  service.Taxonomies.OrderBy(st => st.ParentId).ThenBy(st => st.Id).Select(st => st.Name);
    }

    private static IEnumerable<string> GetCost(ServiceDto service)
    {
        const string free = "Free";

        if (service.CostOptions.Count == 0)
        {
            return [free];
        }
        return ["Yes, it costs money to use. " + service.CostOptions.First().AmountDescription];
    }
}