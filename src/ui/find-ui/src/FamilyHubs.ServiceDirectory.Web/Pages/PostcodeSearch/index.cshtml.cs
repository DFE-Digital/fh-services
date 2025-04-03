using FamilyHubs.ServiceDirectory.Web.Errors;
using FamilyHubs.SharedKernel.Razor.ErrorNext;
using FamilyHubs.SharedKernel.Razor.Header;
using FamilyHubs.SharedKernel.Services.Postcode.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Web.Pages.PostcodeSearch;

public class PostcodeSearchModel : PageModel, IHasErrorStatePageModel
{
    public IErrorState Errors { get; private set;  } = ErrorState.Empty;

    public void OnGet(PostcodeError postcodeError)
    {
        if (postcodeError != PostcodeError.None)
        {
            Errors = ErrorState.Create(PossibleErrors.All, postcodeError);
        }
    }
}