using AutoFixture;
using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.AccountAdmin.Pages;
using Microsoft.AspNetCore.Mvc;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.AccountAdmin
{
    public class WhichLocalAuthorityTests
    {
        private readonly ICacheService _mockCacheService;
        private readonly IServiceDirectoryClient _serviceDirectoryClient;
        private readonly PermissionModel _permissionModel;
        private const string ValidLocalAuthority = "ValidLocalAuthority";
        private const long ValidLocalAuthorityId = 1234;
        
        
        public WhichLocalAuthorityTests()
        {
            var fixture = new Fixture();
            var organisations = fixture.Create<List<OrganisationDto>>();

            organisations[0].Id= ValidLocalAuthorityId;
            organisations[0].Name = ValidLocalAuthority;
            for (var i = 1; i < organisations.Count; i++)
            {
                organisations[i].Id = i;
            }
            
            _mockCacheService = Substitute.For<ICacheService>();
            _mockCacheService.GetOrganisations().Returns(organisations);
            
            _permissionModel = fixture.Create<PermissionModel>();
            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(_permissionModel);

            _serviceDirectoryClient = Substitute.For<IServiceDirectoryClient>();
            _serviceDirectoryClient.GetCachedLaOrganisations(Arg.Any<CancellationToken>())
                .Returns([
                    new OrganisationDto()
                    {
                        OrganisationType = OrganisationType.LA,
                        Name = ValidLocalAuthority,
                        Description = "Test",
                        AdminAreaCode = "Test",
                        Id = ValidLocalAuthorityId
                    }
                ]);
        }

        [Fact]
        public async Task OnGet_LaOrganisationName_Set()
        {
            //  Arrange
            var sut = new WhichLocalAuthority(_mockCacheService, _serviceDirectoryClient) 
            { 
                LaOrganisationName = string.Empty, 
                LocalAuthorities = new List<string>() 
            };

            //  Act
            await sut.OnGet();

            //  Assert
            Assert.Equal(_permissionModel.LaOrganisationName, sut.LaOrganisationName);

        }

        [Theory]
        [InlineData(true, "/TypeOfUserVcs")]
        [InlineData(false, "/TypeOfUserLa")]
        public async Task OnGet_BackLink_Set(bool vcsJourney, string expectedPath)
        {
            //  Arrange
            _permissionModel.VcsManager = vcsJourney;
            _permissionModel.VcsProfessional = vcsJourney;
            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(_permissionModel);
            var sut = new WhichLocalAuthority(_mockCacheService, _serviceDirectoryClient)
            {
                LaOrganisationName = string.Empty,
                LocalAuthorities = new List<string>()
            };

            //  Act
            await sut.OnGet();

            //  Assert
            Assert.Equal(expectedPath, sut.PreviousPageLink);

        }

        [Theory]
        [InlineData(true, "Which local authority area do they work in?")]
        [InlineData(false, "Which local authority do they work for?")]
        public async Task OnGet_PageHeading_Set(bool vcsJourney, string expectedHeading)
        {
            //  Arrange
            _permissionModel.VcsManager = vcsJourney;
            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(_permissionModel);
            var sut = new WhichLocalAuthority(_mockCacheService, _serviceDirectoryClient)
            {
                LaOrganisationName = string.Empty,
                LocalAuthorities = []
            };

            //  Act
            await sut.OnGet();

            //  Assert
            Assert.Equal(expectedHeading, sut.PageHeading);

        }

        [Fact]
        public async Task OnPost_ModelStateInvalid_ReturnsPageWithError()
        {
            //  Arrange
            
            var sut = new WhichLocalAuthority(_mockCacheService, _serviceDirectoryClient)
            {
                LaOrganisationName = string.Empty,
                LocalAuthorities = new List<string>()
            };

            sut.ModelState.AddModelError("SomeError", "SomeErrorMessage");

            //  Act
            await sut.OnPost();

            //  Assert
            Assert.True(sut.HasValidationError);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(Constants.TooLongGreaterThan255)]
        public async Task OnPost_InvalidName_ReturnsPageWithError(string authorityName)
        {
            //  Arrange
            var sut = new WhichLocalAuthority(_mockCacheService, _serviceDirectoryClient)
            {
                LaOrganisationName = authorityName,
                LocalAuthorities = []
            };


            //  Act
            await sut.OnPost();

            //  Assert
            Assert.True(sut.HasValidationError);
        }

        [Theory]
        [InlineData(true, "/WhichVcsOrganisation")]
        [InlineData(false, "/UserEmail")]
        public async Task OnPost_Valid_RedirectsToExpectedPage(bool isVcsJourney, string redirectPage)
        {
            //  Arrange
            _permissionModel.VcsManager = isVcsJourney;
            _permissionModel.VcsProfessional = isVcsJourney;
            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(_permissionModel);
            var sut = new WhichLocalAuthority(_mockCacheService, _serviceDirectoryClient)
            {
                LaOrganisationName = ValidLocalAuthority,
                LocalAuthorities = []
            };


            //  Act
            var result = await sut.OnPost();

            //  Assert

            Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal(redirectPage, ((RedirectToPageResult)result).PageName);

        }

        [Fact]
        public async Task OnPost_Valid_SetsValueInCache()
        {
            //  Arrange
            var sut = new WhichLocalAuthority(_mockCacheService, _serviceDirectoryClient)
            {
                LaOrganisationName = ValidLocalAuthority,
                LocalAuthorities = []
            };


            //  Act
            _ = await sut.OnPost();

            //  Assert
            await _mockCacheService
                .Received()
                .StorePermissionModel(
                Arg.Is<PermissionModel>(arg => arg.LaOrganisationName == ValidLocalAuthority 
                                               && arg.LaOrganisationId == ValidLocalAuthorityId), Arg.Any<string>());
        }

    }
}
