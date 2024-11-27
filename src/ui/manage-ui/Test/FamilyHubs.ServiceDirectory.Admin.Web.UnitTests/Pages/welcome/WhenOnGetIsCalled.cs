using System.Security.Claims;
using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Pages;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Pages.welcome;

public class WhenOnGetIsCalled
{
    private readonly ICacheService _cacheService;
    private readonly IServiceDirectoryClient _serviceDirectoryClient;
    private readonly HttpContext _httpContext;
    
    public WhenOnGetIsCalled()
    {
        _cacheService = Substitute.For<ICacheService>();
        _serviceDirectoryClient = Substitute.For<IServiceDirectoryClient>();
        _httpContext = new DefaultHttpContext();
    }
    
    [Fact]
    public async Task ShouldSetTextualPropertiesToDfeAdmin_WhenUserIsDfeAdmin()
    {
        // Arrange
        var welcomeModel = new WelcomeModel(_cacheService, _serviceDirectoryClient);
        var claims = new List<Claim>
        {
            new(FamilyHubsClaimTypes.OrganisationId, "-1"),
            new(FamilyHubsClaimTypes.FullName, "Dfe Admin"),
            new(FamilyHubsClaimTypes.Role, RoleTypes.DfeAdmin)
        };
        _httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
        welcomeModel.PageContext = new PageContext { HttpContext = _httpContext };
        _serviceDirectoryClient.GetOrganisationById(-1)
            .Returns(CreateTestOrganisationDetailsDto("Some Dfe Name"));

        // Act
        await welcomeModel.OnGet();

        // Assert
        Assert.Equal("Dfe Admin", welcomeModel.Heading);
        Assert.Equal("Department for Education", welcomeModel.CaptionText);
        Assert.Equal("Manage users, services, locations and organisations and view performance data.", welcomeModel.Description);
    }

    [Theory] [InlineData(RoleTypes.LaManager)] [InlineData(RoleTypes.LaDualRole)]
    public async Task ShouldSetTextualPropertiesToLaAdmin_WhenUserIsLaAdmin(string role)
    {
        // Arrange
        var welcomeModel = new WelcomeModel(_cacheService, _serviceDirectoryClient);
        var claims = new List<Claim>
        {
            new(FamilyHubsClaimTypes.OrganisationId, "1"),
            new(FamilyHubsClaimTypes.FullName, "La Admin"),
            new(FamilyHubsClaimTypes.Role, role)
        };
        _httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
        welcomeModel.PageContext = new PageContext { HttpContext = _httpContext };
        _serviceDirectoryClient.GetOrganisationById(1)
            .Returns(CreateTestOrganisationDetailsDto("Local Authority Name"));

        // Act
        await welcomeModel.OnGet();

        // Assert
        Assert.Equal("La Admin", welcomeModel.Heading);
        Assert.Equal("Local Authority Name", welcomeModel.CaptionText);
        Assert.Equal("Manage users, services, locations and organisations and view performance data.", welcomeModel.Description);
    }
    
    [Theory]
    [InlineData(RoleTypes.VcsManager)]
    [InlineData(RoleTypes.VcsDualRole)]
    public async Task ShouldSetTextualPropertiesToVcsAdmin_WhenUserIsVcsAdmin(string role)
    {
        // Arrange
        var welcomeModel = new WelcomeModel(_cacheService, _serviceDirectoryClient);
        var claims = new List<Claim>
        {
            new(FamilyHubsClaimTypes.OrganisationId, "1"),
            new(FamilyHubsClaimTypes.FullName, "Vcs Admin"),
            new(FamilyHubsClaimTypes.Role, role)
        };
        _httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
        welcomeModel.PageContext = new PageContext { HttpContext = _httpContext };
        _serviceDirectoryClient.GetOrganisationById(1)
            .Returns(CreateTestOrganisationDetailsDto("Vcs organisation name"));

        // Act
        await welcomeModel.OnGet();

        // Assert
        Assert.Equal("Vcs Admin", welcomeModel.Heading);
        Assert.Equal("Vcs organisation name", welcomeModel.CaptionText);
        Assert.Equal("Manage services, locations and organisations and view performance data.", welcomeModel.Description);
    }

    [Theory]
    [InlineData(RoleTypes.DfeAdmin, MenuPage.Dfe)]
    [InlineData(RoleTypes.LaManager, MenuPage.La)]
    [InlineData(RoleTypes.LaDualRole, MenuPage.La)]
    [InlineData(RoleTypes.VcsManager, MenuPage.Vcs)]
    [InlineData(RoleTypes.VcsDualRole, MenuPage.Vcs)]
    public async Task ShouldSetMenuPageCorrectly_BasedOnUserRole(string role, MenuPage expected)
    {
        // Arrange
        var welcomeModel = new WelcomeModel(_cacheService, _serviceDirectoryClient);
        var claims = new List<Claim>
        {
            new(FamilyHubsClaimTypes.OrganisationId, "-1"),
            new(FamilyHubsClaimTypes.FullName, "Dfe Admin"),
            new(FamilyHubsClaimTypes.Role, role)
        };
        _httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
        welcomeModel.PageContext = new PageContext { HttpContext = _httpContext };
        _serviceDirectoryClient
            .GetOrganisationById(-1).Returns(CreateTestOrganisationDetailsDto("Some Dfe Name"));

        // Act
        await welcomeModel.OnGet();

        // Assert
        Assert.Equal(expected, welcomeModel.MenuPage);
    }

    [Fact]
    public async Task ShouldThrowException_WhenOrganisationIdIsNotANumber()
    {
        // Arrange
        var welcomeModel = new WelcomeModel(_cacheService, _serviceDirectoryClient);
        var claims = new List<Claim>
        {
            new(FamilyHubsClaimTypes.FullName, "Dfe Admin"),
            new(FamilyHubsClaimTypes.Role, RoleTypes.LaManager),
            new(FamilyHubsClaimTypes.OrganisationId, "NotANumber")
        };
        _httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
        welcomeModel.PageContext = new PageContext { HttpContext = _httpContext };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => welcomeModel.OnGet());
    }
    
    private static OrganisationDetailsDto CreateTestOrganisationDetailsDto(string nameOfOrg)
    {
        return new OrganisationDetailsDto
        {
            Name = nameOfOrg,
            OrganisationType = OrganisationType.NotSet,
            Description = "DfE description",
            AdminAreaCode = "abc"
        };
    }
}