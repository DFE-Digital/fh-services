using AutoFixture;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.AccountAdmin.Pages;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.AccountAdmin
{
    public class TypeOfUserVcsTests
    {
        private readonly ICacheService _mockCacheService;
        private readonly PermissionModel _permissionModel;

        public TypeOfUserVcsTests()
        {
            _mockCacheService = Substitute.For<ICacheService>();
            var fixture = new Fixture();
            _permissionModel = fixture.Create<PermissionModel>();
            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(_permissionModel);
        }

        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData(true, true)]
        public async Task OnGet_ExpectedValuesSet(bool isVcsManager, bool isVcsProfessional)
        {
            //  Arrange
            _permissionModel.VcsManager = isVcsManager;
            _permissionModel.VcsProfessional = isVcsProfessional;

            _mockCacheService.GetPermissionModel(Arg.Any<string>()).Returns(_permissionModel);
            var sut = new TypeOfUserVcs(_mockCacheService);

            //  Act
            await sut.OnGet();

            //  Assert
            Assert.Equal(isVcsManager, sut.VcsManager);
            Assert.Equal(isVcsProfessional, sut.VcsProfessional);
        }

        [Fact]
        public async Task OnPost_ModelStateInvalid_ReturnsPageWithError()
        {
            //  Arrange
            var sut = new TypeOfUserVcs(_mockCacheService);
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
            var sut = new TypeOfUserVcs(_mockCacheService) { SelectedValues = new[]{ nameof(TypeOfUserVcs.VcsManager) } };

            //  Act
            var result = await sut.OnPost();

            //  Assert
            Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/WhichLocalAuthority", ((RedirectToPageResult)result).PageName);
        }

        [Theory]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData(true, true)]
        public async Task OnPost_Valid_SetsValueInCache(bool isVcsManager, bool isVcsProfessional)
        {
            //  Arrange
            var sut = new TypeOfUserVcs(_mockCacheService) 
                { SelectedValues = new[] {isVcsManager ? nameof(TypeOfUserVcs.VcsManager) : null, isVcsProfessional ? nameof(TypeOfUserVcs.VcsProfessional) : null}.OfType<string>() };

            //  Act
            _ = await sut.OnPost();

            //  Assert
            await _mockCacheService
                .Received()
                .StorePermissionModel(
                Arg.Is<PermissionModel>(arg => arg.VcsManager == isVcsManager 
                                               && arg.VcsProfessional == isVcsProfessional), 
                Arg.Any<string>());
        }
    }
}
