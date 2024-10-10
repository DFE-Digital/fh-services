using System.Security.Claims;
using AutoFixture;
using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.AccountAdmin.Pages;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.AccountAdmin
{
    public class TypeOfRoleTests
    {
        private readonly ICacheService _mockCacheService;
        private readonly Fixture _fixture;
        private readonly TypeOfRole _sut;

        public TypeOfRoleTests()
        {
            _mockCacheService = Substitute.For<ICacheService>();
            var mockServiceDirectoryClient = Substitute.For<IServiceDirectoryClient>();
            mockServiceDirectoryClient.GetCachedLaOrganisations(CancellationToken.None)
                .Returns(new List<OrganisationDto>([
                    new OrganisationDto
                    {
                        OrganisationType = OrganisationType.LA,
                        Name = "Test",
                        Description = "Test",
                        AdminAreaCode = "Test",
                        Id = 1
                    }
                ]));
            
            _sut = new TypeOfRole(_mockCacheService, mockServiceDirectoryClient)
            {
                SelectedValue = string.Empty,
                PageContext =
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new []{ new Claim("role", "DfeAdmin"), new Claim("OrganisationId", "1") }))
                    }
                }
            };

            _fixture = new Fixture();
        }

        [Fact]
        public async Task OnGet_OrganisationType_Set()
        {
            //  Arrange
            var permissionModel = _fixture.Create<PermissionModel>();
            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(permissionModel);

            //  Act
            await _sut.OnGet();

            //  Assert
            Assert.Equal(permissionModel.OrganisationType, _sut.SelectedValue);
        }

        [Fact]
        public async Task OnPost_ModelStateInvalid_ReturnsPageWithError()
        {
            //  Arrange
            _sut.ModelState.AddModelError("SomeError", "SomeErrorMessage");
            
            //  Act
            await _sut.OnPost();

            //  Assert
            Assert.True(_sut.Errors.HasErrors);
        }

        [Theory]
        [InlineData("LA", "/TypeOfUserLa")]
        [InlineData("VCS", "/TypeOfUserVcs")]
        public async Task OnPost_Valid_RedirectsToExpectedPage(string organisationType, string expectedRoute)
        {
            //  Arrange
            _sut.SelectedValue = organisationType;

            //  Act
            var result = await _sut.OnPost();

            //  Assert
            Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal(expectedRoute, ((RedirectToPageResult) result).PageName);
        }

        [Fact]
        public async Task OnPost_Valid_SetsValueInCache()
        {
            //  Arrange
            _sut.SelectedValue = "LA";

            //  Act
            _ = await _sut.OnPost();

            //  Assert
            await _mockCacheService
                .Received()
                .StorePermissionModel(Arg.Is<PermissionModel>(arg => arg.OrganisationType == "LA"),
                    Arg.Any<string>());
        }
    }
}