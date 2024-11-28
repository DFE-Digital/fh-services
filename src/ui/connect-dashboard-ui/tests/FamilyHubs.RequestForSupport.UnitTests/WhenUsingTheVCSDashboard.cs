using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.ReferralService.Shared.Enums;
using FamilyHubs.ReferralService.Shared.Models;
using FamilyHubs.RequestForSupport.Web.Pages.Vcs;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using FamilyHubs.Referral.Core.ApiClients;
using FamilyHubs.RequestForSupport.UnitTests.Helpers;
using FamilyHubs.RequestForSupport.Web.VcsDashboard;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Razor.Dashboard;
using FamilyHubs.SharedKernel.Razor.FamilyHubsUi.Options;
using Microsoft.Extensions.Options;
using NSubstitute;
using IReferralClientService = FamilyHubs.RequestForSupport.Core.ApiClients.IReferralClientService;
using OrganisationDto = FamilyHubs.ReferralService.Shared.Dto.OrganisationDto;

namespace FamilyHubs.RequestForSupport.UnitTests;

public class WhenUsingTheVcsDashboard
{
    private readonly DashboardModel _pageModel;
    private readonly IOrganisationClientService _organisationClientService;
    private readonly IReferralClientService _referralClientService;

    public WhenUsingTheVcsDashboard()
    {
        _referralClientService = Substitute.For<IReferralClientService>();
        var mockOptionsFamilyHubsUiOptions = Substitute.For<IOptions<FamilyHubsUiOptions>>();
        _organisationClientService = Substitute.For<IOrganisationClientService>();
        var familyHubsUiOptions = new FamilyHubsUiOptions();

        var claims = new List<Claim>
        {
            new(FamilyHubsClaimTypes.OrganisationId, "1"),
            new(FamilyHubsClaimTypes.FullName, "My full name"),
            new(FamilyHubsClaimTypes.Role, RoleTypes.LaDualRole)
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
        var pageContext = new PageContext(actionContext)
        {
            ViewData = viewData
        };

        familyHubsUiOptions.Urls.Add("ThisWeb", new Uri("http://example.com").ToString());

        mockOptionsFamilyHubsUiOptions.Value
            .Returns(familyHubsUiOptions);

        _pageModel = new DashboardModel(_referralClientService, mockOptionsFamilyHubsUiOptions,
            _organisationClientService)
        {
            PageContext = pageContext
        };
        SetupReferralClientService();
    }

    [Fact]
    public async Task ThenOnGetOneRowIsPrepared()
    {
        // Arrange
        
        // Act
        await _pageModel.OnGet("ContactInFamily", SortOrder.ascending);

        // Assert
        IDashboard<ReferralDto> dashboard = _pageModel;
        dashboard.Rows.Should().ContainSingle();
    }

    [Fact]
    public async Task ShouldReturnCorrectTextualProperties()
    {
        // Arrange
        _organisationClientService.GetOrganisationDtoByIdAsync(1).Returns(
            new ServiceDirectory.Shared.Dto.OrganisationDto
            {
                Id = 1,
                OrganisationType = OrganisationType.NotSet,
                Name = "VCS org name",
                Description = "some descript",
                AdminAreaCode = "abc",
            });

        // Act
        await _pageModel.OnGet(null, SortOrder.none);

        // Assert
        _pageModel.Title.Should().Be("My requests");
        _pageModel.CaptionText.Should().Be("VCS org name");
        _pageModel.SubTitle.Should().Be("Connection requests received");
    }

   private void SetupReferralClientService()
    {
        List<ReferralDto> list = [TestHelpers.GetMockReferralDto()];
        var pageList = new PaginatedList<ReferralDto>(list, 1, 1, 1);
        _referralClientService.GetRequestsForConnectionByOrganisationId(Arg.Any<string>(),
            Arg.Any<ReferralOrderBy>(),
            Arg.Any<bool?>(),
            Arg.Any<int>(),
            Arg.Any<int>(),
            Arg.Any<CancellationToken>()).Returns(pageList);
    }
}