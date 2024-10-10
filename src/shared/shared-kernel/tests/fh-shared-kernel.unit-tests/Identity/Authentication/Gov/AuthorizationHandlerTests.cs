using FamilyHubs.SharedKernel.Identity.Authentication.Gov;
using FamilyHubs.SharedKernel.UnitTests.Identity.TestHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Security.Claims;

namespace FamilyHubs.SharedKernel.UnitTests.Identity.Authentication.Gov;

public class AuthorizationHandlerTests
{
    private readonly AccountActiveRequirement _requirement;
    private readonly AuthorizationHandler _authorizationHandler;
        
    public AuthorizationHandlerTests()
    {
        _requirement = new AccountActiveRequirement();
        var configuration = FakeConfiguration.GetOidcConfiguration();
        _authorizationHandler = new AuthorizationHandler(configuration);
    }

    [Fact]
    public async Task HandleAsync_IfClaimDoesNotExist_ThenSucceeds()
    {
        //Arrange
        var httpContextBase = Substitute.For<HttpContext>();
        var response = Substitute.For<HttpResponse>();
        httpContextBase.Response.Returns(response);
        var claim = new Claim("AccountSuspended", "true");
        var claimsPrinciple = new ClaimsPrincipal([new ClaimsIdentity([claim])]);
        var context = new AuthorizationHandlerContext([_requirement], claimsPrinciple, httpContextBase);

        //Act
        await _authorizationHandler.HandleAsync(context);

        //Assert
        Assert.True(context.HasSucceeded);
        response.DidNotReceive().Redirect("/Errors/AccountSuspended");
    }

    [Fact]
    public async Task HandleAsync_IfClaimExists_And_NotSuspended_ThenSucceeds()
    {
        //Arrange
        var httpContextBase = Substitute.For<HttpContext>();
        var response = Substitute.For<HttpResponse>();
        httpContextBase.Response.Returns(response);
        var claim = new Claim(ClaimTypes.AuthorizationDecision, "active");
        var claimsPrinciple = new ClaimsPrincipal([new ClaimsIdentity([claim])]);
        var context = new AuthorizationHandlerContext([_requirement], claimsPrinciple, httpContextBase);

        //Act
        await _authorizationHandler.HandleAsync(context);

        //Assert
        Assert.True(context.HasSucceeded);
        response.DidNotReceive().Redirect("/Errors/AccountSuspended");
    }

    [Fact]
    public async Task HandleAsync_IfClaimExists_And_IsSuspended_ThenRedirects()
    {
        //Arrange

        var httpContextBase = Substitute.For<HttpContext>();
        var response = Substitute.For<HttpResponse>();
        httpContextBase.Response.Returns(response);
        var claim = new Claim(ClaimTypes.AuthorizationDecision, "sUsPended");
        var claimsPrinciple = new ClaimsPrincipal([new ClaimsIdentity([claim])]);
        var context = new AuthorizationHandlerContext([_requirement], claimsPrinciple, httpContextBase);

        //Act
        await _authorizationHandler.HandleAsync(context);

        //Assert
        Assert.True(context.HasSucceeded);
        response.Received().Redirect("https://familyhubs-test.com/service/account-unavailable");
    }
}