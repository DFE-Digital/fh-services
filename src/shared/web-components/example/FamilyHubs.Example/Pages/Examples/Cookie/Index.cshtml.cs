using FamilyHubs.SharedKernel.Razor.Cookies;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.Example.Pages.Examples.Cookie;

public class IndexModel : PageModel
{
    public readonly ICookiePage CookiePage;

    public IndexModel(ICookiePage cookiePage)
    {
        CookiePage = cookiePage;
    }

    public void OnGet()
    {
        CookiePage.OnGet(Request);
    }

    public void OnPost(bool analytics)
    {
        CookiePage.OnPost(analytics, Request, Response);
    }
}
