using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Mvc;
using FamilyHubs.Idams.Maintenance.UI.Pages.Shared;

namespace FamilyHubs.Idams.Maintenance.UI.Pages;

public class IndexModel : HeaderPageModel
{
    public IActionResult OnGet()
    {
        if (HttpContext.IsUserLoggedIn())
        {
            return RedirectToPage("/Welcome");
        }

        return Page();
    }
}