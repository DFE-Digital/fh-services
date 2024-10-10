using AutoFixture;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.AccountAdmin.Pages;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.AccountAdmin
{
    public class TypeOfUserLaTests
    {
        private readonly ICacheService _mockCacheService;
        private readonly Fixture _fixture;

        public TypeOfUserLaTests()
        {
            _mockCacheService = Substitute.For<ICacheService>();
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public async Task OnGet_UserTypeForLa_SetToExpected(bool isLaManager, bool isLaProfessional)
        {
            //  Arrange
            var permissionModel = _fixture.Create<PermissionModel>();
            permissionModel.LaManager = isLaManager;
            permissionModel.LaProfessional = isLaProfessional;

            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(permissionModel);
            var sut = new TypeOfUserLa(_mockCacheService);

            //  Act
            await sut.OnGet();

            //  Assert
            Assert.Equal(isLaManager, sut.LaManager);
            Assert.Equal(isLaProfessional, sut.LaProfessional);
        }

        [Fact]
        public async Task OnPost_ModelStateInvalid_ReturnsPageWithError()
        {
            //  Arrange
            var permissionModel = _fixture.Create<PermissionModel>();
            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(permissionModel);
            
            var sut = new TypeOfUserLa(_mockCacheService);
            sut.ModelState.AddModelError("SomeError", "SomeErrorMessage");

            //  Act
            await sut.OnPost();

            //  Assert
            Assert.True(sut.Errors.HasErrors);
        }

        [Fact]
        public async Task OnPost_Valid_RedirectsToExpectedPage()
        {
            //  Arrange
            var permissionModel = _fixture.Create<PermissionModel>();
            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(permissionModel);
            var sut = new TypeOfUserLa(_mockCacheService) { SelectedValues = [nameof(TypeOfUserLa.LaManager)] };

            //  Act
            var result = await sut.OnPost();

            //  Assert

            Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/WhichLocalAuthority", ((RedirectToPageResult) result).PageName);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public async Task OnPost_Valid_SetsValueInCache(bool expectedManager, bool expectedProfessional)
        {
            //  Arrange
            var permissionModel = _fixture.Create<PermissionModel>();
            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(permissionModel);
            var sut = new TypeOfUserLa(_mockCacheService)
                { SelectedValues = new[] {expectedManager ? nameof(TypeOfUserLa.LaManager) : null, expectedProfessional ? nameof(TypeOfUserLa.LaProfessional) : null}.OfType<string>() };

            //  Act
            _ = await sut.OnPost();

            //  Assert
            await _mockCacheService.Received().StorePermissionModel(
                Arg.Is<PermissionModel>(arg =>
                    arg.LaManager == expectedManager && arg.LaProfessional == expectedProfessional &&
                    arg.LaManager == expectedManager), Arg.Any<string>());
        }
    }
}