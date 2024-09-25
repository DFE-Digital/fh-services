using FamilyHubs.ServiceDirectory.Admin.Web.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Pages.manage_services;

public class DeleteServiceShutter : PageModel
{
    public string ServiceName { get; set; } = null!;
    public string Qualifier { get; set; } = null!;

    public IActionResult OnGet(string serviceName, bool isDeleted)
    {
        ServiceName = serviceName;
        Qualifier = isDeleted ? "" : "not";
        return Page();
    }
}