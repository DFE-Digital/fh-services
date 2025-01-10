
using System.Security.Claims;
using FamilyHubs.RequestForSupport.Core.ApiClients;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Razor.FamilyHubsUi.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using NSubstitute;
using IReferralClientService = FamilyHubs.RequestForSupport.Core.ApiClients.IReferralClientService;

namespace FamilyHubs.RequestForSupport.UnitTests.Dashboards;

public class BaseDashboard<T> where T : PageModel
{
    protected readonly IOrganisationClientService OrganisationClientService;
    protected readonly IReferralClientService ReferralClientService;
    private IOptions<FamilyHubsUiOptions> FamilyHubsUiOptions { get; set; }
    protected T? PageModel { get; set; }

    protected BaseDashboard()
    {
        ReferralClientService = Substitute.For<IReferralClientService>();
        OrganisationClientService = Substitute.For<IOrganisationClientService>();
        
        FamilyHubsUiOptions = Substitute.For<IOptions<FamilyHubsUiOptions>>();
        
        
        var claims = new List<Claim>
        {
            new(FamilyHubsClaimTypes.OrganisationId, "1"),
        };

        var identity = new ClaimsIdentity(claims);

        var principle = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext
        {
            User = principle
        };

        //need these as well for the page context
        var modelState = new ModelStateDictionary();
        var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
        var modelMetadataProvider = new EmptyModelMetadataProvider();
        var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);

        // need page context for the page model
        var basePageContext = new PageContext(actionContext)
        {
            ViewData = viewData
        };
        
        var familyHubsUiOptions = new FamilyHubsUiOptions();
        familyHubsUiOptions.Urls.Add("ThisWeb", new Uri("http://example.com").ToString());
        FamilyHubsUiOptions.Value
            .Returns(familyHubsUiOptions);
        
        PageModel = Activator.CreateInstance(typeof(T), ReferralClientService, FamilyHubsUiOptions, OrganisationClientService) as T;
        PageModel!.PageContext = basePageContext;
    }
}