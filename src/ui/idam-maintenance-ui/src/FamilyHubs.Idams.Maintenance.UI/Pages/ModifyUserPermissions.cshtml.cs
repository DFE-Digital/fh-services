using FamilyHubs.Idams.Maintenance.Core.ApiClient;
using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.Idams.Maintenance.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.Idams.Maintenance.UI.Pages;

public class ModifyUserPermissionsModel : PageModel
{
    private readonly IIdamService _idamService;
    private readonly IServiceDirectoryClient _serviceDirectoryClient;

    [BindProperty]
    public long AccountId { get; set; }

    public string Name { get; set; } = string.Empty;

    [BindProperty]
    public required string SelectedRole { get; set; }

    [BindProperty]
    public required string LaOrganisationName { get; set; } = string.Empty;

    public required List<string> LocalAuthorities { get; set; } = [];

    public List<string> RoleTypes { get; set; }

    public bool ValidationValid { get; private set; } = true;

    public ModifyUserPermissionsModel(IIdamService idamService, IServiceDirectoryClient serviceDirectoryClient)
    {
        _idamService = idamService;
        _serviceDirectoryClient = serviceDirectoryClient;

        List<string> roleTypes =
        [
            SharedKernel.Identity.RoleTypes.DfeAdmin,
            SharedKernel.Identity.RoleTypes.LaManager,
            SharedKernel.Identity.RoleTypes.VcsManager,
            SharedKernel.Identity.RoleTypes.LaProfessional,
            SharedKernel.Identity.RoleTypes.VcsProfessional,
            SharedKernel.Identity.RoleTypes.VcsDualRole,
            SharedKernel.Identity.RoleTypes.LaDualRole
        ];

        RoleTypes = roleTypes;
    }

    public async Task OnGet(long id)
    {
        AccountId = id;
        var organisations = await _serviceDirectoryClient.GetOrganisations();
        LocalAuthorities = organisations.Select(x => x.Name).ToList();

        Account? account = await _idamService.GetAccountById(id);
        if (account != null) 
        { 
            Name = account.Name;

            var accountClaim = account.Claims.FirstOrDefault(x => x.Name == "OrganisationId");
            if (accountClaim != null && accountClaim.Value != "-1" && int.TryParse(accountClaim.Value, out int value))
            {
                
                var organisation = organisations.Find(x => x.Id == value);
                if (organisation != null)
                {
                    LaOrganisationName = organisation.Name;
                }
            }

            accountClaim = account.Claims.FirstOrDefault(x => x.Name == "role");
            if (accountClaim != null)
            {
                SelectedRole = accountClaim.Value;
            }
        }

    }

    public async Task<IActionResult> OnPost()
    {
        if (string.IsNullOrEmpty(SelectedRole))
        {
            ValidationValid = false;
        }

        if (SelectedRole != SharedKernel.Identity.RoleTypes.DfeAdmin && string.IsNullOrEmpty(LaOrganisationName))
        {
            ValidationValid = false;
        }

        if (!ValidationValid)
        {
            return Page();
        }

        long organisationId = -1;
        
        if (!string.IsNullOrEmpty(LaOrganisationName))
        {
            var organisations = await _serviceDirectoryClient.GetOrganisations();
            var organisation = organisations.Find(x => x.Name == LaOrganisationName);
            if (organisation != null) 
            { 
                organisationId = organisation.Id;
            }
        }

        bool result = await _idamService.UpdateRoleAndOrganisation(AccountId, organisationId, SelectedRole);
        if (result) 
        {
            return RedirectToPage($"ModifiedUserClaimsConfirmation", new { accountId = AccountId });
        }

        return Page();
    }
}
