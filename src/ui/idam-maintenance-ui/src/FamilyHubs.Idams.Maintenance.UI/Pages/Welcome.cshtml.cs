using FamilyHubs.Idams.Maintenance.UI.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FamilyHubs.Idams.Maintenance.UI.Pages;

public class WelcomeModel : HeaderPageModel
{
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
}
