using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Pages.Shared;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Pages;

//todo: all pages seem to assume that user has been through the welcome page by accessing data from the cache, but not handling when it's not there
// which means that if a user bookmarks a page, they'll get an error (or if the cache expires, although that would depend on cache timeout vs login timeout)

public enum MenuPage
{
    La,
    Vcs,
    Dfe
}

[Authorize(Roles=RoleGroups.AdminRole)]
public class WelcomeModel : HeaderPageModel
{
    public MenuPage MenuPage { get; set; }
    public string? Heading { get; set; }
    public string? CaptionText { get; set; }
    public string Description { get; set; } = string.Empty;

    private readonly ICacheService _cacheService;
    private readonly IServiceDirectoryClient _serviceDirectoryClient;

    public WelcomeModel(
        ICacheService cacheService,
        IServiceDirectoryClient serviceDirectoryClient)
    {
        _cacheService = cacheService;
        _serviceDirectoryClient = serviceDirectoryClient;
    }

    public async Task OnGet()
    {
        var familyHubsUser = HttpContext.GetFamilyHubsUser();
        await SetModelPropertiesBasedOnRole(familyHubsUser);

        await _cacheService.ResetLastPageName();
    }

    private async Task SetModelPropertiesBasedOnRole(FamilyHubsUser familyHubsUser)
    {
        Heading = familyHubsUser.FullName;
        CaptionText = await GetOrganisationName(familyHubsUser);
        const string dfeAndLaAdminDescription = "Manage users, services, locations, organisations and view performance data.";
        
        switch (familyHubsUser.Role)
        {
            case RoleTypes.DfeAdmin:
                Description = dfeAndLaAdminDescription;
                MenuPage = MenuPage.Dfe;
                break;
            case RoleTypes.LaManager:
            case RoleTypes.LaDualRole:
                Description = dfeAndLaAdminDescription;
                MenuPage = MenuPage.La;
                break;
            case RoleTypes.VcsManager:
            case RoleTypes.VcsDualRole:
                Description = "Manage services and locations.";
                MenuPage = MenuPage.Vcs;
                break;
            default:
                throw new InvalidOperationException($"Unknown role: {familyHubsUser.Role}");
        }
    }

    private async Task<string> GetOrganisationName(FamilyHubsUser familyHubsUser)
    {
        if(familyHubsUser.Role == RoleTypes.DfeAdmin)
        {
            return "Department for Education";
        }
        
        var parseOrgId = long.TryParse(familyHubsUser.OrganisationId, out var organisationId);
        if (!parseOrgId)
        {
            throw new InvalidOperationException($"Could not parse OrganisationId from claim: {organisationId}");
        }
        
        var org = await _serviceDirectoryClient.GetOrganisationById(organisationId);
        return org.Name;
    }

    public IActionResult OnGetAddPermissionFlow()
    {
        _cacheService.StoreUserFlow("AddPermissions");
        return RedirectToPage("/TypeOfRole", new { area = "AccountAdmin", cacheid = Guid.NewGuid() });
    }

    public async Task<IActionResult> OnGetAddOrganisation()
    {
        await _cacheService.StoreUserFlow("AddOrganisation");
        await _cacheService.ResetString(CacheKeyNames.LaOrganisationId);
        await _cacheService.ResetString(CacheKeyNames.AddOrganisationName);
        return RedirectToPage("/AddOrganisationWhichLocalAuthority", new { area = "vcsAdmin" });
    }
}
