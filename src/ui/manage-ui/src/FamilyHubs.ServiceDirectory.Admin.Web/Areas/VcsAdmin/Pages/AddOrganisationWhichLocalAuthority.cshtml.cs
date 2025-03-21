using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Errors;
using FamilyHubs.ServiceDirectory.Admin.Web.ViewModel;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Razor.ErrorNext;
using Microsoft.AspNetCore.Mvc;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Areas.VcsAdmin.Pages
{
    public class AddOrganisationWhichLocalAuthorityModel : InputPageViewModel
    {
        [BindProperty]
        public required string LaOrganisationName { get; set; } = string.Empty;

        public required List<string> LocalAuthorities { get; set; } = new List<string>();

        private readonly ICacheService _cacheService;
        private readonly IServiceDirectoryClient _serviceDirectoryClient;

        public AddOrganisationWhichLocalAuthorityModel(ICacheService cacheService, IServiceDirectoryClient serviceDirectoryClient) 
        {
            Errors = ErrorState.Empty;
            _cacheService = cacheService;
            _serviceDirectoryClient = serviceDirectoryClient;
            PageHeading = "Which local authority is the organisation in?";
            BackButtonPath = "/Welcome";
        }

        public async Task<IActionResult> OnGet()
        {
            if (!HttpContext.IsUserDfeAdmin())
            {
                var userOrganisationId = HttpContext.GetUserOrganisationId();
                await _cacheService.StoreString(CacheKeyNames.LaOrganisationId, userOrganisationId.ToString());
                return RedirectToPage("/AddOrganisation");
            }

            var localAuthorities = await _serviceDirectoryClient.GetCachedLaOrganisations();
            LocalAuthorities = localAuthorities.Select(l => l.Name).ToList();

            var laOrganisationId = await _cacheService.RetrieveString(CacheKeyNames.LaOrganisationId);
            if(!string.IsNullOrEmpty(laOrganisationId))
            {
                var laOrganisation = localAuthorities.Find(x => x.Id.ToString() == laOrganisationId);
                if(laOrganisation != null)
                {
                    LaOrganisationName= laOrganisation.Name;
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var laOrganisations = await _serviceDirectoryClient.GetCachedLaOrganisations();
            var laOrganisationsNames = laOrganisations.Select(x=>x.Name).ToList(); 

            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(LaOrganisationName) && LaOrganisationName.Length <= 255
                && laOrganisationsNames.Contains(LaOrganisationName))
            {
                var laOrganisationId = laOrganisations.Single(l => l.Name == LaOrganisationName).Id;
                await _cacheService.StoreString(CacheKeyNames.LaOrganisationId, laOrganisationId.ToString());

                return RedirectToPage("/AddOrganisation");
            }

            Errors = ErrorState.Create(PossibleErrors.All, ErrorId.Add_Organisation__WhichLocalAuthority_Missing);

            LocalAuthorities = laOrganisations.Select(l => l.Name).ToList();

            return Page();
        }
    }
}
