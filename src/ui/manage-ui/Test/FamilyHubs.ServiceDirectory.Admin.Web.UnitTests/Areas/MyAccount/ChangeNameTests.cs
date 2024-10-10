using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Admin.Core.Models;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.MyAccount.Pages;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.MyAccount
{
    public class ChangeNameTests
    {
        private readonly IIdamClient _mockIdamClient;
        
        public ChangeNameTests()
        {
            _mockIdamClient = Substitute.For<IIdamClient>();
        }

        [Fact]
        public void OnGet_Valid_SetFullNameFromCache()
        {
            //  Arrange           
            const string expectedName = "test name";

            var claims = new List<Claim> { new(FamilyHubsClaimTypes.FullName, expectedName) };
            var mockHttpContext = TestHelper.GetHttpContext(claims);

            var sut = new ChangeNameModel(_mockIdamClient)
            {
                PageContext = { HttpContext = mockHttpContext }
            };

            //  Act
            sut.OnGet();

            //  Assert
            Assert.Equal(expectedName, sut.FullName);
        }

        [Fact]
        public async Task OnPost_Valid_UpdateAccount()
        {
            //  Arrange
            var claims = new List<Claim> { 
                new(FamilyHubsClaimTypes.FullName, "oldName") ,
                new(FamilyHubsClaimTypes.AccountId, "1")
            };
            var mockHttpContext = TestHelper.GetHttpContext(claims);

            await _mockIdamClient.UpdateAccountSelfService(Arg.Any<UpdateAccountSelfServiceDto>(), Arg.Any<CancellationToken>());
            var sut = new ChangeNameModel(_mockIdamClient)
            {
                PageContext = { HttpContext = mockHttpContext },
                FullName = "newName"
            };

            //  Act
            var result = await sut.OnPost(CancellationToken.None);

            //  Assert
            await _mockIdamClient.Received(1).UpdateAccountSelfService(Arg.Any<UpdateAccountSelfServiceDto>(), Arg.Any<CancellationToken>());
            Assert.Equal("ChangeNameConfirmation", ((RedirectToPageResult)result).PageName);
        }

        [Fact]
        public async Task OnPost_NotValid_NoNameHasValidationError()
        {
            //  Arrange
            var claims = new List<Claim> {
                new(FamilyHubsClaimTypes.FullName, "oldName") ,
                new(FamilyHubsClaimTypes.AccountId, "1")
            };
            var mockHttpContext = TestHelper.GetHttpContext(claims);

            await _mockIdamClient.UpdateAccount(Arg.Any<UpdateAccountDto>(), Arg.Any<CancellationToken>());
            var sut = new ChangeNameModel(_mockIdamClient)
            {
                PageContext = { HttpContext = mockHttpContext },                
            };

            //  Act
            await sut.OnPost(CancellationToken.None);

            //  Assert
            Assert.True(sut.Errors.HasErrors);            
        }
    }
}
