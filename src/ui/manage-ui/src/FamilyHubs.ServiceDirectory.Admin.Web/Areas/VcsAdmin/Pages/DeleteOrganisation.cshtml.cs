using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Errors;
using FamilyHubs.ServiceDirectory.Admin.Web.Pages.Shared;
using FamilyHubs.SharedKernel.Razor.ErrorNext;
using FamilyHubs.SharedKernel.Razor.Header;
using Microsoft.AspNetCore.Mvc;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Areas.VcsAdmin.Pages
{
    public class DeleteOrganisationModel : HeaderPageModel, IHasErrorStatePageModel
    {
        private readonly IServiceDirectoryClient _serviceDirectoryClient;
        private readonly ICacheService _cacheService;
        private readonly IIdamClient _idamClient;

        public bool HasValidationError { get; set; }
        public string BackButtonPath { get; set; } = "/VcsAdmin/ManageOrganisations";
        public string? OrganisationName { get; set; }

        [BindProperty]
        public required bool? DeleteOrganisation { get; set; } = null;

        public IErrorState Errors { get; private set; }

        public DeleteOrganisationModel(IServiceDirectoryClient serviceDirectoryClient, ICacheService cacheService, IIdamClient idamClient)
        {
            _serviceDirectoryClient = serviceDirectoryClient;
            _cacheService = cacheService;
            _idamClient = idamClient;
            Errors = ErrorState.Empty;
        }

        public async Task OnGet(long organisationId)
        {
            var organisation = await _serviceDirectoryClient.GetOrganisationById(organisationId);
            if ( organisation is not null )
            {
                OrganisationName = organisation.Name;
            }
            await SetBackButton();
            await _cacheService.StoreString("DeleteOrganisationName", OrganisationName ?? string.Empty);
        }

        public async Task<IActionResult> OnPost(long organisationId)
        {
            if ( ModelState.IsValid && DeleteOrganisation is not null )
            {
                if (DeleteOrganisation.Value)
                {
                    await _idamClient.DeleteOrganisationAccounts(organisationId);                    
                    await _serviceDirectoryClient.DeleteOrganisation(organisationId);

                    return RedirectToPage("DeleteOrganisationResult", new { OrganisationId = organisationId, IsDeleted = true });
                }
                else
                {
                    return RedirectToPage("DeleteOrganisationResult", new { OrganisationId = organisationId, IsDeleted = false });
                }
                
            }
            
            HasValidationError = true;

            Errors = ErrorState.Create(PossibleErrors.All, ErrorId.Delete_Organisation);

            return Page();

        }

        private async Task SetBackButton()
        {
            var cachedBackPath = await _cacheService.RetrieveLastPageName();

            //  Check that the cached route matches the expected return route. This is incase
            //  someone has bookmarked the page and come directly here
            if (!string.IsNullOrEmpty(cachedBackPath) && cachedBackPath.Contains(BackButtonPath))
            {
                BackButtonPath = cachedBackPath;
            }
        }
    }
}
