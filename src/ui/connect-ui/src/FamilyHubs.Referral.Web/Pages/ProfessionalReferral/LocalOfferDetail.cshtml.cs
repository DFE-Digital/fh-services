using FamilyHubs.Referral.Core.ApiClients;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Authorization;
using FamilyHubs.Referral.Web.Pages.Shared;
using FamilyHubs.SharedKernel.Identity;

namespace FamilyHubs.Referral.Web.Pages.ProfessionalReferral;

[Authorize(Roles = RoleGroups.LaOrVcsProfessionalOrDualRole)]
public class LocalOfferDetailModel : HeaderPageModel
{
    private readonly IOrganisationClientService _organisationClientService;
    private readonly IIdamsClient _idamsClient;
    public ServiceDto LocalOffer { get; set; } = default!;

    public string? ReturnUrl { get; set; }

    [BindProperty]
    public string ServiceId { get; set; } = default!;

    public bool ShowConnectionRequestButton { get; set; }

    public LocalOfferDetailModel(
        IOrganisationClientService organisationClientService,
        IIdamsClient idamsClient)
    {
        _organisationClientService = organisationClientService;
        _idamsClient = idamsClient;
    }

    public async Task<IActionResult> OnGetAsync(string serviceId)
    {
        ServiceId = serviceId;
        var referer = Request.Headers["Referer"];
        ReturnUrl = StringValues.IsNullOrEmpty(referer) ? Url.Page("Search") : referer.ToString();
        LocalOffer = await _organisationClientService.GetLocalOfferById(serviceId);

        ShowConnectionRequestButton = await ShouldShowConnectionRequestButton();

        return Page();
    }

    private async Task<bool> ShouldShowConnectionRequestButton()
    {
        bool showConnectionRequestButton = HttpContext.GetRole() is
            RoleTypes.LaProfessional or RoleTypes.LaDualRole;
        if (showConnectionRequestButton)
        {
            var vcsProEmails = await _idamsClient
                .GetVcsProfessionalsEmailsAsync(LocalOffer.OrganisationId);
            showConnectionRequestButton = vcsProEmails.Any();
        }

        return showConnectionRequestButton;
    }
}
