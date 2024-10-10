using System.Security.Claims;
using System.Security.Principal;
using FamilyHubs.SharedKernel.GovLogin.Configuration;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Identity.Authentication.Stub;
using Microsoft.AspNetCore.Http;
using NSubstitute;

namespace FamilyHubs.SharedKernel.UnitTests.Identity.Authentication.Stub;

public class StubAccountMiddlewareTests
{
    private readonly GovUkOidcConfiguration _configuration;
    private readonly RequestDelegate _nextMock;
    private const string RequestPath = "/somepath/action";

    public StubAccountMiddlewareTests()
    {
        _configuration = new GovUkOidcConfiguration { Oidc = new Oidc() };
        _configuration.Oidc.PrivateKey = Guid.NewGuid().ToString();
        _configuration.BearerTokenSigningKey = Guid.NewGuid().ToString();
        _nextMock = Substitute.For<RequestDelegate>();
    }

    [Fact]
    public async Task InvokeAsync_UserNull_NoTokenSet()
    {
        //  Arrange
        var mockContext = CreateMockHttpContext(RequestPath);
        var accountMiddleware = new StubAccountMiddleware(_nextMock, _configuration);

        //  Act
        await accountMiddleware.InvokeAsync(mockContext);

        //  Assert
        var bearerToken = mockContext.GetBearerToken();
        Assert.True(string.IsNullOrEmpty(bearerToken));
    }

    [Fact]
    public async Task InvokeAsync_UserNotAuthenticated_NoTokenSet()
    {
        //  Arrange
        var mockContext = CreateMockHttpContext(RequestPath);
        var claimsPrincipal = CreateUser(false);
        mockContext.User.Returns(claimsPrincipal);
        var accountMiddleware = new StubAccountMiddleware(_nextMock, _configuration);

        //  Act
        await accountMiddleware.InvokeAsync(mockContext);

        //  Assert
        var bearerToken = mockContext.GetBearerToken();
        Assert.True(string.IsNullOrEmpty(bearerToken));
    }

    [Fact]
    public async Task InvokeAsync_SetsBearerToken()
    {
        //  Arrange
        var mockContext = CreateMockHttpContext(RequestPath);
        var claimsPrincipal = CreateUser(true);
        mockContext.User.Returns(claimsPrincipal);
        var accountMiddleware = new StubAccountMiddleware(_nextMock, _configuration);

        //  Act
        await accountMiddleware.InvokeAsync(mockContext);

        //  Assert
        var bearerToken = mockContext.GetBearerToken();
        Assert.False(string.IsNullOrEmpty(bearerToken));
    }

    private HttpContext CreateMockHttpContext(string requestPath)
    {
        var mockHttpContext = Substitute.For<HttpContext>();

        var items = new Dictionary<object, object?>();
        mockHttpContext.Items.Returns(items);

        var request = Substitute.For<HttpRequest>();
        request.Path.Returns(new PathString(requestPath));
        mockHttpContext.Request.Returns(request);
        return mockHttpContext;
    }
        
    private ClaimsPrincipal CreateUser(bool isAuthenticated)
    {
        var mockUser = Substitute.For<ClaimsPrincipal>();
        var mockIdentity = Substitute.For<IIdentity>();

        mockIdentity.IsAuthenticated.Returns(isAuthenticated);
        mockUser.Identity.Returns(mockIdentity);

        return mockUser;
    }
}