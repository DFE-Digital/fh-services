using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.DistributedCache;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Models.ServiceJourney;
using FamilyHubs.ServiceDirectory.Admin.Web.Journeys;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Pages.manage_services;

public class start_edit_serviceModel : PageModel
{
    private readonly IRequestDistributedCache _cache;
    private readonly IServiceDirectoryClient _serviceDirectoryClient;

    public start_edit_serviceModel(
        IRequestDistributedCache cache,
        IServiceDirectoryClient serviceDirectoryClient)
    {
        _cache = cache;
        _serviceDirectoryClient = serviceDirectoryClient;
    }

    public async Task<IActionResult> OnGetAsync(long? serviceId, ServiceDetailEntrance entryPoint, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(serviceId);

        var service = await _serviceDirectoryClient.GetServiceById(serviceId.Value, cancellationToken);

        var familyHubsUser = HttpContext.GetFamilyHubsUser();

        var serviceModel = await CreateServiceModel(serviceId.Value, entryPoint, service, familyHubsUser.Role, cancellationToken);

        // the user's just starting the journey
        await _cache.SetAsync(familyHubsUser.Email, serviceModel);

        return Redirect(ServiceJourneyPageExtensions.GetEditStartPagePath());
    }

    private async Task<ServiceModel> CreateServiceModel(
        long serviceId,
        ServiceDetailEntrance entryPoint,
        ServiceDto service,
        string userRole,
        CancellationToken cancellationToken)
    {
        var serviceModel = new ServiceModel
        {
            Id = serviceId,
            Name = service.Name,
            EntryPoint = entryPoint,
            Description = service.Summary,
            MoreDetails = service.Description,
            ServiceType = GetServiceTypeArg(service),
        };

        // serviceModel already contains serviceModel, but we explicitly pass ServiceType rather than have the method assume the ServiceModel is already part populated
        await AddOrganisationIds(service, serviceModel, userRole, serviceModel.ServiceType.Value, cancellationToken);
        AddWhoFor(service, serviceModel);
        AddServiceCost(service, serviceModel);
        AddSupportOffered(service, serviceModel);
        AddTimes(service, serviceModel);
        AddLanguages(service, serviceModel);
        AddHowUse(service, serviceModel);
        AddContacts(service, serviceModel);
        AddLocations(service, serviceModel);

        return serviceModel;
    }

    //todo: should these belong on ServiceModel?
    private async Task AddOrganisationIds(
        ServiceDto service,
        ServiceModel serviceModel,
        string userRole,
        ServiceTypeArg serviceType,
        CancellationToken cancellationToken)
    {
        serviceModel.OrganisationId = service.OrganisationId;

        if (userRole == RoleTypes.DfeAdmin)
        {
            if (serviceType == ServiceTypeArg.Vcs)
            {
                // we could do this concurrently with getting the service, but this is simpler for an edge case
                var organisation = await _serviceDirectoryClient.GetOrganisationById(service.OrganisationId, cancellationToken);
                serviceModel.LaOrganisationId = organisation.AssociatedOrganisationId;
            }
            else
            {
                serviceModel.LaOrganisationId = service.OrganisationId;
            }
        }
    }

    //todo: just use the existing ServiceType enum?
    private ServiceTypeArg GetServiceTypeArg(ServiceDto service)
    {
        // this logic only holds whilst LA's can only create FamilyExperience services
        return service.ServiceType switch
        {
            ServiceType.FamilyExperience => ServiceTypeArg.La,
            ServiceType.InformationSharing => ServiceTypeArg.Vcs,
            _ => throw new InvalidOperationException($"Unexpected ServiceType {service.ServiceType}")
        };
    }

    private void AddLocations(ServiceDto service, ServiceModel serviceModel)
    {
        serviceModel.Locations = service.ServiceAtLocations
            .Select(sal => new ServiceLocationModel(
                service.Locations.First(l => l.Id == sal.LocationId), sal))
            .ToList();
    }

