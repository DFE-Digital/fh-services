using AutoFixture;
using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.AccountAdmin.Pages;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.AccountAdmin
{
    public class UserEmailTests
    {
        private readonly ICacheService _mockCacheService;
        private readonly IIdamClient _mockIIdamClient;
        private readonly PermissionModel _permissionModel;

        public UserEmailTests()
        {
            _mockCacheService = Substitute.For<ICacheService>();
            _mockIIdamClient = Substitute.For<IIdamClient>();
            var fixture = new Fixture();
            _permissionModel = fixture.Create<PermissionModel>();
            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(_permissionModel);
        }

        [Fact]
        public async Task OnGet_EmailAddress_Set()
        {
            //  Arrange
            var sut = new UserEmail(_mockCacheService, _mockIIdamClient) { EmailAddress = string.Empty };

            //  Act
            await sut.OnGet();

            //  Assert
            Assert.Equal(_permissionModel.EmailAddress, sut.EmailAddress);
        }

        [Fact]
        public async Task OnPost_ModelStateInvalid_ReturnsPageWithError()
        {
            //  Arrange
            var sut = new UserEmail(_mockCacheService, _mockIIdamClient) { EmailAddress = string.Empty };
            sut.ModelState.AddModelError("SomeError", "SomeErrorMessage");

            //  Act
            await sut.OnPost();

            //  Assert
            Assert.True(sut.HasValidationError);
        }

        [Theory]
        [InlineData("invalidemail")]
        [InlineData("nodomain@i")]
        [InlineData("noAt.i")]
        public async Task OnPost_InvalidEmail_ReturnsPageWithError(string email)
        {
            //  Arrange
            var sut = new UserEmail(_mockCacheService, _mockIIdamClient) { EmailAddress = email };

            //  Act
            await sut.OnPost();

            //  Assert
            Assert.True(sut.HasValidationError);
        }

        [Fact]
        public async Task OnPost_Valid_RedirectsToExpectedPage()
        {
            //  Arrange
            var sut = new UserEmail(_mockCacheService, _mockIIdamClient) { EmailAddress = "someone@domain.com" };

            //  Act
            var result = await sut.OnPost();

            //  Assert
            Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/UserName", ((RedirectToPageResult)result).PageName);
        }

        [Fact]
        public async Task OnPost_Valid_SetsValueInCache()
        {
            //  Arrange
            var sut = new UserEmail(_mockCacheService, _mockIIdamClient) { EmailAddress = "someone@domain.com" };

            //  Act
            _ = await sut.OnPost();

            //  Assert
            await _mockCacheService.Received().StorePermissionModel(
                Arg.Is<PermissionModel>(arg => arg.EmailAddress == "someone@domain.com"), Arg.Any<string>());
        }
    }
}
