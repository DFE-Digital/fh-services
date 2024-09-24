using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Pages.manage_services;

[Authorize(Roles = RoleGroups.LaManagerOrDualRole)]
public class DeleteServiceErrorModel : PageModel
{
    private readonly IServiceDirectoryClient _serviceDirectoryClient;
    
    public string ServiceName { get; set; } = string.Empty;

    [BindProperty] public long ServiceId { get; set; }
    
    public DeleteServiceErrorModel(IServiceDirectoryClient serviceDirectoryClient)
    {
        _serviceDirectoryClient = serviceDirectoryClient;
    }

    public async Task OnGetAsync(long serviceId)
    {
        ServiceId = serviceId;
        var service = await _serviceDirectoryClient.GetServiceById(ServiceId);
        ServiceName = service.Name;
    }
}