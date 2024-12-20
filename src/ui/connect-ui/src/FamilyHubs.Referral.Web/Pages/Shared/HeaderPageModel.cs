using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Razor.FamilyHubsUi.Options;
using FamilyHubs.SharedKernel.Razor.Header;
using FamilyHubs.SharedKernel.Razor.Links;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.Referral.Web.Pages.Shared;

public class HeaderPageModel : PageModel, IFamilyHubsHeader
{
    private readonly string? _selectedLinkText;

    public HeaderPageModel(bool highlightSearchForService = true, bool highlightRequests = false)
    {
        _selectedLinkText = highlightSearchForService switch
        {
            true => "Search for service",
            false when (highlightRequests) => "My requests",
            _ => null
        };
    }

    public bool ShowActionLinks => IsAuthenticatedAndTermsAccepted;
    public bool ShowNavigationMenu => IsAuthenticatedAndTermsAccepted;

    private bool IsAuthenticatedAndTermsAccepted =>
        User.Identity?.IsAuthenticated == true
        && HttpContext.TermsAndConditionsAccepted();

    LinkStatus IFamilyHubsHeader.GetStatus(IFhRenderLink link) =>
        link.Text == _selectedLinkText ? LinkStatus.Active : LinkStatus.Visible;

    IEnumerable<IFhRenderLink> IFamilyHubsHeader.NavigationLinks(
        FhLinkOptions[] navigationLinks,
        IFamilyHubsUiOptions familyHubsUiOptions)
    {
        string role = HttpContext.GetRole();

        return role is RoleTypes.VcsProfessional or RoleTypes.VcsDualRole
            ? familyHubsUiOptions.GetAlternative("VcsHeader").Header.NavigationLinks
            : navigationLinks;
    }
}