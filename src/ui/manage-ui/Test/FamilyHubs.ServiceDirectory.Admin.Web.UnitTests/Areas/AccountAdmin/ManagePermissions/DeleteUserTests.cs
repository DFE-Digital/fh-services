using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.AccountAdmin.Pages.ManagePermissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.AccountAdmin.ManagePermissions
{
    public class DeleteUserTests
    {
        private readonly IIdamClient _mockIdamClient;
        private readonly ICacheService _mockCacheService;
        private readonly IEmailService _mockEmailService;

        public DeleteUserTests()
        {
            _mockIdamClient = Substitute.For<IIdamClient>();
            _mockCacheService = Substitute.For<ICacheService>();
            _mockEmailService = Substitute.For<IEmailService>();
        }

        [Fact]
        public async Task OnGet_BackPathSet()
        {
            //  Arrange
            const long accountId = 1;
            var account = new AccountDto
            {
                Name = "TestUser"
            };
            _mockIdamClient.GetAccountById(Arg.Any<long>()).Returns(account);
            _mockCacheService.RetrieveLastPageName().Returns(Task.FromResult("testurl"));

            var sut = new DeleteUserModel(_mockIdamClient, _mockCacheService, _mockEmailService);

            //  Act
            await sut.OnGet(accountId);

            //  Assert
            Assert.Equal("testurl", sut.BackUrl);
        }

        [Fact]
        public async Task OnGet_AccountRetrievedFromIdams()
        {
            //  Arrange
            const long accountId = 1;
            var account = new AccountDto
            {
                Name = "TestUser"
            };
            _mockIdamClient.GetAccountById(Arg.Any<long>()).Returns(account);

            var sut = new DeleteUserModel(_mockIdamClient, _mockCacheService, _mockEmailService);

            //  Act
            await sut.OnGet(accountId);

            //  Assert
            await _mockIdamClient.Received().GetAccountById(accountId);
        }

        [Fact]
        public async Task OnGet_UserNameStoredInCache()
        {
            //  Arrange
            const long accountId = 1;
            const string userName = "TestUser";
            var account = new AccountDto
            {
                Name = userName
            };
            _mockIdamClient.GetAccountById(Arg.Any<long>()).Returns(account);

            var sut = new DeleteUserModel(_mockIdamClient, _mockCacheService, _mockEmailService);

            //  Act
            await sut.OnGet(accountId);

            //  Assert
            await _mockCacheService.Received(1).StoreString("DeleteUserName", userName);
        }

        [Fact]
        public async Task OnPost_NoActionSelected_ReturnsPage()
        {
            //  Arrange
            const long accountId = 1;
            var sut = new DeleteUserModel(_mockIdamClient, _mockCacheService, _mockEmailService);
            sut.ModelState.AddModelError("test", "test");

            //  Act
            var result = await sut.OnPost(accountId);

            //  Assert
            Assert.IsType<PageResult>(result);
            Assert.True(sut.Errors.HasErrors);
        }

        [Fact]
        public async Task OnPost_InvokesDeleteAccountMethodWhenYesSelected()
        {
            //  Arrange
            const long accountId = 1;

            var account = new AccountDto
            {
                Email = "test@test.com",
                Claims = [new AccountClaimDto() { Name = "role", Value = "VcsManager" }]
            };
            _mockIdamClient.GetAccountById(Arg.Any<long>()).Returns(account);

            await _mockIdamClient.DeleteAccount(Arg.Any<long>());
            var sut = new DeleteUserModel(_mockIdamClient, _mockCacheService, _mockEmailService)
            {
                DeleteUser = true
            };

            //  Act
            var result = await sut.OnPost(accountId);

            //  Assert
            Assert.IsType<RedirectToPageResult>(result);
            Assert.False(sut.Errors.HasErrors);
            await _mockIdamClient.Received(1).DeleteAccount(accountId);
        }

        [Fact]
        public async Task OnPost_NotInvokesDeleteAccountMethodWhenNoSelected()
        {
            //  Arrange
            const long accountId = 1;
            var sut = new DeleteUserModel(_mockIdamClient, _mockCacheService, _mockEmailService)
            {
                DeleteUser = false
            };

            //  Act
            var result = await sut.OnPost(accountId);

            //  Assert
            Assert.IsType<RedirectToPageResult>(result);
            Assert.False(sut.Errors.HasErrors);
            await _mockIdamClient.Received(0).DeleteAccount(accountId);
        }

        [Fact]
        public async Task OnPost_EmailNotificationIsSent()
        {
            //  Arrange
            const long accountId = 1;
            await _mockEmailService.SendAccountDeletedEmail(Arg.Any<AccountDeletedNotificationModel>());

            var account = new AccountDto
            {
                Email = "test@test.com",
                Claims = [new AccountClaimDto() { Name = "role", Value = "VcsManager" }]
            };
            _mockIdamClient.GetAccountById(Arg.Any<long>()).Returns(account);

            var sut = new DeleteUserModel(_mockIdamClient, _mockCacheService, _mockEmailService)
            {
                DeleteUser = true
            };

            //  Act
            await sut.OnPost(accountId);

            //  Assert            
            await _mockEmailService.Received(1).SendAccountDeletedEmail(Arg.Is<AccountDeletedNotificationModel>(x =>
                x.EmailAddress == account.Email && x.Role == "VcsManager"));
        }
    }
}