using System.Net;
using FamilyHubs.Referral.Core.ApiClients;
using FamilyHubs.Referral.Web.Pages.Shared;
using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Razor.FamilyHubsUi.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FamilyHubs.Referral.Web.Pages.Referrals.La;

[Authorize(Roles = $"{RoleGroups.LaProfessionalOrDualRole}")]
public class RequestDetailsModel : HeaderPageModel
{
    private readonly IReferralDashboardClientService _referralClientService;
    public ReferralDto Referral { get; set; } = default!;
    private readonly string _serviceUrl;

    public RequestDetailsModel(
        IReferralDashboardClientService referralClientService,
        IOptions<FamilyHubsUiOptions> familyHubsUiOptions) : base(false, true)
    {
        _referralClientService = referralClientService;
        _serviceUrl = "/ProfessionalReferral/LocalOfferDetail?serviceid=";
    }

    public async Task<IActionResult> OnGet(int id)
    {
        try
        {
            Referral = await _referralClientService.GetReferralById(id);
        }
        catch (ReferralClientServiceException ex)
        {
            // user has changed the id in the url to see a referral they shouldn't have access to
            if (ex.StatusCode == HttpStatusCode.Forbidden)
            {
                return Redirect("/Error/403");
            }
            throw;
        }
        return Page();
    }

    public string GetReferralServiceUrl(long serviceId)
    {
        return $"{_serviceUrl}{serviceId}";
    }
}