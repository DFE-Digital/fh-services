using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.AccountAdmin.Pages.ManagePermissions;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.AccountAdmin.ManagePermissions
{
    public class EditEmailChangedTests
    {
        private readonly IIdamClient _mockIdamClient;

        public EditEmailChangedTests()
        {
            _mockIdamClient = Substitute.For<IIdamClient>();
        }

        [Fact]
        public async Task OnGet_ReturnsUpdatedUserName()
        {
            //  Arrange
            const int accountId = 1234;
            const string userName = "UsersName";
            var account = new AccountDto { Id = accountId, Email = "email", Name = userName };
            _mockIdamClient.GetAccountById(1234).Returns(Task.FromResult<AccountDto?>(account));

            var sut = new EditEmailChangedConfirmationModel(_mockIdamClient) { AccountId = accountId.ToString() };

            //  Act
            await sut.OnGet();

            //  Assert
            Assert.Equal(userName, sut.UserName);
        }
    }
}
