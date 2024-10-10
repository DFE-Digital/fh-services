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
    public class WhichVcsOrganisationTests
    {
        private readonly ICacheService _mockCacheService;
        private readonly IServiceDirectoryClient _serviceDirectoryClient;
        private readonly Fixture _fixture;
        private const string ValidVcsOrganisation = "ValidLocalAuthority";
        private const long ValidVcsOrganisationId = 1234;
        private readonly PermissionModel permissionModel;

        public WhichVcsOrganisationTests()
        {
            _fixture = new Fixture();
            var organisations = _fixture.Create<List<OrganisationDto>>();

            organisations[0].Id= ValidVcsOrganisationId;
            organisations[0].Name = ValidVcsOrganisation;
            for (var i = 1; i < organisations.Count; i++)
            {
                organisations[i].Id = i;
            }
            

            _mockCacheService = Substitute.For<ICacheService>();
            _mockCacheService.GetOrganisations().Returns(organisations);
            
            permissionModel = _fixture.Create<PermissionModel>();
            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(permissionModel);

            _serviceDirectoryClient = Substitute.For<IServiceDirectoryClient>();
            _serviceDirectoryClient.GetCachedVcsOrganisations(Arg.Any<long>(), Arg.Any<CancellationToken>())
                .Returns([
                    new OrganisationDto()
                    {
                        OrganisationType = OrganisationType.LA,
                        Name = ValidVcsOrganisation,
                        Description = "Test",
                        AdminAreaCode = "Test",
                        Id = ValidVcsOrganisationId
                    }
                ]);
        }

        [Fact]
        public async Task OnGet_VcsOrganisationName_Set()
        {
            //  Arrange
            
            var sut = new WhichVcsOrganisation(_mockCacheService, _serviceDirectoryClient) 
            { 
                VcsOrganisationName = string.Empty, 
                VcsOrganisations = []
            };

            //  Act
            await sut.OnGet();

            //  Assert
            Assert.Equal(permissionModel.VcsOrganisationName, sut.VcsOrganisationName);

        }

        [Fact]
        public async Task OnGet_BackLink_Set()
        {
            //  Arrange
            permissionModel.VcsManager = true;
            permissionModel.VcsProfessional = true;
            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(permissionModel);
            var sut = new WhichVcsOrganisation(_mockCacheService, _serviceDirectoryClient)
            {
                VcsOrganisationName = string.Empty,
                VcsOrganisations = []
            };

            //  Act
            await sut.OnGet();

            //  Assert
            Assert.Equal("/WhichLocalAuthority", sut.PreviousPageLink);

        }

        [Fact]
        public async Task OnGet_PageHeading_Set()
        {
            //  Arrange
            permissionModel.VcsManager = true;
            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(permissionModel);
            var sut = new WhichVcsOrganisation(_mockCacheService, _serviceDirectoryClient)
            {
                VcsOrganisationName = string.Empty,
                VcsOrganisations = new List<string>()
            };

            //  Act
            await sut.OnGet();

            //  Assert
            Assert.Equal("Which organisation do they work for?", sut.PageHeading);

        }

        [Fact]
        public async Task OnPost_ModelStateInvalid_ReturnsPageWithError()
        {
            //  Arrange
            permissionModel.VcsManager = true;
            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(permissionModel);
            
            var sut = new WhichVcsOrganisation(_mockCacheService, _serviceDirectoryClient)
            {
                VcsOrganisationName = string.Empty,
                VcsOrganisations = new List<string>()
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
            var sut = new WhichVcsOrganisation(_mockCacheService, _serviceDirectoryClient)
            {
                VcsOrganisationName = authorityName,
                VcsOrganisations = []
            };
            
            //  Act
            await sut.OnPost();

            //  Assert
            Assert.True(sut.HasValidationError);
        }

        [Fact]
        public async Task OnPost_Valid_RedirectsToExpectedPage()
        {
            //  Arrange
            permissionModel.VcsManager = true;
            permissionModel.VcsProfessional = true;
            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(permissionModel);
            var sut = new WhichVcsOrganisation(_mockCacheService, _serviceDirectoryClient)
            {
                VcsOrganisationName = ValidVcsOrganisation,
                VcsOrganisations = []
            };

            //  Act
            var result = await sut.OnPost();

            //  Assert
            Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/UserEmail", ((RedirectToPageResult)result).PageName);
        }

        [Fact]
        public async Task OnPost_Valid_SetsValueInCache()
        {
            //  Arrange
            var sut = new WhichVcsOrganisation(_mockCacheService, _serviceDirectoryClient)
            {
                VcsOrganisationName = ValidVcsOrganisation,
                VcsOrganisations = new List<string>()
            };
            
            //  Act
            _ = await sut.OnPost();

            //  Assert
            await _mockCacheService.Received().StorePermissionModel(
                Arg.Is<PermissionModel>(arg => arg.VcsOrganisationName == ValidVcsOrganisation 
                                               && arg.VcsOrganisationId == ValidVcsOrganisationId), Arg.Any<string>());
        }
    }
}
