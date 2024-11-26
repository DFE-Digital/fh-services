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
        switch (familyHubsUser.Role)
        {
            case RoleTypes.DfeAdmin:
                SetDfeAdminProperties(familyHubsUser);
                break;
            case RoleTypes.LaManager:
            case RoleTypes.LaDualRole:
                await SetLaAdminProperties(familyHubsUser);
                break;
            case RoleTypes.VcsManager:
            case RoleTypes.VcsDualRole:
                await SetVcsAdminProperties(familyHubsUser);
                break;
            default:
                throw new InvalidOperationException($"Unknown role: {familyHubsUser.Role}");
        }
    }
    
    private void SetDfeAdminProperties(FamilyHubsUser familyHubsUser)
    {
        Heading = familyHubsUser.FullName;
        CaptionText = "Department for Education";
        Description = "Manage users, services, locations and organisations and view performance data.";
        MenuPage = MenuPage.Dfe;
    }
    
    private async Task SetLaAdminProperties(FamilyHubsUser familyHubsUser)
    {
        CaptionText = await TryGetOrganisationName(familyHubsUser);
        Description = "Manage users, services, locations and organisations and view performance data.";
        Heading = familyHubsUser.FullName;
        MenuPage = MenuPage.La;
    }
    
    private async Task SetVcsAdminProperties(FamilyHubsUser familyHubsUser)
    {
        CaptionText = await TryGetOrganisationName(familyHubsUser);
        Description = "Manage services, locations and organisations and view performance data.";
        Heading = familyHubsUser.FullName;
        MenuPage = MenuPage.Vcs;
    }

    private async Task<string> TryGetOrganisationName(FamilyHubsUser familyHubsUser)
    {
        if (!long.TryParse(familyHubsUser.OrganisationId, out var organisationId))
        {
            return "";
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
