using FamilyHubs.SharedKernel.GovLogin.Configuration;
using FamilyHubs.SharedKernel.Identity.Authentication.Gov;
using FamilyHubs.SharedKernel.Identity.Authorisation.FamilyHubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Security.Claims;
using System.Security.Principal;
using FamilyHubs.SharedKernel.Identity;

namespace FamilyHubs.SharedKernel.UnitTests.Identity.Authentication.Gov;

public class AccountMiddlewareTests
{
    private readonly GovUkOidcConfiguration _configuration;
    private readonly RequestDelegate _nextMock;
    private readonly ILogger<AccountMiddleware> _mockedLogger;
    private readonly ISessionService _mockSessionService;

    public AccountMiddlewareTests()
    {
        _configuration = new GovUkOidcConfiguration { Oidc = new Oidc() , Urls = new Urls()};
        _configuration.BearerTokenSigningKey = Guid.NewGuid().ToString();
        _configuration.Oidc.PrivateKey = Guid.NewGuid().ToString();
        _nextMock = Substitute.For<RequestDelegate>();
        _mockedLogger = Substitute.For<ILogger<AccountMiddleware>>();
        _mockSessionService = Substitute.For<ISessionService>();
    }

    [Fact]
    public async Task InvokeAsync_UserNull_NoTokenSet()
    {
        //  Arrange
        var mockContext = CreateMockHttpContext();
        var accountMiddleware = new AccountMiddleware(_nextMock, _configuration, _mockSessionService, _mockedLogger);

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
        var mockContext = CreateMockHttpContext();
        var claimsPrincipal = CreateUser(false);
        mockContext.User.Returns(claimsPrincipal);
        var accountMiddleware = new AccountMiddleware(_nextMock, _configuration, _mockSessionService, _mockedLogger);

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
        var mockContext = CreateMockHttpContext();
        var claimsPrincipal = CreateUser(true);
        mockContext.User.Returns(claimsPrincipal);
        var accountMiddleware = new AccountMiddleware(_nextMock, _configuration, _mockSessionService, _mockedLogger);

        //  Act
        await accountMiddleware.InvokeAsync(mockContext);

        //  Assert
        var bearerToken = mockContext.GetBearerToken();
        Assert.False(string.IsNullOrEmpty(bearerToken));
    }

    private HttpContext CreateMockHttpContext()
    {
        var mockHttpContext = Substitute.For<HttpContext>();

        var features = Substitute.For<IFeatureCollection>();
        var endpointFeature = Substitute.For<IEndpointFeature>();

        endpointFeature.Endpoint.Returns((Endpoint?)null);

        features.Get<IEndpointFeature>().Returns(endpointFeature);
        mockHttpContext.Features.Returns(features);

        var items = new Dictionary<object, object?>();
        mockHttpContext.Items.Returns(items);

        var request = Substitute.For<HttpRequest>();
        request.Path.Returns(new PathString("/somepath/action"));
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