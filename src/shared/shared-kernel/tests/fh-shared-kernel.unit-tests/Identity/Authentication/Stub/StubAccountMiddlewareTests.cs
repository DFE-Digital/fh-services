using System;
using System.Collections.Generic;
using FamilyHubs.SharedKernel.GovLogin.Configuration;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Identity.Authentication.Stub;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FamilyHubs.SharedKernel.UnitTests.Identity.Authentication.Gov
{
    public class StubAccountMiddlewareTests
    {
        private GovUkOidcConfiguration _configuration;
        private RequestDelegate _nextMock;
        private const string _requestPath = "/somepath/action";

        public StubAccountMiddlewareTests()
        {
            _configuration = new GovUkOidcConfiguration { Oidc = new Oidc() };
            _configuration.Oidc.PrivateKey = Guid.NewGuid().ToString();
            _configuration.BearerTokenSigningKey = Guid.NewGuid().ToString();
            _nextMock = Mock.Of<RequestDelegate>();
        }

        [Fact]
        public async Task InvokeAsync_UserNull_NoTokenSet()
        {
            //  Arrange
            var mockContext = CreateMockHttpContext(_requestPath);
            var context = mockContext.Object;
            var accountMiddleware = new StubAccountMiddleware(_nextMock, _configuration);

            //  Act
            await accountMiddleware.InvokeAsync(context);

            //  Assert
            var bearerToken = context.GetBearerToken();
            Assert.True(string.IsNullOrEmpty(bearerToken));
        }

        [Fact]
        public async Task InvokeAsync_UserNotAuthenticated_NoTokenSet()
        {
            //  Arrange
            var mockContext = CreateMockHttpContext(_requestPath);
            mockContext.Setup(m => m.User).Returns(CreateUser(false));
            var context = mockContext.Object;
            var accountMiddleware = new StubAccountMiddleware(_nextMock, _configuration);

            //  Act
            await accountMiddleware.InvokeAsync(context);

            //  Assert
            var bearerToken = context.GetBearerToken();
            Assert.True(string.IsNullOrEmpty(bearerToken));
        }

        [Fact]
        public async Task InvokeAsync_SetsBearerToken()
        {
            //  Arrange
            var mockContext = CreateMockHttpContext(_requestPath);
            mockContext.Setup(m => m.User).Returns(CreateUser(true));
            var context = mockContext.Object;
            var accountMiddleware = new StubAccountMiddleware(_nextMock, _configuration);

            //  Act
            await accountMiddleware.InvokeAsync(context);

            //  Assert
            var bearerToken = context.GetBearerToken();
            Assert.False(string.IsNullOrEmpty(bearerToken));
        }

        private Mock<HttpContext> CreateMockHttpContext(string requestPath)
        {
            var mockHttpContext = new Mock<HttpContext>();

            var items = new Dictionary<object, object?>();
            mockHttpContext.Setup(m => m.Items).Returns(items);

            var request = new Mock<HttpRequest>();
            request.SetupGet(m => m.Path).Returns(requestPath);
            mockHttpContext.Setup(m => m.Request).Returns(request.Object);
            return mockHttpContext;
        }
        
        private ClaimsPrincipal CreateUser(bool isAuthenticated)
        {
            var mockUser = new Mock<ClaimsPrincipal>();
            var mockIdentity = new Mock<IIdentity>();

            mockIdentity.Setup(m => m.IsAuthenticated).Returns(isAuthenticated);
            mockUser.Setup(m => m.Identity).Returns(mockIdentity.Object);

            return mockUser.Object;
        }
    }
}