    private void AddContacts(ServiceDto service, ServiceModel serviceModel)
    {
        var contact = service.Contacts.FirstOrDefault();
        // some legacy services have contacts details that are empty strings,
        // which we convert to nulls here, otherwise they'll fail validation if we write them to the database later
        serviceModel.HasEmail = !string.IsNullOrWhiteSpace(contact?.Email);
        serviceModel.Email = serviceModel.HasEmail ? contact!.Email : null;
        serviceModel.HasTelephone = !string.IsNullOrWhiteSpace(contact?.Telephone);
        serviceModel.TelephoneNumber = serviceModel.HasTelephone ? contact!.Telephone : null;
        serviceModel.HasWebsite = !string.IsNullOrWhiteSpace(contact?.Url);
        serviceModel.Website = serviceModel.HasWebsite ? contact!.Url : null;
        serviceModel.HasTextMessage = !string.IsNullOrWhiteSpace(contact?.TextPhone);
        serviceModel.TextTelephoneNumber = serviceModel.HasTextMessage ? contact!.TextPhone : null;
    }

    private static void AddLanguages(ServiceDto service, ServiceModel serviceModel)
    {
        serviceModel.LanguageCodes = service.Languages
            .Select(l => l.Code)
            .ToList();

        //todo: move to sd shared?
        service.InterpretationServices?.Split(',').ToList().ForEach(s =>
        {
            switch (s)
            {
                //todo: magic strings in service directory shared
                case "translation":
                    serviceModel.TranslationServices = true;
                    break;
                case "bsl":
                    serviceModel.BritishSignLanguage = true;
                    break;
            }
        });
    }

    private static void AddHowUse(ServiceDto service, ServiceModel serviceModel)
    {
        serviceModel.HowUse = service.ServiceDeliveries
            .Select(sd => sd.Name)
            .ToArray();
    }

    private static void AddTimes(ServiceDto service, ServiceModel serviceModel)
    {
        //todo: we could have multiple schedules that should all be the same
        // but should we be a bit more defensive here?
        var serviceSchedule = service.Schedules
            .FirstOrDefault();

        serviceModel.Times = serviceSchedule?.ByDay?.Split(",")
                             ?? Enumerable.Empty<string>();

        serviceModel.TimeDescription = serviceSchedule?.Description;
        serviceModel.HasTimeDetails = serviceModel.TimeDescription != null;
    }

    private static void AddSupportOffered(ServiceDto service, ServiceModel serviceModel)
    {
        serviceModel.SelectedCategories = service.Taxonomies
            .Select(x => x.ParentId)
            .Distinct()
            .ToList();
        serviceModel.SelectedSubCategories = service.Taxonomies
            .Select(x => x.Id)
            .ToList();
    }

    private static void AddServiceCost(ServiceDto service, ServiceModel serviceModel)
    {
        serviceModel.HasCost = service.CostOptions.Count > 0;
        if (serviceModel.HasCost != true)
            return;

        var cost = service.CostOptions.First();

        // we can't do this here, because it'd look like the service had a cost description when it doesn't (unless the user changed something on the service and saved)
        // we can't do it on the cost page, as it would look like a bug that the description is on the cost page, but not on the service details page
        // we'd have to do it as a batch process, which would be better done in sql during a migration
        //if (!string.IsNullOrEmpty(cost.Option) && cost.Amount != null && cost.Amount != decimal.Zero)
        //{
        //    // the service has a structured cost
        //    // it's either one of the original services imported from a spreadsheet
        //    // or it could be one imported at a later date

        //    // make a sensible description from the structured cost
        //    var currencyAmount = cost.Amount.Value.ToString("C", new CultureInfo("en-GB"));
        //    serviceModel.CostDescription = $"{currencyAmount} every {cost.Option.ToLowerInvariant()}{(!string.IsNullOrEmpty(cost.AmountDescription) ? $"{Environment.NewLine}{cost.AmountDescription}" : "")}";
        //}
        //else
        //{
            serviceModel.CostDescription = cost.AmountDescription!;
        //}
    }

    private static void AddWhoFor(ServiceDto service, ServiceModel serviceModel)
    {
        var eligibility = service.Eligibilities.FirstOrDefault();
        serviceModel.ForChildren = eligibility != null;
        if (serviceModel.ForChildren == true)
        {
            serviceModel.MinimumAge = eligibility!.MinimumAge;
            serviceModel.MaximumAge = eligibility.MaximumAge;
        }
    }
}