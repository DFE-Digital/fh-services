using FamilyHubs.SharedKernel.Razor.FamilyHubsUi.Options;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace FamilyHubs.SharedKernel.Razor.Cookies;

public class CookiePage : ICookiePage
{
    public string CookiePolicyContent { get; }
    public ICookiePage.ConsentCookie? Cookie { get; private set; }
    private readonly AnalyticsOptions? _analyticsOptions;

    public CookiePage(IOptions<FamilyHubsUiOptions> familyHubsUiOptions, string cookiePolicyContent = "_CookiePolicy.cshtml")
    {
        CookiePolicyContent = cookiePolicyContent;
        _analyticsOptions = familyHubsUiOptions.Value.Analytics;
    }

    public bool ShowSuccessBanner { get; set; }

    public void OnGet(HttpRequest request)
    {
        Cookie = JsonSerializer.Deserialize<ICookiePage.ConsentCookie>(request.Cookies[_analyticsOptions!.CookieName] ?? "{}");
    }

    public void OnPost(bool analytics, HttpRequest request, HttpResponse response)
    {
        if (_analyticsOptions == null)
            return;

        Cookie = SetConsentCookie(request, response, analytics);
        if (!analytics)
        {
            ResetAnalyticCookies(request, response);
        }

        ShowSuccessBanner = true;
    }

    /// <remarks>
    /// Notes:
    /// * this needs to be compatible with our javascript cookie code, such as cookie-functions.ts
    /// * Response.Cookies has a static EnableCookieNameEncoding - can we use that and switch to Append?
    /// </remarks>
    private ICookiePage.ConsentCookie SetConsentCookie(HttpRequest request, HttpResponse response, bool analyticsAllowed)
    {
        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(365),
            Path = "/",
            SameSite = SameSiteMode.Strict
        };

        if (request.IsHttps)
        {
            cookieOptions.Secure = true;
        }

        var cookie = new ICookiePage.ConsentCookie
        {
            analytics = analyticsAllowed,
            version = _analyticsOptions!.CookieVersion
        };

        response.Cookies.Append(_analyticsOptions!.CookieName, JsonSerializer.Serialize(cookie), cookieOptions);

        return cookie;
    }

    private static void ResetAnalyticCookies(HttpRequest request, HttpResponse response)
    {
        foreach (var uaCookie in request.Cookies.Where(c => c.Key.StartsWith("_ga")))
        {
            DeleteCookies(response, uaCookie.Key);
        }

        DeleteCookies(response, "_gid");

        // MS Clarity
        DeleteCookies(response, "_clck", "_clsk", "CLID", "ANONCHK", "MR", "MUID", "SM");
    }

    /// <summary>
    /// Asks the browser to deletes the supplied cookies.
    /// </summary>
    private static void DeleteCookies(HttpResponse response, params string[] cookies)
    {
        foreach (var cookie in cookies)
        {
            //todo: cookieoptions for domain?
            response.Cookies.Delete(cookie);
        }
    }
}
