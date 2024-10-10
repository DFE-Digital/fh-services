using FamilyHubs.Referral.Core.DistributedCache;
using FamilyHubs.Referral.Core.Models;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Routing;
using NSubstitute;

namespace FamilyHubs.ReferralUi.UnitTests.Web.Pages.ProfessionalReferral;

public class BaseProfessionalReferralPage
{
    protected readonly IConnectionRequestDistributedCache ReferralDistributedCache;
    protected readonly ConnectionRequestModel ConnectionRequestModel;

    private const long ServiceId = 123;
    protected const string Reason = "Reason";
    protected const string EmailAddress = "example.com";
    protected const string Telephone = "07700 900000";
    protected const string Text = "07700 900001";
    protected const string AddressLine1 = "AddressLine1";
    protected const string AddressLine2 = "AddressLine2";
    protected const string TownOrCity = "TownOrCity";
    protected const string County = "County";
    protected const string Postcode = "Postcode";
    protected const string EngageReason = "EngageReason";

    protected const string ProfessionalEmail = "Joe.Professional@email.com";

    protected BaseProfessionalReferralPage()
    {
        ConnectionRequestModel = new ConnectionRequestModel
        {
            ServiceId = ServiceId.ToString(),
            FamilyContactFullName = "FamilyContactFullName",
            Reason = Reason,
            ContactMethodsSelected = [true, true, true, true],
            EmailAddress = EmailAddress,
            TelephoneNumber = Telephone,
            TextphoneNumber = Text,
            AddressLine1 = AddressLine1,
            AddressLine2 = AddressLine2,
            TownOrCity = TownOrCity,
            County = County,
            Postcode = Postcode,
            EngageReason = EngageReason
        };

        ReferralDistributedCache = Substitute.For<IConnectionRequestDistributedCache>();
        //todo: add pro's email to class and check key, rather than It.IsAny<string>()
        ReferralDistributedCache.SetAsync(Arg.Any<string>(), Arg.Any<ConnectionRequestModel>()).Returns(Task.CompletedTask);
        ReferralDistributedCache.GetAsync(Arg.Any<string>()).Returns(Task.FromResult<ConnectionRequestModel?>(ConnectionRequestModel));
        ReferralDistributedCache.RemoveAsync(Arg.Any<string>()).Returns(Task.CompletedTask);
    }

    protected PageContext GetPageContextWithUserClaims()
    {
        var displayName = "User name";
        var identity = new GenericIdentity(displayName);
        identity.AddClaim(new Claim(FamilyHubsClaimTypes.Role, "Professional"));
        identity.AddClaim(new Claim(FamilyHubsClaimTypes.OrganisationId, "1"));
        identity.AddClaim(new Claim(FamilyHubsClaimTypes.AccountId, "1"));
        identity.AddClaim(new Claim(FamilyHubsClaimTypes.AccountStatus, "active"));
        identity.AddClaim(new Claim(FamilyHubsClaimTypes.FullName, "Test User"));
        identity.AddClaim(new Claim(FamilyHubsClaimTypes.ClaimsValidTillTime, DateTime.UtcNow.AddMinutes(30).ToString()));
        identity.AddClaim(new Claim(ClaimTypes.Email, ProfessionalEmail));
        identity.AddClaim(new Claim(FamilyHubsClaimTypes.PhoneNumber, "012345678"));
        var principle = new ClaimsPrincipal(identity);
        // use default context with user
        var httpContext = new DefaultHttpContext()
        {
            User = principle
        };

        //need these as well for the page context
        var modelState = new ModelStateDictionary();
        var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
        var modelMetadataProvider = new EmptyModelMetadataProvider();
        var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
        // need page context for the page model
        return new PageContext(actionContext)
        {
            ViewData = viewData
        };
    }
}