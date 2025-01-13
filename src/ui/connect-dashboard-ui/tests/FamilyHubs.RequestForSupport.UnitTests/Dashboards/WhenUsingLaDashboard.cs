using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.ReferralService.Shared.Enums;
using FamilyHubs.ReferralService.Shared.Models;
using FamilyHubs.RequestForSupport.Web.Pages.La;
using FamilyHubs.SharedKernel.Razor.Dashboard;
using FluentAssertions;
using NSubstitute;
using static FamilyHubs.RequestForSupport.UnitTests.Helpers.TestHelpers;

namespace FamilyHubs.RequestForSupport.UnitTests.Dashboards;

public class WhenUsingLaDashboard : BaseDashboard<DashboardModel>
{

    public WhenUsingLaDashboard()
    {
        SetupReferralClientService();
    }
    
    [Fact]
    public async Task OnGet_ShouldReturnARowOfData()
    {
        // Arrange & Act
        await PageModel!.OnGet(null, SortOrder.none, 1);

        // Assert
        IDashboard<ReferralDto> dashboard = PageModel;
        dashboard.Rows.Should().ContainSingle();
    }
    
    [Fact]
    public async Task ShouldReturnCorrectTextualProperties()
    {
        // Arrange
        OrganisationClientService.GetOrganisationDtoByIdAsync(1).Returns(
            new OrganisationDto
            {
                Id = 1,
                Name = "VCS org name",
                Description = "some descript",
            });

        // Act
        await PageModel!.OnGet(null, SortOrder.none);

        // Assert
        PageModel.Title.Should().Be("My requests");
        PageModel.CaptionText.Should().Be("VCS org name");
        PageModel.SubTitle.Should().Be("Connection requests sent to services");
    }
    
    private void SetupReferralClientService()
    {
        List<ReferralDto> list = [GetMockReferralDto()];
        var pageList = new PaginatedList<ReferralDto>(list, 1, 1, 1);
        ReferralClientService.GetRequestsByLaProfessional(Arg.Any<string>(),
            Arg.Any<ReferralOrderBy>(),
            Arg.Any<bool?>(),
            Arg.Any<int>(),
            Arg.Any<int>(),
            Arg.Any<CancellationToken>()).Returns(pageList);
    }
}