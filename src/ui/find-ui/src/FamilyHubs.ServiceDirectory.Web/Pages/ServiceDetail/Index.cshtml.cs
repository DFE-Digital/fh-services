using FamilyHubs.ServiceDirectory.Core.ServiceDirectory.Interfaces;
using FamilyHubs.ServiceDirectory.Shared.Display;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.ServiceDirectory.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Location = FamilyHubs.ServiceDirectory.Web.Models.Location;

namespace FamilyHubs.ServiceDirectory.Web.Pages.ServiceDetail;

// TODO: FHB-712 - Most of this can be shared with Connect, so look to move it to a shared space when functionality done!

public class Index : PageModel
{
    private readonly IServiceDirectoryClient _serviceDirectoryClient;

    public ServiceDetailModel Service { get; set; } = null!;

    public Index(IServiceDirectoryClient serviceDirectoryClient)
    {
        _serviceDirectoryClient = serviceDirectoryClient;
    }

    private async Task<ServiceDto> GetService(long serviceId) =>
        await _serviceDirectoryClient.GetServiceById(serviceId);

    private static string GetEligibility(ICollection<EligibilityDto> serviceEligibilities)
        => serviceEligibilities.Count == 0
            ? "No"
            : $"Yes, {serviceEligibilities.First().MinimumAge} to {serviceEligibilities.First().MaximumAge} years old";

    private static string GetCostOption(ICollection<CostOptionDto> serviceCostOptions)
        => serviceCostOptions.Count == 0 ? "Free" : "Yes, it costs money to use.";

    private static string GetDeliveries(ICollection<ServiceDeliveryDto> serviceDeliveries)
        => string.Join(", ", serviceDeliveries.Select(sD => sD.Name).Order());

    private static string IsFamilyHub(LocationTypeCategory locationTypeCategory) =>
        locationTypeCategory == LocationTypeCategory.FamilyHub ? "Yes" : "No";

    private static ScheduleDto GetScheduleForLocation(long locationId,
        ICollection<ServiceAtLocationDto> serviceAtLocations)
        => serviceAtLocations.First(sAl => sAl.LocationId == locationId).Schedules.First(); // TODO: FHB-712 - This throws errors for quite a few things but doesn't in Connect

    private static IEnumerable<string> GetAccessibilities(
        ICollection<AccessibilityForDisabilitiesDto> serviceAccessibilityForDisabilities)
    {
        List<string> accessibilities = [];

        accessibilities
            .AddRange(serviceAccessibilityForDisabilities
                .Select(accessibilityForDisabilitiesDto => accessibilityForDisabilitiesDto.Accessibility)
                .OfType<string>());

        return accessibilities;
    }

    private static string GetDaysAvailable(ScheduleDto locationSchedule) =>
        (locationSchedule.ByDay?.Split(',')).GetDayNames();

    private static List<Location> GetLocations(ICollection<LocationDto> serviceLocations,
        ICollection<ServiceAtLocationDto> serviceAtLocations)
    {
        List<Location> locations = [];
        locations.AddRange(
            from serviceLocation in serviceLocations
            let locationSchedule = GetScheduleForLocation(serviceLocation.Id, serviceAtLocations)
            select new Location
            {
                IsFamilyHub = IsFamilyHub(serviceLocation.LocationTypeCategory),
                Details = serviceLocation.Description,
                DaysAvailable = GetDaysAvailable(locationSchedule),
                ExtraAvailabilityDetails = locationSchedule.Description,
                Address = serviceLocation.GetAddress(),
                Accessibilities = GetAccessibilities(serviceLocation.AccessibilityForDisabilities)
            });

        return locations;
    }

    private static ServiceDetailModel GetServiceDetailModel(ServiceDto serviceDto)
    {
        ServiceDetailModel serviceDetailModel = new()
        {
            Name = serviceDto.Name,
            Description = serviceDto.Description,
            Eligibility = GetEligibility(serviceDto.Eligibilities),
            Cost = GetCostOption(serviceDto.CostOptions),
            MoreDetails = "More Details", // TODO: Where does this come from?
            Deliveries = GetDeliveries(serviceDto.ServiceDeliveries),

            Categories = serviceDto.Taxonomies.Select(t => t.Name).Order(),
            Languages = serviceDto.Languages.Select(l => l.Name).Order(),

            Locations = GetLocations(serviceDto.Locations, serviceDto.ServiceAtLocations),
            Contact = new Contact() // TODO: Contact
        };

        return serviceDetailModel;
    }

    public async Task<IActionResult> OnGetAsync(long serviceId)
    {
        ServiceDto serviceDto = await GetService(serviceId);

        Service = GetServiceDetailModel(serviceDto);

        return Page();
    }
}