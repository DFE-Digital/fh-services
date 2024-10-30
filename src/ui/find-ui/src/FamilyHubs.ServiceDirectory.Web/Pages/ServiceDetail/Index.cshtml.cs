using System.Text;
using FamilyHubs.ServiceDirectory.Core.ServiceDirectory.Interfaces;
using FamilyHubs.ServiceDirectory.Shared.Display;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.ServiceDirectory.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Location = FamilyHubs.ServiceDirectory.Web.Models.Location;

namespace FamilyHubs.ServiceDirectory.Web.Pages.ServiceDetail;

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

    private static IEnumerable<AttendingType> GetDeliveries(ICollection<ServiceDeliveryDto> serviceDeliveries)
        => serviceDeliveries.Select(x => x.Name).Order();

    private static string IsFamilyHub(LocationTypeCategory locationTypeCategory) =>
        locationTypeCategory == LocationTypeCategory.FamilyHub ? "Yes" : "No";

    private static string? GetDaysAvailableOnlineOrTelephone(ScheduleDto? scheduleDto) =>
        scheduleDto?.ByDay?.Split(',').GetDayNames(); // TODO: FHB-712 - Common Method

    private static ScheduleDto? GetScheduleForLocation(long locationId,
        ICollection<ServiceAtLocationDto> serviceAtLocations)
        => serviceAtLocations.FirstOrDefault(x => x.LocationId == locationId)?.Schedules.FirstOrDefault();

    private static IEnumerable<string> GetAccessibilities(
        ICollection<AccessibilityForDisabilitiesDto> serviceAccessibilityForDisabilities)
    {
        List<string> accessibilities = [];

        accessibilities
            .AddRange(serviceAccessibilityForDisabilities
                .Select(x => x.Accessibility)
                .OfType<string>());

        return accessibilities;
    }

    private static string GetDaysAvailable(ScheduleDto locationSchedule) =>
        (locationSchedule.ByDay?.Split(',')).GetDayNames(); // TODO: FHB-712 - Common Method

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
                DaysAvailable = locationSchedule is not null ? GetDaysAvailable(locationSchedule) : null,
                ExtraAvailabilityDetails = locationSchedule?.Description,
                Address = serviceLocation.GetAddress(),
                Accessibilities = GetAccessibilities(serviceLocation.AccessibilityForDisabilities)
            });

        return locations;
    }

    private static Contact GetContact(ICollection<ContactDto> serviceContacts)
    {
        ContactDto? contactDto = serviceContacts.FirstOrDefault();

        return new Contact
        {
            Email = contactDto?.Email,
            Phone = contactDto?.Telephone,
            TextMessage = contactDto?.TextPhone,
            Website = contactDto?.Url
        };
    }

    private static ServiceDetailModel GetServiceDetailModel(ServiceDto serviceDto)
    {
        ScheduleDto? serviceScheduleDto =
            serviceDto.Schedules.FirstOrDefault(x => x.ServiceId == serviceDto.Id);

        IEnumerable<AttendingType> deliveries = GetDeliveries(serviceDto.ServiceDeliveries);

        StringBuilder deliveryString = new();
        StringBuilder onlineTelephoneString = new();

        foreach (AttendingType delivery in deliveries) // TODO: FHB-712 - Move into own method, possibly 2 methods!!!
        {
            switch (delivery)
            {
                case AttendingType.InPerson:
                    deliveryString.Append("In person");
                    break;
                case AttendingType.Online:
                    deliveryString.Append(", Online");
                    onlineTelephoneString.Append("Online");
                    break;
                case AttendingType.Telephone:
                    deliveryString.Append(", Telephone");
                    onlineTelephoneString.Append(onlineTelephoneString.Length == 0 ? "Telephone" : " and telephone");
                    break;
            }
        }

        ServiceDetailModel serviceDetailModel = new()
        {
            Name = serviceDto.Name,
            Summary = serviceDto.Summary,
            Eligibility = GetEligibility(serviceDto.Eligibilities),
            Cost = GetCostOption(serviceDto.CostOptions),
            MoreDetails = serviceDto.Description,
            Deliveries = deliveryString.ToString(),

            OnlineTelephone = onlineTelephoneString.ToString(),
            DaysAvailable = GetDaysAvailableOnlineOrTelephone(serviceScheduleDto) ?? "None provided",
            ExtraAvailabilityDetails = serviceScheduleDto?.Description ?? "None provided",

            Categories = serviceDto.Taxonomies.Select(t => t.Name).Order(),
            Languages = serviceDto.Languages.Select(l => l.Name).Order(),

            Locations = GetLocations(serviceDto.Locations, serviceDto.ServiceAtLocations),
            Contact = GetContact(serviceDto.Contacts)
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