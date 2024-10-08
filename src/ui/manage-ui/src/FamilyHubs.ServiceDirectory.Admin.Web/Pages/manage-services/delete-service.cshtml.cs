using System.Collections.Immutable;
using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Razor.ErrorNext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Pages.manage_services;

[Authorize(Roles = RoleGroups.AdminRole)]
public class DeleteService : PageModel
{
    private readonly IServiceDirectoryClient _serviceDirectoryClient;
    private readonly IReferralService _referralServiceClient;

    private const string OpenConnectionErrorUrl = "/manage-services/delete-service-error";
    private const string ShutterPageUrl = "/manage-services/delete-service-shutter";

    public string BackUrl => "/manage-services/Service-Detail?flow=edit";
    [BindProperty] public long ServiceId { get; set; }
    public string ServiceName { get; set; } = null!;
    public bool UserRoleCanSeeConnectionRequestWarning =>
        RoleGroups.VcsManagerOrDualRole.Contains(HttpContext.GetFamilyHubsUser().Role);

    [BindProperty] public bool? Selected { get; set; }

    public IErrorState Error { get; set; } = ErrorState.Empty;

    public DeleteService(IServiceDirectoryClient serviceDirectoryClient, IReferralService referralServiceClient)
    {
        _serviceDirectoryClient = serviceDirectoryClient;
        _referralServiceClient = referralServiceClient;
    }

    private async Task<bool> IsOpenConnectionRequests() =>
        await _referralServiceClient.GetReferralsCountByServiceId(ServiceId) > 0;

    private async Task<string> GetServiceName() =>
        (await _serviceDirectoryClient.GetServiceById(ServiceId)).Name;

    private async Task MarkServiceAsDefunct() => await _serviceDirectoryClient.DeleteService(ServiceId);

    private bool NeitherRadioButtonIsSelected() => Selected is null;

    private ImmutableDictionary<int, PossibleError> GetError()
    {
        string errorMessage = $"Select if you want to delete {ServiceName}";
        return ImmutableDictionary.Create<int, PossibleError>()
            .Add(ErrorId.Delete_Service__NeitherRadioButtonIsSelected, errorMessage);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ServiceName = await GetServiceName();

        if (NeitherRadioButtonIsSelected())
        {
            Error = ErrorState.Create(GetError(), ErrorId.Delete_Service__NeitherRadioButtonIsSelected);
            return Page();
        }

        if (Selected is false)
        {
            return RedirectToPage(ShutterPageUrl, new { serviceName = ServiceName, isDeleted = false });
        }

        if (await IsOpenConnectionRequests())
        {
            return RedirectToPage(OpenConnectionErrorUrl, new { serviceId = ServiceId });
        }

        await MarkServiceAsDefunct();

        return RedirectToPage(ShutterPageUrl, new { serviceName = ServiceName, isDeleted = true });
    }

    public async Task<IActionResult> OnGetAsync(long serviceId)
    {
        ServiceId = serviceId;
        ServiceName = await GetServiceName();

        return Page();
    }
}