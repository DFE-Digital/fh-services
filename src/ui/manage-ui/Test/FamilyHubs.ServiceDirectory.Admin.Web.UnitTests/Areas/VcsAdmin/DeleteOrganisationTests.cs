using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.VcsAdmin.Pages;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.VcsAdmin;

public class DeleteOrganisationTests
{
    private readonly IServiceDirectoryClient _mockServiceDirectoryClient;
    private readonly ICacheService _mockCacheService;
    private readonly IIdamClient _mockIdamClient;
    
    private const int OrganisationId = 1;
    
    
    public DeleteOrganisationTests()
    {
        _mockServiceDirectoryClient = Substitute.For<IServiceDirectoryClient>();
        _mockCacheService = Substitute.For<ICacheService>();
        _mockIdamClient = Substitute.For<IIdamClient>();                       
    }

    [Fact]
    public async Task OnGet_BackPathSet()
    {
        //  Arrange
        _mockCacheService.RetrieveLastPageName().Returns(Task.FromResult("testurl/abc"));

        var sut = new DeleteOrganisationModel(
            _mockServiceDirectoryClient, 
            _mockCacheService, 
            _mockIdamClient)
        {
            DeleteOrganisation = null , 
            BackButtonPath = "testurl"
        };

        //  Act
        await sut.OnGet(OrganisationId);

        //  Assert
        Assert.Equal("testurl/abc", sut.BackButtonPath);
    }

    [Fact]
    public async Task OnGet_OranisationNameIsSavedInCache()
    {
        //  Arrange
        var organisation = new OrganisationDetailsDto
        {
            Id= OrganisationId, 
            Name = "TestOrg", 
            Description="description", 
            AdminAreaCode="code",
            OrganisationType=OrganisationType.VCFS
        };
        _mockServiceDirectoryClient.GetOrganisationById(Arg.Any<long>(), Arg.Any<CancellationToken>()).Returns(organisation);
        _mockCacheService.When(x => x.StoreString(Arg.Any<string>(), Arg.Any<string>()));
        var sut = new DeleteOrganisationModel(_mockServiceDirectoryClient, _mockCacheService, _mockIdamClient) { DeleteOrganisation = null };

        //  Act
        await sut.OnGet(OrganisationId);

        //  Assert
        await _mockCacheService.Received(1).StoreString("DeleteOrganisationName", organisation.Name);        
    }


    [Fact]
    public async Task OnPost_HasValidationErrorWhenNoSelection()
    {
        //  Arrange
        var sut = new DeleteOrganisationModel(_mockServiceDirectoryClient, _mockCacheService, _mockIdamClient)
        {
            DeleteOrganisation = null
        };

        //  Act
        var result = await sut.OnPost(OrganisationId);

        //  Assert
        Assert.IsType<PageResult>(result);
        Assert.True(sut.HasValidationError);
    }

    [Fact]
    public async Task OnPost_UserRedirectedToResultPageWhenNoSelected()
    {
        //  Arrange
        var sut = new DeleteOrganisationModel(_mockServiceDirectoryClient, _mockCacheService, _mockIdamClient)
        {
            DeleteOrganisation = false
        };

        //  Act
        var result = await sut.OnPost(OrganisationId);

        //  Assert
        Assert.IsType<RedirectToPageResult>(result);
        Assert.False(sut.HasValidationError);
        Assert.Equal("DeleteOrganisationResult", ((RedirectToPageResult)result).PageName);
    }

    [Fact]
    public async Task OnPost_OrganisationAndAccountsDeletedWhenYesSelected()
    {
        //  Arrange
        _mockIdamClient.When(x => x.DeleteOrganisationAccounts(Arg.Any<long>()));
        _mockServiceDirectoryClient.When(x => x.DeleteOrganisation(Arg.Any<long>()));

        var sut = new DeleteOrganisationModel(_mockServiceDirectoryClient, _mockCacheService, _mockIdamClient) { DeleteOrganisation = true };

        //  Act
        var result = await sut.OnPost(OrganisationId);

        //  Assert
        await _mockIdamClient.Received(1).DeleteOrganisationAccounts(OrganisationId);
        await _mockServiceDirectoryClient.Received(1).DeleteOrganisation(OrganisationId);

        Assert.IsType<RedirectToPageResult>(result);
        Assert.False(sut.HasValidationError);
        Assert.Equal("DeleteOrganisationResult", ((RedirectToPageResult)result).PageName);
    }
}