using FamilyHubs.ServiceDirectory.Admin.Core;
using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Pages.Shared;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Areas.VcsAdmin.Pages;

public class ViewOrganisationModel : HeaderPageModel
{
    private readonly IServiceDirectoryClient _serviceDirectoryClient;
    private readonly ICacheService _cacheService;
    private readonly ILogger<ViewOrganisationModel> _logger;

    [BindProperty(SupportsGet = true)]
    public string OrganisationId { get; set; } = string.Empty;

    [BindProperty]
    public string OrganisationName { get; set; } = string.Empty;

    [BindProperty]
    public string LocalAuthority { get; set; } = string.Empty;

    [BindProperty]
    public string OrganisationType { get; set; } = "Voluntary community organisation";

    public string BackPath { get; set; } = "/VcsAdmin/ManageOrganisations";

    public IEnumerable<ServiceNameDto> Services { get; set; } = [];

    public bool CanSave { get; set; } = false;

    public ViewOrganisationModel(IServiceDirectoryClient serviceDirectoryClient, ICacheService cacheService, ILogger<ViewOrganisationModel> logger)
    {
        _serviceDirectoryClient = serviceDirectoryClient;
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<IActionResult> OnGet(bool? updated = false)
    {
        await SetBackButton();
        var outcome = await SetOrganisationDetails(updated);

        if (!outcome.IsSuccess)
        {
            return outcome.FailureResult!;
        }

        Services = await GetServicesBelongingToOrganisation();

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var outcome = await SetOrganisationDetails(true);
        if (outcome.IsSuccess)
        {
            var result = await _serviceDirectoryClient.UpdateOrganisation(outcome.SuccessResult!);
            var id = outcome.SuccessResult!.Id;
            if (result == id)
            {
                return RedirectToPage("UpdateOrganisationResult");
            }

            throw new InvalidOperationException($"Unexpected result from organisation Update, was expecting Id to be {id} but was {result}");
        }

        return outcome.FailureResult!;
    }

    private async Task<IEnumerable<ServiceNameDto>> GetServicesBelongingToOrganisation()
    {
        var serviceSummaries = await _serviceDirectoryClient.GetServiceSummaries(long.Parse(OrganisationId), null, 1, int.MaxValue);

        return serviceSummaries.Items;
    }

    private async Task<Outcome<OrganisationDetailsDto, IActionResult>> SetOrganisationDetails(bool? updated = false)
    {
        var organisation = await _serviceDirectoryClient.GetOrganisationById(long.Parse(OrganisationId));

        if (organisation == null)
        {
            _logger.LogWarning("Organisation {OrganisationId} not found", OrganisationId);
            return new Outcome<OrganisationDetailsDto, IActionResult>(RedirectToPage("/Error/404"));
        }

        if (organisation.OrganisationType != Shared.Enums.OrganisationType.VCFS)
        {
            _logger.LogWarning("Organisation {OrganisationId} is not a VCS organisation", OrganisationId);
            return new Outcome<OrganisationDetailsDto, IActionResult>(RedirectToPage("/Error/404"));
        }

        if (organisation.AssociatedOrganisationId == null)
        {
            _logger.LogWarning("Organisation {OrganisationId} has no parent", OrganisationId);
            return new Outcome<OrganisationDetailsDto, IActionResult>(RedirectToPage("/Error/404"));
        }

        var user = HttpContext.GetFamilyHubsUser();
        if (user.Role != RoleTypes.DfeAdmin && organisation.AssociatedOrganisationId.ToString() != user.OrganisationId)
        {
            _logger.LogWarning("User {UserEmail} cannot view {OrganisationId}", user.Email, OrganisationId);
            return new Outcome<OrganisationDetailsDto, IActionResult>(RedirectToPage("/Error/403"));
        }

        var localAuthority =
            await _serviceDirectoryClient.GetOrganisationById(organisation.AssociatedOrganisationId.Value);

        if (localAuthority == null)
        {
            _logger.LogWarning("Organisation {OrganisationId} Parent {AssociatedOrganisationId} not found",
                OrganisationId, organisation.AssociatedOrganisationId);
            return new Outcome<OrganisationDetailsDto, IActionResult>(RedirectToPage("/Error/404"));
        }

        if (updated.HasValue && updated.Value)
        {
            OrganisationName = await _cacheService.RetrieveString(CacheKeyNames.UpdateOrganisationName);
            CanSave = true;
            organisation.Name = OrganisationName;
        }
        else
        {
            OrganisationName = organisation.Name;
        }
            
        await _cacheService.StoreString(CacheKeyNames.UpdateOrganisationName, OrganisationName);
        await _cacheService.StoreString(CacheKeyNames.LaOrganisationId, localAuthority.Id.ToString());

        LocalAuthority = localAuthority.Name;
        return new Outcome<OrganisationDetailsDto, IActionResult>(organisation);
    }

    private async Task SetBackButton()
    {
        var cachedBackPath = await _cacheService.RetrieveLastPageName();

        //  Check that the cached route matches the expected return route. This is in case
        //  someone has bookmarked the page and come directly here
        if (!string.IsNullOrEmpty(cachedBackPath) && cachedBackPath.Contains(BackPath))
        {
            BackPath = cachedBackPath;
        }
    }
}