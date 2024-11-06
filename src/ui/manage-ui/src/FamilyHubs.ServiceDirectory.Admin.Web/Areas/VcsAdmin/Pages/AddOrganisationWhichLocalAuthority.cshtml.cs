using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.ViewModel;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Areas.VcsAdmin.Pages
{
    public class AddOrganisationWhichLocalAuthorityModel : InputPageViewModel
    {
        [BindProperty]
        public required string LaOrganisationName { get; set; } = string.Empty;

        [BindProperty]
        public string? FromUrl { get; set; }

        public required List<string> LocalAuthorities { get; set; } = new List<string>();

        private readonly ICacheService _cacheService;
        private readonly IServiceDirectoryClient _serviceDirectoryClient;

        public AddOrganisationWhichLocalAuthorityModel(ICacheService cacheService, IServiceDirectoryClient serviceDirectoryClient) 
        {
            ErrorMessage = "Select a local authority";
            ErrorElementId = "LaOrganisationName";
            _cacheService = cacheService;
            _serviceDirectoryClient = serviceDirectoryClient;
            PageHeading = "Which local authority is the organisation in?";
        }

        public async Task<IActionResult> OnGet(string fromUrl)
        {
            /*
             * TODO: FHB-678 .. DfE Admin
             *
             * This button needs to go back to the Add User flow if the journey is adding a user.
             * But if it's accessed from the manage service place, it needs to go back there (with all the query stuff)
             * Can technically also enter this page from the /Welcome screen specifically as dfe admin, so it needs to go there too .....
             */
            // todo: validate fromurl
            BackButtonPath = fromUrl;

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

                return RedirectToPage("/AddOrganisation", new { FromUrl });
            }

            HasValidationError = true;

            LocalAuthorities = laOrganisations.Select(l => l.Name).ToList();

            return Page();
        }      

        
    }
}
