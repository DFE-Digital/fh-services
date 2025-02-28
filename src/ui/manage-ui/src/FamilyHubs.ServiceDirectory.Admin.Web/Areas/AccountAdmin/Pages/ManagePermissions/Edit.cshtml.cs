using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Constants;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Mvc;
using FamilyHubs.ServiceDirectory.Admin.Web.Pages.Shared;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Areas.AccountAdmin.Pages.ManagePermissions
{
    public class EditModel : HeaderPageModel
    {
        [BindProperty(SupportsGet = true)]
        public string AccountId { get; set; } = string.Empty; //Route Property

        private readonly IIdamClient _idamClient;
        private readonly IServiceDirectoryClient _serviceDirectoryClient;
        private readonly ICacheService _cacheService;

        public long Id { get; set; } = -1;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Organisation { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string BackPath { get; set; } = "/AccountAdmin/ManagePermissions";

        public EditModel(IIdamClient idamClient, IServiceDirectoryClient serviceDirectoryClient, ICacheService cacheService)
        {
            _idamClient = idamClient;
            _serviceDirectoryClient = serviceDirectoryClient;
            _cacheService = cacheService;
        }

        public async Task<IActionResult> OnGet()
        {
            await SetBackButton();

            if (!long.TryParse(AccountId, out long id))
            {
                // Invalid AccountId
                return RedirectToPage("/ManagePermissions/Index");
            }

            var account = await _idamClient.GetAccountById(id);

            if(account == null)
            {
                // User not found
                return RedirectToPage("/ManagePermissions/Index");
            }

            var organisationName = await GetOrganisationName(account);

            Email = account.Email;
            Name = account.Name;
            Organisation = organisationName;
            Role = GetRoleText(account);
            Id = account.Id;

            return Page();
        }

        private async Task<string> GetOrganisationName(AccountDto account)
        {
            var organisationClaim = account.Claims.Single(x => x.Name == FamilyHubsClaimTypes.OrganisationId);
            var organisationId = long.Parse(organisationClaim.Value);
            var organisation = await _serviceDirectoryClient.GetOrganisationById(organisationId);

            return organisation.Name;
        }

        private string GetRoleText(AccountDto account)
        {
            var roleClaim = account.Claims.Single(x => x.Name == FamilyHubsClaimTypes.Role);
            var role = roleClaim.Value;

            switch (role)
            {
                case RoleTypes.LaManager:
                    return RoleDescription.LaManager;

                case RoleTypes.LaProfessional:
                    return RoleDescription.LaProfessional;

                case RoleTypes.LaDualRole:
                    return $"{RoleDescription.LaManager}, {RoleDescription.LaProfessional}";

                case RoleTypes.VcsManager:
                    return RoleDescription.VcsManager;

                case RoleTypes.VcsProfessional:
                    return RoleDescription.VcsProfessional;

                case RoleTypes.VcsDualRole:
                    return $"{RoleDescription.VcsManager}, {RoleDescription.VcsProfessional}";
            }

            throw new InvalidOperationException("Role type not Valid");
        }

        private async Task SetBackButton()
        {
            var cachedBackPath = await _cacheService.RetrieveLastPageName();

            //  Check that the cached route matches the expected return route. This is incase
            //  someone has bookmarked the page and come directly here
            if (cachedBackPath.Contains(BackPath))
            {
                BackPath = cachedBackPath;
            }
        }
    }
}
