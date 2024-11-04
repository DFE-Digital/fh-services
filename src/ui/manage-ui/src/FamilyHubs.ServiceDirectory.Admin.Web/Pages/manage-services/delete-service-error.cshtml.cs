using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Pages.manage_services;

[Authorize(Roles = $"{RoleTypes.DfeAdmin},{RoleGroups.VcsManagerOrDualRole}")]
public class DeleteServiceErrorModel : PageModel
{
    private readonly IServiceDirectoryClient _serviceDirectoryClient;

    [BindProperty] public long ServiceId { get; private set; }
    public string ServiceName { get; private set; } = null!;
    public string? VcfsOrganisationName { get; private set; }

    public bool IsUserDfEAdmin { get; private set; }

    public DeleteServiceErrorModel(IServiceDirectoryClient serviceDirectoryClient)
    {
        _serviceDirectoryClient = serviceDirectoryClient;
    }

    private async Task<string> GetServiceName() => (await _serviceDirectoryClient.GetServiceById(ServiceId)).Name;

    private async Task<string> GetVcfsOrganisationName()
    {
        ServiceDto serviceDto = await _serviceDirectoryClient.GetServiceById(ServiceId);
        OrganisationDto organisationDto = await _serviceDirectoryClient.GetOrganisationById(serviceDto.OrganisationId);

        return organisationDto.Name;
    }

    public async Task OnGetAsync(long serviceId)
    {
        ServiceId = serviceId;
        ServiceName = await GetServiceName();

        IsUserDfEAdmin = HttpContext.GetFamilyHubsUser().Role.Equals(RoleTypes.DfeAdmin);

        if (IsUserDfEAdmin)
        {
            VcfsOrganisationName = await GetVcfsOrganisationName();
        }
    }
}