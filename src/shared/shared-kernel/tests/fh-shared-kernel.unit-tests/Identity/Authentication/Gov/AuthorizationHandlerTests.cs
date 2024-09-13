using AutoFixture;
using FamilyHubs.SharedKernel.GovLogin.Configuration;
using FamilyHubs.SharedKernel.Identity.Authentication.Gov;
using FamilyHubs.SharedKernel.UnitTests.Identity.TestHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FamilyHubs.SharedKernel.UnitTests.Identity.Authentication.Gov
{
    public class AuthorizationHandlerTests
    {
        private string _role;
        private AccountActiveRequirement _requirement;
        private AuthorizationHandler _authorizationHandler;
        private GovUkOidcConfiguration _configuration;

        public AuthorizationHandlerTests()
        {
            var fixture = new Fixture();
            _role = fixture.Create<string>();
            _requirement = new AccountActiveRequirement();
            _configuration = FakeConfiguration.GetOidcConfiguration();
            _authorizationHandler = new AuthorizationHandler(_configuration);
        }

        [Fact]
        public async Task HandleAsync_IfClaimDoesNotExist_ThenSucceeds()
        {
            //Arrange
            var httpContextBase = new Mock<HttpContext>();
            var response = new Mock<HttpResponse>();
            httpContextBase.Setup(c => c.Response).Returns(response.Object);
            var claim = new Claim("AccountSuspended", "true");
            var claimsPrinciple = new ClaimsPrincipal(new[] { new ClaimsIdentity(new[] { claim }) });
            var context = new AuthorizationHandlerContext(new[] { _requirement }, claimsPrinciple, httpContextBase.Object);

            //Act
            await _authorizationHandler.HandleAsync(context);

            //Assert
            Assert.True(context.HasSucceeded);
            response.Verify(x => x.Redirect("/Errors/AccountSuspended"), Times.Never);
        }

        [Fact]
        public async Task HandleAsync_IfClaimExists_And_NotSuspended_ThenSucceeds()
        {
            //Arrange
            var httpContextBase = new Mock<HttpContext>();
            var response = new Mock<HttpResponse>();
            httpContextBase.Setup(c => c.Response).Returns(response.Object);
            var claim = new Claim(ClaimTypes.AuthorizationDecision, "active");
            var claimsPrinciple = new ClaimsPrincipal(new[] { new ClaimsIdentity(new[] { claim }) });
            var context = new AuthorizationHandlerContext(new[] { _requirement }, claimsPrinciple, httpContextBase.Object);

            //Act
            await _authorizationHandler.HandleAsync(context);

            //Assert
            Assert.True(context.HasSucceeded);
            response.Verify(x => x.Redirect("/Errors/AccountSuspended"), Times.Never);
        }

        [Fact]
        public async Task HandleAsync_IfClaimExists_And_IsSuspended_ThenRedirects()
        {
            //Arrange

            var httpContextBase = new Mock<HttpContext>();
            var response = new Mock<HttpResponse>();
            httpContextBase.Setup(c => c.Response).Returns(response.Object);
            var claim = new Claim(ClaimTypes.AuthorizationDecision, "sUsPended");
            var claimsPrinciple = new ClaimsPrincipal(new[] { new ClaimsIdentity(new[] { claim }) });
            var context = new AuthorizationHandlerContext(new[] { _requirement }, claimsPrinciple, httpContextBase.Object);

            //Act
            await _authorizationHandler.HandleAsync(context);

            //Assert
            Assert.True(context.HasSucceeded);
            response.Verify(x => x.Redirect("https://familyhubs-test.com/service/account-unavailable"));
        }
    }
}
