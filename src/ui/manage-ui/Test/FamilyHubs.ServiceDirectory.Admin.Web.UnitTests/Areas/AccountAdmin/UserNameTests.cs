using AutoFixture;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.AccountAdmin.Pages;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.AccountAdmin
{
    public class UserNameTests
    {
        private readonly ICacheService _mockCacheService;
        private readonly PermissionModel _permissionModel;

        public UserNameTests()
        {
            _mockCacheService = Substitute.For<ICacheService>();
            var fixture = new Fixture();
            _permissionModel = fixture.Create<PermissionModel>();
            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(_permissionModel);
        }

        [Fact]
        public async Task OnGet_FullName_Set()
        {
            //  Arrange
            var sut = new UserName(_mockCacheService) { FullName = string.Empty };

            //  Act
            await sut.OnGet();

            //  Assert
            Assert.Equal(_permissionModel.FullName, sut.FullName);

        }

        [Fact]
        public async Task OnPost_ModelStateInvalid_ReturnsPageWithError()
        {
            //  Arrange
            var sut = new UserName(_mockCacheService) { FullName = string.Empty };
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
        public async Task OnPost_InvalidName_ReturnsPageWithError(string name)
        {
            //  Arrange
            var sut = new UserName(_mockCacheService) { FullName = name };

            //  Act
            await sut.OnPost();

            //  Assert
            Assert.True(sut.HasValidationError);
        }

        [Fact]
        public async Task OnPost_Valid_RedirectsToExpectedPage()
        {
            //  Arrange
            var sut = new UserName(_mockCacheService) { FullName = "Someones Name" };

            //  Act
            var result = await sut.OnPost();

            //  Assert

            Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/AddPermissionCheckAnswer", ((RedirectToPageResult)result).PageName);

        }

        [Fact]
        public async Task OnPost_Valid_SetsValueInCache()
        {
            //  Arrange
            var sut = new UserName(_mockCacheService) { FullName = "Someones Name" };

            //  Act
            _ = await sut.OnPost();

            //  Assert
            await _mockCacheService.Received().StorePermissionModel(
                Arg.Is<PermissionModel>(arg => arg.FullName == "Someones Name"), Arg.Any<string>());

        }

    }
}
