using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.AccountAdmin.Pages.ManagePermissions;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.AccountAdmin.ManagePermissions
{
    public class EditRolesTests
    {
        private readonly IIdamClient _mockIdamClient;
        private readonly IEmailService _mockEmailService;
        private const int AccountId = 1;

        public EditRolesTests()
        {
            _mockIdamClient = Substitute.For<IIdamClient>();
            _mockEmailService = Substitute.For<IEmailService>();
        }

        [Fact]
        public async Task OnGet_BackPathSet()
        {
            //  Arrange
            var account = new AccountDto
            {
                Claims = [new AccountClaimDto { Name = "role", Value = "LaManager" }]
            };
            _mockIdamClient.GetAccountById(Arg.Any<long>()).Returns(account);
            var sut = new EditRolesModel(_mockIdamClient, _mockEmailService);

            //  Act
            await sut.OnGet(AccountId);

            //  Assert
            Assert.Equal($"/AccountAdmin/ManagePermissions/{AccountId}", sut.BackButtonPath);
        }

        [Fact]
        public async Task OnGet_AccountRetrievedFromIdams()
        {
            //  Arrange
            const long accountId = 1;
            var account = new AccountDto
            {
                Claims = [new AccountClaimDto { Name = "role", Value = "LaManager" }]
            };
            _mockIdamClient.GetAccountById(Arg.Any<long>()).Returns(account);
            var sut = new EditRolesModel(_mockIdamClient, _mockEmailService);

            //  Act
            await sut.OnGet(accountId);

            //  Assert
            await _mockIdamClient.Received(1).GetAccountById(accountId);
        }

        [Theory]
        [InlineData(RoleTypes.LaManager, true, false, false, false)]
        [InlineData(RoleTypes.LaProfessional, false,true, false, false)]
        [InlineData(RoleTypes.LaDualRole, true, true, false, false)]
        [InlineData(RoleTypes.VcsManager,false, false, true, false )]
        [InlineData(RoleTypes.VcsProfessional, false, false, false, true)]
        [InlineData(RoleTypes.VcsDualRole, false, false, true, true)]
        public async Task OnGet_UserRolesSet(string role, bool laManagerValue, bool laProfessionalValue, bool vcsManagerValue, bool vcsProfessionalValue  )
        {
            //  Arrange
            var account = new AccountDto
            {
                Claims = [new AccountClaimDto { Name = "role", Value = role }]
            };
            _mockIdamClient.GetAccountById(Arg.Any<long>()).Returns(account);
            var sut = new EditRolesModel(_mockIdamClient, _mockEmailService);

            //  Act
            await sut.OnGet(AccountId);

            //  Assert            
            Assert.Equal(laManagerValue, sut.LaManager);
            Assert.Equal(laProfessionalValue, sut.LaProfessional);
            Assert.Equal(vcsManagerValue, sut.VcsManager);
            Assert.Equal(vcsProfessionalValue, sut.VcsProfessional);                       
        }


        [Theory]
        [InlineData(RoleTypes.LaManager, true, false)]
        [InlineData(RoleTypes.LaProfessional,true, false )]
        [InlineData(RoleTypes.LaDualRole, true, false)]
        [InlineData(RoleTypes.VcsManager, false, true )]
        [InlineData(RoleTypes.VcsProfessional, false, true)]
        [InlineData(RoleTypes.VcsDualRole, false, true)]
        public async Task OnGet_UserTypeSet(string role, bool isLaValue,  bool isVcsValue )
        {
            //  Arrange
            var account = new AccountDto
            {
                Claims = [new AccountClaimDto { Name = "role", Value = role }]
            };
            _mockIdamClient.GetAccountById(Arg.Any<long>()).Returns(account);
            var sut = new EditRolesModel(_mockIdamClient, _mockEmailService);

            //  Act
            await sut.OnGet(AccountId);

            //  Assert
            Assert.Equal(isLaValue, sut.IsLa);
            Assert.Equal(isVcsValue, sut.IsVcs);
        }

        [Fact]
        public async Task OnPost_NoRoleSelected_ReturnsPage()
        {
            //  Arrange
            var account = new AccountDto
            {
                Claims = [new AccountClaimDto { Name = "role", Value = "LaManager" }]
            };
            _mockIdamClient.GetAccountById(Arg.Any<long>()).Returns(account);
            var sut = new EditRolesModel(_mockIdamClient, _mockEmailService);

            //  Act
            var result = await sut.OnPost(AccountId);

            //  Assert
            Assert.IsType<PageResult>(result);
            Assert.True(sut.HasValidationError);
        }

        [Fact]
        public async Task OnPost_InvokesUpdateMethod()
        {
            //  Arrange
            var account = new AccountDto
            {
                Claims = [new AccountClaimDto { Name = "role", Value = "LaManager" }]
            };
            _mockIdamClient.GetAccountById(Arg.Any<long>()).Returns(account);
            var sut = new EditRolesModel(_mockIdamClient, _mockEmailService)
            {
                LaManager = true
            };
            //  Act
            var result = await sut.OnPost(AccountId);

            //  Assert
            Assert.IsType<RedirectToPageResult>(result);
            Assert.False(sut.HasValidationError);
            await _mockIdamClient.Received(1).UpdateClaim(Arg.Any<UpdateClaimDto>());
        }

        [Fact]
        public async Task OnPost_LaEmialNotificationIsSent()
        {
            //  Arrange
            const string email = "test@test.com";
            var account = new AccountDto
            {
                Email = email,
                Claims = [new AccountClaimDto { Name = "role", Value = "LaManager" }]
            };
            _mockIdamClient.GetAccountById(Arg.Any<long>()).Returns(account);
            await _mockEmailService.SendLaPermissionChangeEmail(Arg.Any<PermissionChangeNotificationModel>());

            var sut = new EditRolesModel(_mockIdamClient, _mockEmailService);
            //LaDualRole selected
            sut.LaManager = true;
            sut.LaProfessional = true;
            
            //  Act
            await sut.OnPost(AccountId);

            //  Assert            
            await _mockEmailService.Received(1).SendLaPermissionChangeEmail(
                Arg.Is<PermissionChangeNotificationModel>(x => 
                    x.EmailAddress == email && x.OldRole == "LaManager" && x.NewRole == "LaDualRole"));
        }

        [Fact]
        public async Task OnPost_VcsEmialNotificationIsSent()
        {
            //  Arrange
            const string email = "test@test.com";
            var account = new AccountDto
            {
                Email = email,
                Claims = [new AccountClaimDto { Name = "role", Value = "VcsManager" }]
            };
            _mockIdamClient.GetAccountById(Arg.Any<long>()).Returns(account);
            await _mockEmailService.SendLaPermissionChangeEmail(Arg.Any<PermissionChangeNotificationModel>());

            var sut = new EditRolesModel(_mockIdamClient, _mockEmailService)
            {
                VcsManager = true,
                VcsProfessional = true
            };

            //  Act
            await sut.OnPost(AccountId);

            //  Assert            
            await _mockEmailService.Received(1).SendVcsPermissionChangeEmail(
                Arg.Is<PermissionChangeNotificationModel>(x => x.EmailAddress == email && x.OldRole == "VcsManager" && x.NewRole == "VcsDualRole"));
        }
    }
}
