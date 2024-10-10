using AutoFixture;
using FamilyHubs.ServiceDirectory.Admin.Core;
using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Exceptions;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.VcsAdmin.Pages;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.VcsAdmin;

public class AddOrganisationCheckDetailsTests
{
    private readonly ICacheService _mockCacheService;
    private readonly IServiceDirectoryClient _mockServiceDirectoryClient;
    private readonly Fixture _fixture;
    private readonly HttpContext _httpContext;
        
    private const int OrganisationId = 123;

    public AddOrganisationCheckDetailsTests()
    {
        _mockCacheService = Substitute.For<ICacheService>();
        _mockServiceDirectoryClient = Substitute.For<IServiceDirectoryClient>();
        _fixture = new Fixture();

        _httpContext = new DefaultHttpContext();
        _httpContext.Request.Headers.Append("Host", "localhost:7216");
        _httpContext.Request.Headers.Append("Referer", "https://localhost:7216/Welcome");
    }

    [Fact]
    public async Task OnGet_Valid_SetsOrganisationNameFromCache()
    {
        //  Arrange
        var organisations = _fixture.Create<List<OrganisationDto>>();
        organisations[0].Id = OrganisationId;

        _mockServiceDirectoryClient.GetCachedLaOrganisations(Arg.Any<CancellationToken>()).Returns(organisations);
        _mockCacheService.RetrieveString(Arg.Any<string>()).Returns(OrganisationId.ToString());

        var sut = new AddOrganisationCheckDetailsModel(_mockCacheService, _mockServiceDirectoryClient)
        {
            PageContext = { HttpContext = _httpContext }
        };

        //  Act
        await sut.OnGet();

        //  Assert
        sut.OrganisationName.Should().Be(OrganisationId.ToString());
    }

    [Fact]
    public async Task OnPost_Valid_CreatesOrganisation()
    {
        //  Arrange            
        _mockCacheService.RetrieveString(CacheKeyNames.AddOrganisationName).Returns("Name");
        _mockCacheService.RetrieveString(CacheKeyNames.AdminAreaCode).Returns("AdminCode");
        _mockCacheService.RetrieveString(CacheKeyNames.LaOrganisationId).Returns(OrganisationId.ToString());
        var args = new List<OrganisationDetailsDto>();
        var outcome = new Outcome<long, ApiException>((long)1);
        _mockServiceDirectoryClient.CreateOrganisation(Arg.Do<OrganisationDetailsDto>(x => args.Add(x))).Returns(Task.FromResult(outcome));
        var sut = new AddOrganisationCheckDetailsModel(_mockCacheService, _mockServiceDirectoryClient)
        {
            PageContext = { HttpContext = _httpContext }
        };

        //  Act
        await sut.OnPost();

        //  Assert            
        await _mockServiceDirectoryClient.Received(1).CreateOrganisation(Arg.Any<OrganisationDetailsDto>());
        Assert.Equal("Name", args[0].Name);
        Assert.Equal("Name", args[0].Description);
        Assert.Equal("AdminCode", args[0].AdminAreaCode);
        Assert.Equal(OrganisationId, args[0].AssociatedOrganisationId);


    }

    [Fact]
    public async Task OnPost_Valid_RedirectsToExpectedPage()
    {
        //  Arrange
        var outcome = new Outcome<long, ApiException>(1);
        _mockServiceDirectoryClient.CreateOrganisation(Arg.Any<OrganisationDetailsDto>()).Returns(Task.FromResult(outcome));
        _mockCacheService.RetrieveString(Arg.Any<string>()).Returns(OrganisationId.ToString());
        var sut = new AddOrganisationCheckDetailsModel(_mockCacheService, _mockServiceDirectoryClient)
        {
            PageContext = { HttpContext = _httpContext }
        };

        //  Act
        var result = await sut.OnPost();

        //  Assert
        Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("/AddOrganisationResult", ((RedirectToPageResult)result).PageName);

    }
}