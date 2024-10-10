using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.SharedKernel.Identity;
using Xunit;
using FamilyHubs.ServiceDirectory.Shared.Models;
using FamilyHubs.SharedKernel.Razor.Dashboard;
using System.Security.Claims;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Web.Pages.manage_services;
using NSubstitute;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Manage;

public class ServicesModelTests
{
    private readonly IServiceDirectoryClient _serviceDirectoryClientMock = Substitute.For<IServiceDirectoryClient>();

    [Fact]
    public async Task OnGetAsync_WithValidUser_ReturnsExpectedValues()
    {
        // Arrange
        var services = new PaginatedList<ServiceNameDto>([
            new ServiceNameDto { Id = 111, Name = "Name 111" },
            new ServiceNameDto { Id = 222, Name = "Name 222" }

        ], 2, 1, 1);

        _serviceDirectoryClientMock
            .GetServiceSummaries(null, null, 1, 10, SortOrder.ascending, CancellationToken.None)
            .Returns(services);

        var claims = new List<Claim>
        {
            new(FamilyHubsClaimTypes.Role, RoleTypes.DfeAdmin),
            new(FamilyHubsClaimTypes.OrganisationId, "999")
        };
        var mockHttpContext = TestHelper.GetHttpContext(claims);

        var model = new ServicesModel(_serviceDirectoryClientMock)
        {
            PageContext = { HttpContext = mockHttpContext }
        };

        // Act
        await model.OnGetAsync(CancellationToken.None, ServiceTypeArg.La.ToString(), null, SortOrder.ascending, 1);

        // Assert
        Assert.Equal("Services", model.Title);
        Assert.Equal(" for Local Authorities and VCS organisations", model.OrganisationTypeContent);
        Assert.Equal(2, model.Rows.Count());
    }
}