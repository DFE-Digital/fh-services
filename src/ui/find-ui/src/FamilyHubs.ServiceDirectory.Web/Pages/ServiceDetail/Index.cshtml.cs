using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Web.Pages.ServiceDetail;

public class Index : PageModel
{
    public IActionResult OnGet(long serviceId)
    {
        return Page();
    }
}