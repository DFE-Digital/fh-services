using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Errors;
using FamilyHubs.ServiceDirectory.Admin.Web.ViewModel;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Razor.ErrorNext;
using Microsoft.AspNetCore.Mvc;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Areas.AccountAdmin.Pages.ManagePermissions
{
    public class EditRolesModel : InputPageViewModel
    {
        private readonly IIdamClient _idamClient;
        private readonly IEmailService _emailService;

        [BindProperty]
        public bool LaProfessional { get; set; }

        [BindProperty]
        public bool LaManager { get; set; }

        [BindProperty]
        public bool VcsProfessional { get; set; }

        [BindProperty]
        public bool VcsManager { get; set; }
      
        public bool IsLa { get; set; }
        
        public bool IsVcs { get; set; }

        public EditRolesModel(IIdamClient idamClient, IEmailService emailService)
        {
            PageHeading = "What do they need to do?";
            SubmitButtonText = "Confirm";
            _idamClient = idamClient;
            _emailService = emailService;
            Errors = ErrorState.Empty;
        }

        public async Task OnGet(long accountId)
        {
            BackButtonPath = $"/AccountAdmin/ManagePermissions/{accountId}";
            
            var account = await GetAccount(accountId);
            string role = GetRole(account);
            SetOrganisationType(role);
            SetRoleSelection(role);
        }


        public async Task<IActionResult> OnPost(long accountId)
        {
            var account = await GetAccount(accountId);

            if (ModelState.IsValid && (LaManager || LaProfessional || VcsManager || VcsProfessional))
            {
                var oldRole = GetRole(account);
                var newRole = GetSelectedRole();

                if (oldRole == newRole)
                {
                    // No updates required
                    return RedirectToPage("EditRolesChangedConfirmation", new { AccountId = accountId });
                }

                SetOrganisationType(newRole);

                var request = new UpdateClaimDto { AccountId = accountId, Name = "role", Value = newRole };
                await _idamClient.UpdateClaim(request);

                var email = new PermissionChangeNotificationModel
                {
                    EmailAddress = account!.Email, 
                    OldRole = oldRole,
                    NewRole = newRole
                };
                if (IsLa)
                {
                    await _emailService.SendLaPermissionChangeEmail(email);
                }
                else if (IsVcs)
                {
                    await _emailService.SendVcsPermissionChangeEmail(email);
                }
                
                return RedirectToPage("EditRolesChangedConfirmation", new { AccountId = accountId });
            }

            Errors = ErrorState.Create(PossibleErrors.All, ErrorId.ManagePermissions_EditRole_MissingSelection);
                        
            string role = GetRole(account);
            SetOrganisationType(role);

            return Page();
        }

        private string GetSelectedRole()
        {
            var role = string.Empty;
            //La
            if (LaManager && LaProfessional)
            {
                role = RoleTypes.LaDualRole;
            }
            else if (LaManager)
            {
                role = RoleTypes.LaManager;
            }
            else if (LaProfessional)
            {
                role = RoleTypes.LaProfessional;
            }
            //Vcs
            else if (VcsManager && VcsProfessional)
            {
                role = RoleTypes.VcsDualRole;
            }
            else if (VcsManager)
            {
                role = RoleTypes.VcsManager;
            }
            else if (VcsProfessional)
            {
                role = RoleTypes.VcsProfessional;
            }

            return role;
        }

        private void SetOrganisationType(string role)
        {
            switch (role)
            {
                case RoleTypes.LaManager or RoleTypes.LaProfessional or RoleTypes.LaDualRole:
                    IsLa = true;
                    break;
                case RoleTypes.VcsManager or RoleTypes.VcsProfessional or RoleTypes.VcsDualRole:
                    IsVcs = true;
                    break;
            }
        }

        private void SetRoleSelection(string role)
        {
            //La
            if (role == RoleTypes.LaManager)
            {
                LaManager = true;
            }
            else if (role == RoleTypes.LaProfessional)
            {
                LaProfessional = true;
            }
            else if (role == RoleTypes.LaDualRole)
            {
                LaManager = true;
                LaProfessional = true;
            }
            //Vcs
            else if (role == RoleTypes.VcsManager)
            {
                VcsManager = true;
            }
            else if (role == RoleTypes.VcsProfessional)
            {
                VcsProfessional = true;
            }
            else if (role == RoleTypes.VcsDualRole)
            {
                VcsManager = true;
                VcsProfessional = true;
            }
        }
    
        private async Task<AccountDto?> GetAccount(long id)
        {
            var account = await _idamClient.GetAccountById(id);

            if (account is not null)
            {
                return account;
            }

            throw new ArgumentException("User not found");
        }

        private string GetRole(AccountDto? account)
        {
            if (account is not null)
            {
                var roleClaim = account.Claims.Single(x => x.Name == FamilyHubsClaimTypes.Role);
                var role = roleClaim.Value;
                return role;
            }

            throw new ArgumentException("Role not found");
        }
    }
}
