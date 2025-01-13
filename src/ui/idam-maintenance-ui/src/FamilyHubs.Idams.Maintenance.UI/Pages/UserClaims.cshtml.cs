using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.Idams.Maintenance.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.Idams.Maintenance.UI.Pages;

public class UserClaimsModel : PageModel
{
    private readonly IIdamService _idamService;

    public UserClaimsModel(IIdamService idamService)
    {
        _idamService = idamService;
    }
    
    public string UserName { get; set; } = string.Empty;

    [BindProperty]
    public long AccountId { get; set; }

    public List<AccountClaim> UserClaims { get; private set; } = [];
    
    public async Task OnGet(long accountId, string username)
    {
        AccountId = accountId;
        UserName = username;
        UserClaims = await _idamService.GetAccountClaimsById(accountId);
    }
}