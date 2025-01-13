using FamilyHubs.Idams.Maintenance.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FamilyHubs.Idams.Maintenance.UI.Pages;

public class DeleteUserModel : PageModel
{
    private readonly IIdamService _idamService;
    public string? UserName { get; set; }

    [BindProperty]
    public long AccountId { get; set; }

    [BindProperty]
    [Required]
    public bool? DeleteUser { get; set; }

    public string Error { get; set; } = string.Empty;

    public DeleteUserModel(IIdamService idamService)
    {
        _idamService = idamService;
    }
    
    public async Task OnGet(long accountId)
    {
        AccountId = accountId;
        var account = await _idamService.GetAccountById(accountId);
        if (account != null) 
        {
            UserName = account.Name;
        }
    }

    public async Task<IActionResult> OnPost(long accountId)
    {
        if (accountId >= 0 && DeleteUser != null && DeleteUser.Value) 
        { 
            bool result = await _idamService.DeleteUser(accountId);

            if (!result)
            {
                
                var account = await _idamService.GetAccountById(accountId);
                if (account != null)
                {
                    UserName = account.Name;
                }
                Error = $"Failed to delete {UserName}";
                return Page();
            }
        }

        return RedirectToPage("Users");

    }
}
