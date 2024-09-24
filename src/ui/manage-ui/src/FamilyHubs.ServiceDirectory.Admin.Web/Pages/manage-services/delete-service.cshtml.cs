using System.Collections.Immutable;
using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.SharedKernel.Razor.ErrorNext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Pages.manage_services;

public class DeleteService : PageModel
{
    private readonly IServiceDirectoryClient _serviceDirectoryClient;
    private readonly IReferralService _referralServiceClient;

    public string BackUrl => "/manage-services/Service-Detail?flow=edit";

    [BindProperty] public long ServiceId { get; set; }
    public string ServiceName { get; set; } = null!;

    [BindProperty] public bool Yes { get; set; }
    [BindProperty] public bool No { get; set; }

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

    private bool NeitherRadioButtonIsSelected() => !Yes && !No;

    private ImmutableDictionary<int, PossibleError> GetError()
    {
        string errorMessage = $"Select if you want to delete {ServiceName}";
        return ImmutableDictionary.Create<int, PossibleError>()
            .Add(ErrorId.Delete_Service__NeitherRadioButtonIsSelected, errorMessage);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (NeitherRadioButtonIsSelected())
        {
            ServiceName = await GetServiceName();
            Error = ErrorState.Create(GetError(), ErrorId.Delete_Service__NeitherRadioButtonIsSelected);
            return Page();
        }

        if (No)
        {
            // TODO: Implement "No" Shutter Page
            throw new NotImplementedException("\"No\" Shutter Page Required");
        }

        if (await IsOpenConnectionRequests())
        {
            // TODO: Implement "There are open Connection Requests" Error Page
            throw new NotImplementedException("\"There are open Connection Requests\" Error Page Required");
        }

        // TODO: Mark Service as Defunct
        // TODO: Implement "Yes" Shutter Page
        throw new NotImplementedException("\"Yes\" Shutter Page Required");
    }

    public async Task<IActionResult> OnGetAsync(long serviceId)
    {
        ServiceId = serviceId;

        if (await IsOpenConnectionRequests())
        {
            // TODO: Implement "There are open Connection Requests" Error Page
            throw new NotImplementedException("\"There are open Connection Requests\" Error Page Required");
        }

        ServiceName = await GetServiceName();

        return Page();
    }
}