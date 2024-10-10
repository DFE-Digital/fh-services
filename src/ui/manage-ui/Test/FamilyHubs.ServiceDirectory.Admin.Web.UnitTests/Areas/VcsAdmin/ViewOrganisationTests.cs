using AutoFixture;
using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.VcsAdmin.Pages;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.SharedKernel.Identity;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.VcsAdmin;

public class ViewOrganisationTests
{
    private readonly IServiceDirectoryClient _mockServiceDirectoryClient;
    private readonly ICacheService _mockCacheService;
    private readonly ILogger<ViewOrganisationModel> _mockLogger;
    private readonly Fixture _fixture;
        
    private const int OrganisationId = 2;

    public ViewOrganisationTests()
    {
        _mockServiceDirectoryClient = Substitute.For<IServiceDirectoryClient>();
        _mockCacheService = Substitute.For<ICacheService>();
        _mockLogger = Substitute.For<ILogger<ViewOrganisationModel>>();
        _fixture = new Fixture();
        ConfigureMockServiceClient();
    }

    [Fact]
    public async Task OnGet_ReturnsPage()
    {
        //  Arrange
        var mockHttpContext = GetHttpContext(RoleTypes.DfeAdmin, -1);
        var sut = new ViewOrganisationModel(_mockServiceDirectoryClient, _mockCacheService, _mockLogger)
        {
            PageContext = { HttpContext = mockHttpContext },
            OrganisationId = OrganisationId.ToString()
        };

        //  Act
        var response = await sut.OnGet();

        //  Assert
        Assert.IsType<PageResult>(response);
    }

    [Theory]
    [InlineData("5", "Organisation 5 not found")]
    [InlineData("1", "Organisation 1 is not a VCS organisation")]
    [InlineData("3", "Organisation 3 has no parent")]
    [InlineData("4", "User testuser@test.com cannot view 4")]
    public async Task OnGet_InvalidOrganisation_RedirectsToError(string organisationId, string expectedLogMessage)
    {
        //  Arrange
        var mockHttpContext = GetHttpContext(RoleTypes.LaManager, 1);
        var sut = new ViewOrganisationModel(_mockServiceDirectoryClient, _mockCacheService, _mockLogger)
        {
            PageContext = { HttpContext = mockHttpContext },
            OrganisationId = organisationId
        };

        //  Act
        var response = await sut.OnGet();

        //  Assert
        Assert.IsType<RedirectToPageResult>(response);
        _mockLogger.Received(1).Log(
            LogLevel.Warning,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString() == expectedLogMessage && o.GetType().Name == "FormattedLogValues"),
            Arg.Any<Exception>(),
            Arg.Any<Func<object, Exception?, string>>());
    }

    [Fact]
    public async Task OnPost_UpdatesOrganisation()
    {
        //  Arrange
        const string updatedName = "updatedName";
        _mockCacheService.RetrieveString(CacheKeyNames.UpdateOrganisationName).Returns(Task.FromResult(updatedName));
        _mockServiceDirectoryClient.UpdateOrganisation(Arg.Any<OrganisationDetailsDto>()).Returns(OrganisationId);
        var mockHttpContext = GetHttpContext(RoleTypes.DfeAdmin, -1);
        var sut = new ViewOrganisationModel(_mockServiceDirectoryClient, _mockCacheService, _mockLogger)
        {
            PageContext = { HttpContext = mockHttpContext },
            OrganisationId = OrganisationId.ToString()
        };

        //  Act
        await sut.OnPost();

        //  Assert
        var arg = new TestHelper.ArgumentCaptor<OrganisationDetailsDto>();
        await _mockServiceDirectoryClient.Received().UpdateOrganisation(arg.Capture());
        ArgumentNullException.ThrowIfNull(arg.Value);
        arg.Value.Name.Should().Be(updatedName);
    }

    private static HttpContext GetHttpContext(string role, long organisationId)
    {
        var claims = new List<Claim> {
            new(ClaimTypes.Email, "testuser@test.com") ,
            new(FamilyHubsClaimTypes.FullName, "any") ,
            new(FamilyHubsClaimTypes.AccountId, "1"),
            new(FamilyHubsClaimTypes.OrganisationId, organisationId.ToString()),
            new(FamilyHubsClaimTypes.Role, role),
        };

        return TestHelper.GetHttpContext(claims);
    }

    private void ConfigureMockServiceClient()
    {
        var la = TestHelper.CreateTestOrganisationWithServices(1, null, OrganisationType.LA, _fixture);
        var laResponse = Task.FromResult(la);

        var vcs = TestHelper.CreateTestOrganisationWithServices(2, 1, OrganisationType.VCFS, _fixture);
        var vcsResponse = Task.FromResult(vcs);

        var vcsNoParent = TestHelper.CreateTestOrganisationWithServices(3, null, OrganisationType.VCFS, _fixture);
        var vcsNoParentResponse = Task.FromResult(vcsNoParent);

        var vcsUnauthorisedUser = TestHelper.CreateTestOrganisationWithServices(4, 99, OrganisationType.VCFS, _fixture);
        var vcsUnauthorisedUserResponse = Task.FromResult(vcsUnauthorisedUser);

        _mockServiceDirectoryClient.GetOrganisationById(Arg.Is<long>(x => x == 1), Arg.Any<CancellationToken>()).Returns(laResponse);
        _mockServiceDirectoryClient.GetOrganisationById(Arg.Is<long>(x => x == 2), Arg.Any<CancellationToken>()).Returns(vcsResponse);
        _mockServiceDirectoryClient.GetOrganisationById(Arg.Is<long>(x => x == 3), Arg.Any<CancellationToken>()).Returns(vcsNoParentResponse);
        _mockServiceDirectoryClient.GetOrganisationById(Arg.Is<long>(x => x == 4), Arg.Any<CancellationToken>()).Returns(vcsUnauthorisedUserResponse);
    }
}