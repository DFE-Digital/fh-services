using System.Text;
using FamilyHubs.ServiceDirectory.Core.ServiceDirectory.Interfaces;
using FamilyHubs.ServiceDirectory.Shared.Display;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.ServiceDirectory.Web.Models.ServiceDetail;
using FamilyHubs.SharedKernel.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Location = FamilyHubs.ServiceDirectory.Web.Models.ServiceDetail.Location;

namespace FamilyHubs.ServiceDirectory.Web.Pages.ServiceDetail;

public class Index : PageModel
{
    private readonly IServiceDirectoryClient _serviceDirectoryClient;

    public string BackUrl { get; set; } = null!;
    public ServiceDetailModel Service { get; set; } = null!;
    public bool HasContactDetails => 
        !string.IsNullOrEmpty(Service.Contact.Email) || 
        !string.IsNullOrEmpty(Service.Contact.Phone) ||
        !string.IsNullOrEmpty(Service.Contact.TextMessage) || 
        !string.IsNullOrEmpty(Service.Contact.Website);

    public Index(IServiceDirectoryClient serviceDirectoryClient)
    {
        _serviceDirectoryClient = serviceDirectoryClient;
    }

    private async Task<ServiceDto> GetService(long serviceId) =>
        await _serviceDirectoryClient.GetServiceById(serviceId);

    private static string GetAttendingTypes(IEnumerable<AttendingType> attendingTypes) =>
        string.Join(", ", attendingTypes.Select(attendingType => attendingType.ToDescription()));

    private static string GetOnlineTelephoneHeader(string attendingTypes)
    {
        StringBuilder onlineTelephoneHeader = new();

        if (attendingTypes.Contains("Online", StringComparison.InvariantCultureIgnoreCase))
        {
            onlineTelephoneHeader.Append("Online");
        }

        if (attendingTypes.Contains("Telephone", StringComparison.InvariantCultureIgnoreCase))
        {
            onlineTelephoneHeader.Append(onlineTelephoneHeader.Length == 0 ? "Telephone" : " and telephone");
        }

        return onlineTelephoneHeader.ToString();
    }

    private static IEnumerable<AttendingType> GetDeliveries(ICollection<ServiceDeliveryDto> serviceDeliveries)
        => serviceDeliveries.Select(x => x.Name).Order();

    private static string GetEligibility(ICollection<EligibilityDto> serviceEligibilities)
        => serviceEligibilities.Count == 0
            ? "No"
            : $"Yes, {AgeDisplayExtensions.AgeToString(serviceEligibilities.First().MinimumAge)} to {AgeDisplayExtensions.AgeToString(serviceEligibilities.First().MaximumAge)} years old";

    private static string GetCostOption(ICollection<CostOptionDto> serviceCostOptions)
        => serviceCostOptions.Count == 0 ? "Free" : "Yes, it costs money to use.";

    private static string? GetDaysAvailable(ScheduleDto? scheduleDto) => scheduleDto?.ByDay?.Split(',').GetDayNames();

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
                Schedule = new Schedule
                {
                    DaysAvailable = GetDaysAvailable(locationSchedule),
                    ExtraAvailabilityDetails = locationSchedule?.Description
                },
                Address = serviceLocation.GetAddress(),
                Accessibilities = GetAccessibilities(serviceLocation.AccessibilityForDisabilities)
            });

        return locations;
    }

    private static ScheduleDto? GetScheduleForLocation(long locationId,
        ICollection<ServiceAtLocationDto> serviceAtLocations)
        => serviceAtLocations.FirstOrDefault(x => x.LocationId == locationId)?.Schedules.FirstOrDefault();

    private static string IsFamilyHub(LocationTypeCategory locationTypeCategory) =>
        locationTypeCategory == LocationTypeCategory.FamilyHub ? "Yes" : "No";

    private static IEnumerable<string> GetAccessibilities(
        ICollection<AccessibilityForDisabilitiesDto> accessibilityForDisabilities)
        => [..accessibilityForDisabilities.Select(x => x.Accessibility).OfType<string>()];

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

        string attendingTypes = GetAttendingTypes(GetDeliveries(serviceDto.ServiceDeliveries));
        string onlineTelephoneHeader = GetOnlineTelephoneHeader(attendingTypes);

        ServiceDetailModel serviceDetailModel = new()
        {
            Name = serviceDto.Name,
            Summary = serviceDto.Summary,
            Eligibility = GetEligibility(serviceDto.Eligibilities),
            Cost = GetCostOption(serviceDto.CostOptions),
            MoreDetails = serviceDto.Description,
            AttendingTypes = attendingTypes,

            OnlineTelephone = onlineTelephoneHeader,
            Schedule = !string.IsNullOrWhiteSpace(onlineTelephoneHeader)
                ? new Schedule
                {
                    DaysAvailable = GetDaysAvailable(serviceScheduleDto) ?? "None provided",
                    ExtraAvailabilityDetails = serviceScheduleDto?.Description ?? "None provided",
                }
                : null,

            Categories = serviceDto.Taxonomies.Select(t => t.Name).Order(),
            Languages = serviceDto.Languages.Select(l => l.Name).Order(),

            Locations = GetLocations(serviceDto.Locations, serviceDto.ServiceAtLocations),
            Contact = GetContact(serviceDto.Contacts)
        };

        return serviceDetailModel;
    }

    public async Task<IActionResult> OnGetAsync(long serviceId, string fromUrl)
    {
        if (!fromUrl.StartsWith("/ServiceFilter"))
        {
            throw new ArgumentException($"Back button URL is not valid: {fromUrl}");
        }

        ServiceDto serviceDto = await GetService(serviceId);

        BackUrl = fromUrl;
        Service = GetServiceDetailModel(serviceDto);

        return Page();
    }
}