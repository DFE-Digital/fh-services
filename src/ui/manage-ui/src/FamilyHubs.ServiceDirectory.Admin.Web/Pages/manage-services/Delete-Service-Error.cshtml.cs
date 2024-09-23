using FamilyHubs.ServiceDirectory.Admin.Core.DistributedCache;
using FamilyHubs.ServiceDirectory.Admin.Core.Models.ServiceJourney;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Pages.manage_services;

[Authorize(Roles = RoleGroups.AdminRole)]
public class DeleteServiceErrorModel : PageModel
{
    private readonly IRequestDistributedCache _connectionRequestCache;
    public ServiceModel? ServiceModel { get; set; }

    public DeleteServiceErrorModel(IRequestDistributedCache connectionRequestCache)
    {
        _connectionRequestCache = connectionRequestCache;
    }

    public async Task OnGetAsync()
    {
        var user = HttpContext.GetFamilyHubsUser();

        ServiceModel = await _connectionRequestCache.GetAsync<ServiceModel>(user.Email);
    }
}