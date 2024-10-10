using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.ReferralService.Shared.Enums;
using FamilyHubs.ReferralService.Shared.Models;
using FamilyHubs.RequestForSupport.Core.ApiClients;
using FamilyHubs.RequestForSupport.Web.Pages.Vcs;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using System.Security.Principal;
using FamilyHubs.RequestForSupport.UnitTests.Helpers;
using FamilyHubs.SharedKernel.Razor.Dashboard;
using FamilyHubs.SharedKernel.Razor.FamilyHubsUi.Options;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace FamilyHubs.RequestForSupport.UnitTests;

public class WhenUsingTheVcsDashboard
{
    private readonly DashboardModel _pageModel;

    public WhenUsingTheVcsDashboard()
    {
        var mockReferralClientService = Substitute.For<IReferralClientService>();
        var mockOptionsFamilyHubsUiOptions = Substitute.For<IOptions<FamilyHubsUiOptions>>();
        var familyHubsUiOptions = new FamilyHubsUiOptions();

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
        var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
        var modelMetadataProvider = new EmptyModelMetadataProvider();
        var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);

        // need page context for the page model
        var pageContext = new PageContext(actionContext)
        {
            ViewData = viewData
        };

        familyHubsUiOptions.Urls.Add("ThisWeb", new Uri("http://example.com").ToString());

        mockOptionsFamilyHubsUiOptions.Value
            .Returns(familyHubsUiOptions);

        _pageModel = new DashboardModel(mockReferralClientService, mockOptionsFamilyHubsUiOptions)
        {
            PageContext = pageContext
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
