using FamilyHubs.ServiceDirectory.Web.Models;
using System.Diagnostics;
using FamilyHubs.ServiceDirectory.Core.Distance;
using FamilyHubs.ServiceDirectory.Shared.Display;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.SharedKernel.Enums;
using ServiceType = FamilyHubs.ServiceDirectory.Web.Models.ServiceType;

namespace FamilyHubs.ServiceDirectory.Web.Mappers;

//todo: use extension methods?
public static class ServiceMapper
{
    public static IEnumerable<Service> ToViewModel(IEnumerable<ServiceDto> services)
    {
        return services.Select(ToViewModel);
    }

    private static Service ToViewModel(ServiceDto service)
    {
        Debug.Assert(service.ServiceType == Shared.Enums.ServiceType.FamilyExperience);

        var eligibility = service.Eligibilities.FirstOrDefault();

        var name = service.Name;

        return new Service(
            service.Id,
            IsFamilyHub(service.Locations) ? ServiceType.FamilyHub : ServiceType.Service,
            name,
            service.Distance != null ? DistanceConverter.MetersToMiles(service.Distance.Value) : null,
            GetCost(service),
            GetLocations(service.Locations),
            GetCategories(service),
            GetDeliveryMethods(service.ServiceDeliveries),
            GetAgeRange(eligibility));
    }

    private static IEnumerable<string> GetLocations(ICollection<LocationDto> locationDtoList)
        => locationDtoList.Count switch
        {
            0 => [],
            1 => locationDtoList.First().GetAddress(),
            _ => [$"Available at {locationDtoList.Count} locations"]
        };

    private static IEnumerable<string> GetDeliveryMethods(ICollection<ServiceDeliveryDto> serviceDeliveries)
        => serviceDeliveries.Select(x => x.Name.ToDescription()).Order();

    private static string? GetAgeRange(EligibilityDto? eligibility)
    {
        return eligibility == null ? null : $"{AgeDisplayExtensions.AgeToString(eligibility.MinimumAge)} to {AgeDisplayExtensions.AgeToString(eligibility.MaximumAge)}";
    }

    private static bool IsFamilyHub(IEnumerable<LocationDto> locations)
        => locations.Any(location => location.LocationTypeCategory == LocationTypeCategory.FamilyHub);

    private static IEnumerable<string> GetCategories(ServiceDto service)
    {
        return  service.Taxonomies.OrderBy(st => st.ParentId).ThenBy(st => st.Id).Select(st => st.Name);
    }

    private static IEnumerable<string> GetCost(ServiceDto service)
    {
        const string free = "Free";

        if (!service.CostOptions.Any())
        {
            return new[] { free };
        }
        return new[] { "Yes, it costs money to use. " + service.CostOptions.First().AmountDescription };
    }
}