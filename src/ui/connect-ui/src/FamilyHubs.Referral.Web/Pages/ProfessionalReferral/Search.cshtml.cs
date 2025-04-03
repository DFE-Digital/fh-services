using FamilyHubs.Referral.Core.Models;
using FamilyHubs.Referral.Web.Errors;
using FamilyHubs.Referral.Web.Pages.Shared;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Razor.ErrorNext;
using FamilyHubs.SharedKernel.Razor.Header;
using FamilyHubs.SharedKernel.Services.Postcode.Interfaces;
using FamilyHubs.SharedKernel.Services.Postcode.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace FamilyHubs.Referral.Web.Pages.ProfessionalReferral;

//todo: it would be better to look up and store the postcode once here, rather than each time on the results page

[Authorize(Roles = RoleGroups.LaOrVcsProfessionalOrDualRole)]
public class SearchModel : HeaderPageModel, IHasErrorStatePageModel
{
    private readonly IPostcodeLookup _postcodeLookup;
    public IErrorState Errors { get; private set;  } = ErrorState.Empty;

    [BindProperty]
    public string? Postcode { get; set; }

    public SearchModel(IPostcodeLookup postcodeLookup)
    {
        _postcodeLookup = postcodeLookup;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var (postcodeError, _) = await _postcodeLookup.Get(Postcode);
        if (postcodeError == PostcodeError.None)
        {
            return RedirectToPage("LocalOfferResults", new
            {
                postcode = Postcode,
                currentPage = 1
            });
        }

        var errorId = postcodeError switch
        {
            PostcodeError.NoPostcode => ErrorId.NoPostcode,
            PostcodeError.InvalidPostcode => ErrorId.InvalidPostcode,
            PostcodeError.PostcodeNotFound => ErrorId.PostcodeNotFound,
            _ => ErrorId.PostcodeNotFound
        };
        Errors = ErrorState.Create(PossibleErrors.All, errorId);
        return Page();
    }
}
