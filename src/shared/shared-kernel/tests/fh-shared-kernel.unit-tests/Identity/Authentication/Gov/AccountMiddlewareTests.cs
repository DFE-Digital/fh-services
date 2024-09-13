using System;
using System.Collections.Generic;
using FamilyHubs.SharedKernel.GovLogin.Configuration;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Identity.Authentication.Gov;
using FamilyHubs.SharedKernel.Identity.Authorisation.FamilyHubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FamilyHubs.SharedKernel.UnitTests.Identity.Authentication.Gov
{
    public class AccountMiddlewareTests
    {
        private readonly GovUkOidcConfiguration _configuration;
        private readonly RequestDelegate _nextMock;
        private readonly ILogger<AccountMiddleware> _mockedLogger;
        private readonly Mock<ISessionService> _mockSessionService;

        public AccountMiddlewareTests()
        {
            _configuration = new GovUkOidcConfiguration { Oidc = new Oidc() , Urls = new Urls()};
            _configuration.BearerTokenSigningKey = Guid.NewGuid().ToString();
            _configuration.Oidc.PrivateKey = Guid.NewGuid().ToString();
            _nextMock = Mock.Of<RequestDelegate>();
            _mockedLogger = Mock.Of<ILogger<AccountMiddleware>>();
            _mockSessionService = new Mock<ISessionService>();
        }

        [Fact]
        public async Task InvokeAsync_UserNull_NoTokenSet()
        {
            //  Arrange
            var mockContext = CreateMockHttpContext();
            var context = mockContext.Object;
            var accountMiddleware = new AccountMiddleware(_nextMock, _configuration, _mockSessionService.Object, _mockedLogger);

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
            var mockContext = CreateMockHttpContext();
            mockContext.Setup(m => m.User).Returns(CreateUser(false));
            var context = mockContext.Object;
            var accountMiddleware = new AccountMiddleware(_nextMock, _configuration, _mockSessionService.Object, _mockedLogger);

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
            var mockContext = CreateMockHttpContext();
            mockContext.Setup(m => m.User).Returns(CreateUser(true));
            var context = mockContext.Object;
            var accountMiddleware = new AccountMiddleware(_nextMock, _configuration, _mockSessionService.Object, _mockedLogger);

            //  Act
            await accountMiddleware.InvokeAsync(context);

            //  Assert
            var bearerToken = context.GetBearerToken();
            Assert.False(string.IsNullOrEmpty(bearerToken));
        }

        private Mock<HttpContext> CreateMockHttpContext()
        {
            var mockHttpContext = new Mock<HttpContext>();

            var features = new Mock<IFeatureCollection>();
            var endpointFeature = new Mock<IEndpointFeature>();

            endpointFeature.Setup(ef => ef.Endpoint).Returns((Endpoint?)null);

            features.Setup(f => f.Get<IEndpointFeature>())
                .Returns(endpointFeature.Object);
            mockHttpContext.Setup(c => c.Features).Returns(features.Object);

            var items = new Dictionary<object, object?>();
            mockHttpContext.Setup(m => m.Items).Returns(items);

            var request = new Mock<HttpRequest>();
            request.SetupGet(m => m.Path).Returns("/somepath/action");
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
