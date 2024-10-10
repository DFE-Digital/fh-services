using AutoFixture;
using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.VcsAdmin.Pages;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.VcsAdmin
{
    public class AddOrganisationTests
    {
        private readonly ICacheService _mockCacheService;
        private readonly IServiceDirectoryClient _mockServiceDirectoryClient;
        private readonly Fixture _fixture;
        private readonly HttpContext _httpContext;

        public AddOrganisationTests()
        {
            _mockCacheService = Substitute.For<ICacheService>();
            _mockServiceDirectoryClient = Substitute.For<IServiceDirectoryClient>();
            _fixture = new Fixture();

            _httpContext = new DefaultHttpContext();
            _httpContext.Request.Headers.Append("Host", "localhost:7216");
            _httpContext.Request.Headers.Append("Referer", "https://localhost:7216/Welcome");

            _mockServiceDirectoryClient.GetCachedVcsOrganisations(Arg.Any<long>(), CancellationToken.None)
                .Returns(Task.FromResult(new List<OrganisationDto>
                {
                    new()
                    {
                        OrganisationType = OrganisationType.LA,
                        Name = "Any",
                        Description = "Test",
                        AdminAreaCode = "Test",
                        Id = 1
                    }
                }));

            _mockCacheService.RetrieveString(CacheKeyNames.LaOrganisationId).Returns("1");
        }

        [Fact]
        public async Task OnPost_ModelStateInvalid_ReturnsPageWithError()
        {
            // Arrange
            var sut = new AddOrganisationModel(_mockCacheService, _mockServiceDirectoryClient) 
            { 
                PageContext = { HttpContext = _httpContext } 
            };
            sut.ModelState.AddModelError("SomeError", "SomeErrorMessage");

            // Act
            await sut.OnPost();

            // Assert
            Assert.True(sut.HasValidationError);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(Constants.TooLongGreaterThan255)]
        public async Task OnPost_InvalidOrganisation_ReturnsPageWithError(string organisationName)
        {
            //  Arrange
            var sut = new AddOrganisationModel(_mockCacheService, _mockServiceDirectoryClient)
            {
                OrganisationName = organisationName,
                PageContext = { HttpContext = _httpContext }
            };

            //  Act
            await sut.OnPost();

            //  Assert
            Assert.True(sut.HasValidationError);
        }

        [Fact]
        public async Task OnPost_ValidOrganisation_RedirectsToExpectedPage()
        {
            //  Arrange
            var sut = new AddOrganisationModel(_mockCacheService, _mockServiceDirectoryClient)
            {
                OrganisationName = "Some Name",
                PageContext = { HttpContext = _httpContext }
            };

            //  Act
            var result = await sut.OnPost();

            //  Assert

            Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/AddOrganisationCheckDetails", ((RedirectToPageResult)result).PageName);

        }

        [Fact]
        public async Task OnPost_ValidOrganisation_SetsValueInCache()
        {
            //  Arrange
            var permissionModel = _fixture.Create<PermissionModel>();
            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(permissionModel);
            var sut = new AddOrganisationModel(_mockCacheService, _mockServiceDirectoryClient)
            {
                OrganisationName = "Some Name",
                PageContext = { HttpContext = _httpContext }
            };

            //  Act
            _ = await sut.OnPost();

            //  Assert
            await _mockCacheService.Received().StoreString(Arg.Any<string>(), Arg.Is<string>(arg => arg == "Some Name"));
        }
    }
}
