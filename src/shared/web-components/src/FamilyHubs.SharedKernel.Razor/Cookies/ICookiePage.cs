using Microsoft.AspNetCore.Http;

namespace FamilyHubs.SharedKernel.Razor.Cookies;

public interface ICookiePage
{
    string CookiePolicyContent { get; }
    bool ShowSuccessBanner { get; }
    ConsentCookie? Cookie { get; }

    void OnGet(HttpRequest request);
    void OnPost(bool analytics, HttpRequest request, HttpResponse response);

    public record ConsentCookie
    {
        public bool analytics { get; init; }
        public int version { get; init; }
    }
}
