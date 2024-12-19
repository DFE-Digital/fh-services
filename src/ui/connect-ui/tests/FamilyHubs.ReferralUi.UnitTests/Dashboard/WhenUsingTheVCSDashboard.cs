using System.Security.Claims;
using System.Security.Principal;
using FamilyHubs.Referral.Core.ApiClients;
using FamilyHubs.Referral.Web.Pages.Referrals.Vcs;
using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.ReferralService.Shared.Enums;
using FamilyHubs.ReferralService.Shared.Models;
using FamilyHubs.ReferralUi.UnitTests.Helpers;
using FamilyHubs.SharedKernel.Razor.Dashboard;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Tree;
using NSubstitute;

namespace FamilyHubs.ReferralUi.UnitTests.Dashboard;

public class WhenUsingTheVcsDashboard
{
    private readonly DashboardModel _pageModel;

    public WhenUsingTheVcsDashboard()
    {
        var mockReferralClientService = Substitute.For<IReferralDashboardClientService>();

        List<ReferralDto> list = [TestHelpers.GetMockReferralDto()];
        var pageList = new PaginatedList<ReferralDto>(list, 1, 1, 1);
        mockReferralClientService.GetRequestsForConnectionByOrganisationId(Arg.Any<string>(), 
            Arg.Any<ReferralOrderBy>(),
            Arg.Any<bool?>(),
            Arg.Any<int>(),
            Arg.Any<int>(),
            Arg.Any<CancellationToken>()).Returns(pageList);

        var identity = new GenericIdentity("");
        
        var principle = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext
        {
            User = principle
        };

        //need these as well for the page context
        var modelState = new ModelStateDictionary();
        var actionContext = new ActionContext(
            httpContext, new RouteData(new RouteValueDictionary {{ "page", "/Referrals/Vcs/Dashboard" }}), new PageActionDescriptor(), modelState
        );
        var modelMetadataProvider = new EmptyModelMetadataProvider();
        var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
        var urlHelper = Substitute.ForPartsOf<UrlHelper>(actionContext);
        urlHelper.RouteUrl(Arg.Any<UrlRouteContext>()).Returns("RouteURL");

        // need page context for the page model
        var pageContext = new PageContext(actionContext)
        {
            ViewData = viewData
        };

        _pageModel = new DashboardModel(mockReferralClientService)
        {
            PageContext = pageContext,
            Url = urlHelper
        };
    }
    
    [Fact]
    public async Task ThenOnGetOneRowIsPrepared()
    {
        //Act & Arrange
        await _pageModel.OnGet("ContactInFamily", SortOrder.ascending);

        //Assert
        IDashboard<ReferralDto> dashboard = _pageModel;
        dashboard.Rows.Should().ContainSingle();
    }

    
}
