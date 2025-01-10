using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Errors;
using FamilyHubs.ServiceDirectory.Admin.Web.ViewModel;
using FamilyHubs.SharedKernel.Razor.ErrorNext;
using Microsoft.AspNetCore.Mvc;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Areas.VcsAdmin.Pages
{
    public class UpdateOrganisationModel : InputPageViewModel
    {
        private readonly ICacheService _cacheService;
        private readonly IServiceDirectoryClient _serviceDirectoryClient;

        [BindProperty(SupportsGet = true)]
        public string OrganisationId { get; set; } = string.Empty;

        [BindProperty]
        public string OrganisationName { get; set; } = string.Empty;

        public UpdateOrganisationModel(ICacheService cacheService, IServiceDirectoryClient serviceDirectoryClient)
        {
            _cacheService = cacheService;
            _serviceDirectoryClient = serviceDirectoryClient;

            PageHeading = "What is the organisation's name?";
            Errors = ErrorState.Empty;
        }

        public async Task OnGet()
        {
            OrganisationName = await _cacheService.RetrieveString(CacheKeyNames.UpdateOrganisationName);            
            BackButtonPath = $"/VcsAdmin/ViewOrganisation/{OrganisationId}";
        }

        public async Task<IActionResult> OnPost()
        {
            BackButtonPath = $"/VcsAdmin/ViewOrganisation/{OrganisationId}";

            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(OrganisationName) && OrganisationName.Length <= 255)
            {
                var laOrganisationId = await _cacheService.RetrieveString(CacheKeyNames.LaOrganisationId);
                var existingOrganisations = await _serviceDirectoryClient.GetCachedVcsOrganisations(long.Parse(laOrganisationId));
                if (existingOrganisations.Exists(x => x.Name == OrganisationName))
                {
                    await _cacheService.StoreCurrentPageName($"/VcsAdmin/UpdateOrganisation/{OrganisationId}");
                    return RedirectToPage("AddOrganisationAlreadyExists");
                }

                await _cacheService.StoreString(CacheKeyNames.UpdateOrganisationName, OrganisationName);
                return RedirectToPage($"ViewOrganisation", new { OrganisationId = OrganisationId, updated = true });
            }

            Errors = ErrorState.Create(PossibleErrors.All, ErrorId.Update_Organisation__Organisation_Missing);

            return Page();
        }
    }
}
