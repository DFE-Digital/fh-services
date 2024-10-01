using FamilyHubs.Idams.Maintenance.Core.ApiClient;
using FamilyHubs.Idams.Maintenance.Core.Models;
using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.Idams.Maintenance.UI.Pages.Shared;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Identity.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamilyHubs.Idams.Maintenance.UI.Pages;

//[Authorize]
public class WelcomeModel : HeaderPageModel
{
#if MABUSE_DistributedCache
    private readonly ICacheService _cacheService;
    private readonly IServiceDirectoryClient _serviceDirectoryClient;
#endif

    private readonly ILogger<IndexModel> _logger;

    [BindProperty]
    public OrganisationViewModel OrganisationViewModel { get; set; } = new();
    public FamilyHubsUser FamilyHubsUser { get; set; } = new();

    public WelcomeModel(
#if MABUSE_DistributedCache
        ICacheService cacheService, 
#endif
        IServiceDirectoryClient serviceDirectoryClient, ILogger<IndexModel> logger)
    {
        _logger = logger;
#if MABUSE_DistributedCache
        _cacheService = cacheService;
         _serviceDirectoryClient = serviceDirectoryClient;
#endif

    }

#if MABUSE_DistributedCache
    public async Task OnGet()
    {
        FamilyHubsUser = HttpContext.GetFamilyHubsUser();
        await SetOrganisation();
    }
#else
public void OnGet()
    {
        FamilyHubsUser = HttpContext.GetFamilyHubsUser();
        
    }
#endif


    public IActionResult OnGetAddDfEAdminAccount()
    {
        return RedirectToPage("AddDfEAdminAccount");
    }

    public IActionResult OnGetChangeUserPermissions()
    {
        return RedirectToPage("Users");
    }

    public IActionResult OnGetCreateEncryptionKeys()
    {
        return RedirectToPage("CreateEncryptionKeys");
    }

#if MABUSE_DistributedCache
    private async Task SetOrganisation()
    {
        _logger.LogInformation("Setting Organsition in Index Page");


        if (HttpContext.IsUserDfeAdmin())
            return;

        if (await _cacheService.RetrieveOrganisationWithService() == null)
        {
            OrganisationDetailsDto? organisation = null;

            if (long.TryParse(FamilyHubsUser.OrganisationId, out var organisationId))
            {
                organisation = await _serviceDirectoryClient.GetOrganisationById(organisationId);
            }

            if (organisation != null)
            {
                OrganisationViewModel = new OrganisationViewModel
                {
                    Id = organisation.Id,
                    Name = organisation.Name
                };

                await _cacheService.StoreOrganisationWithService(OrganisationViewModel);
            }
        }
        else
        {
            OrganisationViewModel = await _cacheService.RetrieveOrganisationWithService() ?? new OrganisationViewModel();
        }
    }
#endif
}
