using AutoFixture;
using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.VcsAdmin.Pages;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.VcsAdmin;

public class AddOrganisationWhichLocalAuthorityTests
{
    private readonly ICacheService _mockCacheService;
    private readonly IServiceDirectoryClient _serviceDirectoryClient;
    private const string ValidLocalAuthority = "ValidLocalAuthority";
    private const long ValidLocalAuthorityId = 1234;

    public AddOrganisationWhichLocalAuthorityTests()
    {
        var fixture = new Fixture();
            
        var organisations = fixture.Create<List<OrganisationDto>>();
        organisations[0].Id = ValidLocalAuthorityId;
        organisations[0].Name = ValidLocalAuthority;
            
        for (var i = 1; i < organisations.Count; i++)
        {
            organisations[i].Id = i;
        }


        _mockCacheService = Substitute.For<ICacheService>();
        _mockCacheService.GetOrganisations().Returns(organisations);

        _serviceDirectoryClient = Substitute.For<IServiceDirectoryClient>();
        _serviceDirectoryClient.GetCachedLaOrganisations(CancellationToken.None)
            .Returns(Task.FromResult(new List<OrganisationDto>
            {
                new()
                {
                    OrganisationType = OrganisationType.LA,
                    Name = ValidLocalAuthority,
                    Description = "Test",
                    AdminAreaCode = "Test",
                    Id = ValidLocalAuthorityId
                }
            }));
    }


    [Fact]
    public async Task OnGet_LaOrganisationName_Set()
    {
        //  Arrange
        var mockHttpContext = GetHttpContext(RoleTypes.DfeAdmin, -1);
        _mockCacheService.RetrieveString(CacheKeyNames.LaOrganisationId).Returns(ValidLocalAuthorityId.ToString());
        var sut = new AddOrganisationWhichLocalAuthorityModel(_mockCacheService, _serviceDirectoryClient)
        {
            LaOrganisationName = string.Empty,
            LocalAuthorities = [],
            PageContext = { HttpContext = mockHttpContext }
        };

        //  Act
        await sut.OnGet();

        //  Assert
        Assert.Equal(ValidLocalAuthority, sut.LaOrganisationName);
    }

    private static HttpContext GetHttpContext(string role, long organisationId)
    {
        var claims = new List<Claim> {
            new(FamilyHubsClaimTypes.FullName, "any") ,
            new(FamilyHubsClaimTypes.AccountId, "1"),
            new(FamilyHubsClaimTypes.OrganisationId, organisationId.ToString()),
            new(FamilyHubsClaimTypes.Role, role),
        };

        return TestHelper.GetHttpContext(claims);
    }
}