using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.AccountAdmin.Pages.ManagePermissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.AccountAdmin.ManagePermissions
{
    public class EditEmailTests
    {
        private readonly IIdamClient _mockIdamClient;
        private readonly IEmailService _mockEmailService;
        private readonly ILogger<EditEmailModel> _logger;
        
        private const int AccountId = 1234;

        public EditEmailTests()
        {
            _mockIdamClient = Substitute.For<IIdamClient>();
            _mockEmailService = Substitute.For<IEmailService>();
            _logger = Substitute.For<ILogger<EditEmailModel>>();
        }

        [Fact]
        public void OnGet_BackPathSet()
        {
            //  Arrange
            var sut = new EditEmailModel(_mockIdamClient, _mockEmailService, _logger)
            {
                EmailAddress = string.Empty, AccountId = AccountId.ToString()
            };

            //  Act
            sut.OnGet();

            //  Assert
            Assert.Equal($"/AccountAdmin/ManagePermissions/{AccountId}", sut.BackButtonPath);
        }

        [Fact]
        public async Task OnPost_InvalidEmail_ReturnsPage()
        {
            //  Arrange
            var sut = new EditEmailModel(_mockIdamClient, _mockEmailService, _logger)
            {
                EmailAddress = string.Empty, AccountId = AccountId.ToString()
            };

            //  Act
            var result = await sut.OnPost();

            //  Assert
            Assert.IsType<PageResult>(result);
            Assert.True(sut.HasValidationError);
        }

        [Fact]
        public async Task OnPost_InvokesUpdateMethod()
        {
            //  Arrange
            const string email = "some.guy@test.com";
            var account = new AccountDto { Id = AccountId, Email = "oldEmail", Name = "name" , 
                Claims = [new AccountClaimDto { Name = "role", Value = "LaManager" }]
            };
            _mockIdamClient.GetAccountById(AccountId).Returns(Task.FromResult<AccountDto?>(account));

            var sut = new EditEmailModel(_mockIdamClient, _mockEmailService, _logger)
            {
                EmailAddress = email, AccountId = AccountId.ToString()
            };

            //  Act
            var result = await sut.OnPost();

            //  Assert
            Assert.IsType<RedirectToPageResult>(result);
            Assert.False(sut.HasValidationError);
            await _mockIdamClient.Received(1).UpdateAccount(Arg.Any<UpdateAccountDto>(), Arg.Any<CancellationToken>());
        }


        [Fact]
        public async Task OnPost_EmailNotificationIsSent()
        {
            //  Arrange
            const string email = "some.guy@test.com";
            var account = new AccountDto
            {
                Id = AccountId,
                Email = "oldEmail",
                Name = "name",
                Claims = [new AccountClaimDto { Name = "role", Value = "LaManager" }]
            };
            _mockIdamClient.GetAccountById(1234).Returns(Task.FromResult<AccountDto?>(account));
            await _mockEmailService.SendAccountEmailUpdatedEmail(Arg.Any<EmailChangeNotificationModel>());

            var sut = new EditEmailModel(_mockIdamClient, _mockEmailService, _logger)
            {
                EmailAddress = email, AccountId = AccountId.ToString()
            };            

            //  Act
            await sut.OnPost();

            //  Assert            
            await _mockEmailService
                .Received(1)
                .SendAccountEmailUpdatedEmail(Arg.Is<EmailChangeNotificationModel>(x => 
                    x.EmailAddress == email && x.Role == "LaManager"));
        }
    }
}
