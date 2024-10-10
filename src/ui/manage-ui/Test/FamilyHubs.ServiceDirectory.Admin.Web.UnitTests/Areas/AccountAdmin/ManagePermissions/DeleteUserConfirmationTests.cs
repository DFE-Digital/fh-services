using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.AccountAdmin.Pages.ManagePermissions;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.AccountAdmin.ManagePermissions
{
    public class DeleteUserConfirmationTests
    {
        
        private readonly ICacheService _mockCacheService;
        public DeleteUserConfirmationTests()
        {
            _mockCacheService = Substitute.For<ICacheService>();
        }
            
        [Fact]
        public async Task OnGet_UserNameRetrievedFromCache()
        {
            //  Arrange            
            const string userName = "TestUser";
            _mockCacheService.RetrieveString("DeleteUserName").Returns(Task.FromResult(userName));
            var sut = new DeleteUserConfirmationModel( _mockCacheService);

            //  Act
            await sut.OnGet(isDeleted:true);

            //  Assert
            Assert.Equal(userName, sut.UserName);
        }               
    }
}
