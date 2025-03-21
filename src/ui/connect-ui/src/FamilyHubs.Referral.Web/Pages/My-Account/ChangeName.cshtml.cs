using FamilyHubs.Referral.Core.ApiClients;
using FamilyHubs.Referral.Core.Models;
using FamilyHubs.Referral.Web.Errors;
using FamilyHubs.Referral.Web.Pages.Shared;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Razor.ErrorNext;
using FamilyHubs.SharedKernel.Razor.Header;
using Microsoft.AspNetCore.Mvc;

namespace FamilyHubs.Referral.Web.Pages.My_Account;

public class ChangeNameModel : HeaderPageModel, IHasErrorStatePageModel
{
    private readonly IIdamsClient _idamsClient;

    public IErrorState Errors { get; private set; }

    [BindProperty]
    public string? FullName { get; set; }

    public ChangeNameModel(IIdamsClient idamsClient)
    {
        _idamsClient = idamsClient;

        Errors = ErrorState.Empty;
    }

    public void OnGet()
    {
        FullName = HttpContext.GetFamilyHubsUser().FullName;
    }

    //todo: PRG?
    public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
    {
        if (ModelState.IsValid && !string.IsNullOrWhiteSpace(FullName) && FullName.Length <= 255)
        {
            var familyHubsUser = HttpContext.GetFamilyHubsUser();

            //todo: common client with dto's in package
            var request = new UpdateAccountSelfServiceDto
            {
                AccountId = long.Parse(familyHubsUser.AccountId),
                Name = FullName
            };
            await _idamsClient.UpdateAccountSelfService(request, cancellationToken);

            HttpContext.RefreshClaims();

            return RedirectToPage("ChangeNameConfirmation");
        }

        Errors = ErrorState.Create(PossibleErrors.All, ErrorId.ChangeName_EnterAName);

        return Page();
    }
}