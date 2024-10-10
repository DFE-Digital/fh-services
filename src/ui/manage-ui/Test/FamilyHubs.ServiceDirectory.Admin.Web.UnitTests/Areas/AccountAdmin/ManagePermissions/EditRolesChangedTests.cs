using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.AccountAdmin.Pages.ManagePermissions;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.AccountAdmin.ManagePermissions
{
    public class EditRolesChangedTests
    {
        private readonly IIdamClient _mockIdamClient;
        private const int AccountId = 1234;

        public EditRolesChangedTests()
        {
            _mockIdamClient = Substitute.For<IIdamClient>();
        }

        [Fact]
        public async Task OnGet_SetUserName()
        {
            //  Arrange
            const string userName = "UsersName";
            var account = new AccountDto { Id = AccountId, Email = "email", Name = userName };
            _mockIdamClient.GetAccountById(1234).Returns(Task.FromResult<AccountDto?>(account));

            var sut = new EditRolesChangedConfirmationModel(_mockIdamClient) { AccountId = AccountId.ToString() };

            //  Act
            await sut.OnGet();

            //  Assert
            Assert.Equal(userName, sut.UserName);
        }

    }
}
