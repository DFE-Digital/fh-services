using AutoFixture;
using FamilyHubs.SharedKernel.GovLogin.Configuration;
using FamilyHubs.SharedKernel.Identity.Authentication.Gov;
using FamilyHubs.SharedKernel.Identity.Authorisation;
using FamilyHubs.SharedKernel.Identity.Authorisation.FamilyHubs;
using FamilyHubs.SharedKernel.Identity.Models;
using FamilyHubs.SharedKernel.UnitTests.Identity.TestHelpers;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using NSubstitute;

namespace FamilyHubs.SharedKernel.UnitTests.Identity.Authentication.Gov;

public class OidcServiceTests
{
    private readonly string _clientAssertion;
    private readonly Token _token;
    private readonly OpenIdConnectMessage _openIdConnectMessage;
    private readonly GovUkOidcConfiguration _oidcConfig;
    private readonly GovUkUser _user;
    private readonly string _accessToken;
    private readonly string _customClaimValue;
    private readonly List<ClaimsIdentity> _claimsIdentity;
    private readonly ILogger<OidcService> _mockedLogger;
    private readonly ISessionService _mockSessionService;

    public OidcServiceTests()
    {
        var fixture = new Fixture();
        _clientAssertion = fixture.Create<string>();
        _token = fixture.Create<Token>();
        _openIdConnectMessage = fixture.Create<OpenIdConnectMessage>();
        var configuration = FakeConfiguration.GetConfiguration();
        _oidcConfig = configuration.GetGovUkOidcConfiguration();
        _user = fixture.Create<GovUkUser>();
        _accessToken = fixture.Create<string>();
        _customClaimValue = fixture.Create<string>();

        _claimsIdentity = new List<ClaimsIdentity>();

        var claims = new List<Claim>();
        claims.Add(new Claim("sid", "1234"));
        claims.Add(new Claim(ClaimTypes.NameIdentifier, _user.Sub));
        _claimsIdentity.Add(new ClaimsIdentity(claims));

        _mockedLogger = Substitute.For<ILogger<OidcService>>();
        _mockSessionService = Substitute.For<ISessionService>();
    }

    [Fact]
    public async Task GetToken_TokenReturned()
    {
        //Arrange
        var response = new HttpResponseMessage
        {
            Content = new StringContent(JsonSerializer.Serialize(_token)),
            StatusCode = HttpStatusCode.Accepted
        };
        var expectedUrl = new Uri($"{_oidcConfig.Oidc.BaseUrl}/token");

        var client = new HttpClient(new TestHttpMessageHandler(response, expectedUrl, HttpMethod.Post));
        var jwtService = Substitute.For<IJwtSecurityTokenService>();
        jwtService.CreateToken(_oidcConfig.Oidc.ClientId, $"{_oidcConfig.Oidc.BaseUrl}/token",
                Arg.Is<ClaimsIdentity>(c => c.HasClaim("sub", _oidcConfig.Oidc.ClientId) && c.Claims.FirstOrDefault(f => f.Type.Equals("jti")) != null),
                Arg.Is<SigningCredentials>(c => c.Kid.Equals(_oidcConfig.Oidc.KeyVaultIdentifier) && c.Algorithm.Equals("RS512")))
            .Returns(_clientAssertion);
            
        var service = new OidcService(
            client, Substitute.For<IAzureIdentityService>(), jwtService, _oidcConfig, 
            Substitute.For<ICustomClaims>(), _mockSessionService, _mockedLogger);

        //Act
        var actual = await service.GetToken(_openIdConnectMessage);
        Assert.Equivalent(_token, actual);
    }
    
    [Fact]
    public async Task PopulateAccountClaims_TokenEndpointPrincipal_IsNull_ThenNotUpdated()
    {
        //Arrange
        var mockPrincipal = Substitute.For<ClaimsPrincipal>();
        mockPrincipal.Identities.Returns(new List<ClaimsIdentity>());
        var tokenValidatedContext = new TokenValidatedContext(
            new DefaultHttpContext(), new AuthenticationScheme(",", "", typeof(TestAuthHandler)),
            new OpenIdConnectOptions(), mockPrincipal, new AuthenticationProperties())
        {
            TokenEndpointResponse = new OpenIdConnectMessage
            {
                Parameters = { { "access_token", _accessToken } }
            },
            Principal = null
        };
        var service = new OidcService(
            Substitute.For<HttpClient>(), Substitute.For<IAzureIdentityService>(), Substitute.For<IJwtSecurityTokenService>(), 
            _oidcConfig, Substitute.For<ICustomClaims>(), _mockSessionService, _mockedLogger);

        //Act
        await service.PopulateAccountClaims(tokenValidatedContext);
        
        //Assert
        Assert.Null(tokenValidatedContext.Principal);
    }

    [Fact]
    public async Task PopulateAccountClaims_TokenEndpointResponse_IsNull_ThenNotUpdated()
    {
        //Arrange
        var mockPrincipal = Substitute.For<ClaimsPrincipal>();
        var tokenValidatedContext = new TokenValidatedContext(
            new DefaultHttpContext(), new AuthenticationScheme(",", "", typeof(TestAuthHandler)),
            new OpenIdConnectOptions(), Substitute.For<ClaimsPrincipal>(), new AuthenticationProperties())
        {
            Principal = mockPrincipal
        };
        var service = new OidcService(
            Substitute.For<HttpClient>(), Substitute.For<IAzureIdentityService>(), Substitute.For<IJwtSecurityTokenService>(), 
            _oidcConfig, Substitute.For<ICustomClaims>(), _mockSessionService, _mockedLogger);

        //Act
        await service.PopulateAccountClaims(tokenValidatedContext);

        //Assert
        Assert.Empty(tokenValidatedContext.Principal.Claims);
    }

    [Fact]
    public async Task PopulateAccountClaims_UserEndpoint_IsCalled_With_AccessToken()
    {
        //Arrange
        var mockPrincipal = Substitute.For<ClaimsPrincipal>();
        mockPrincipal.Identities.Returns(_claimsIdentity);
        var response = new HttpResponseMessage
        {
            Content = new StringContent(JsonSerializer.Serialize(_user)),
            StatusCode = HttpStatusCode.Accepted
        };
        var expectedUrl = new Uri($"{_oidcConfig.Oidc.BaseUrl}/userinfo");
        var client = new HttpClient(new TestHttpMessageHandler(response, expectedUrl, HttpMethod.Get));
        var tokenValidatedContext = new TokenValidatedContext(
            new DefaultHttpContext(), new AuthenticationScheme(",", "", typeof(TestAuthHandler)),
            new OpenIdConnectOptions(), mockPrincipal, new AuthenticationProperties())
        {
            TokenEndpointResponse = new OpenIdConnectMessage
            {
                Parameters = { { "access_token", _accessToken } }
            },
            Principal = mockPrincipal
        };

        var service = new OidcService(
            client, Substitute.For<IAzureIdentityService>(), Substitute.For<IJwtSecurityTokenService>(), _oidcConfig, 
            Substitute.For<ICustomClaims>(), _mockSessionService, _mockedLogger);

        //Act
        await service.PopulateAccountClaims(tokenValidatedContext);

        //Assert
        Assert.Equivalent(mockPrincipal, tokenValidatedContext.Principal, true);
    }

    [Fact]
    public async Task PopulateAccountClaims_UserEndpoint_IsCalled_EmailClaimPopulated()
    {
        //Arrange
        var mockPrincipal = Substitute.For<ClaimsPrincipal>();
        mockPrincipal.Identities.Returns(_claimsIdentity);
        var response = new HttpResponseMessage
        {
            Content = new StringContent(JsonSerializer.Serialize(_user)),
            StatusCode = HttpStatusCode.Accepted
        };
        var expectedUrl = new Uri($"{_oidcConfig.Oidc.BaseUrl}/userinfo");
        var client = new HttpClient(new TestHttpMessageHandler(response, expectedUrl, HttpMethod.Get));
        var tokenValidatedContext = new TokenValidatedContext(
            new DefaultHttpContext(), new AuthenticationScheme(",", "", typeof(TestAuthHandler)),
            new OpenIdConnectOptions(), mockPrincipal, new AuthenticationProperties())
        {
            TokenEndpointResponse = new OpenIdConnectMessage
            {
                Parameters = { { "access_token", _accessToken } }
            },
            Principal = mockPrincipal
        };

        var service = new OidcService(
            client, Substitute.For<IAzureIdentityService>(), Substitute.For<IJwtSecurityTokenService>(), 
            _oidcConfig, Substitute.For<ICustomClaims>(), _mockSessionService, _mockedLogger);

        //Act
        await service.PopulateAccountClaims(tokenValidatedContext);

        //Assert
        tokenValidatedContext.Principal.Identities.First().Claims.First(c => c.Type.Equals(ClaimTypes.Email)).Value.Should()
            .Be(_user.Email);
    }

    [Fact]
    public async Task PopulateAccountClaims_UserEndpoint_IsCalled_AdditionalClaimsPopulatedFromFunction()
    {
        //Arrange
        var mockPrincipal = Substitute.For<ClaimsPrincipal>();
        mockPrincipal.Identities.Returns(_claimsIdentity);
        var response = new HttpResponseMessage
        {
            Content = new StringContent(JsonSerializer.Serialize(_user)),
            StatusCode = HttpStatusCode.Accepted
        };
        var expectedUrl = new Uri($"{_oidcConfig.Oidc.BaseUrl}/userinfo");
        var client = new HttpClient(new TestHttpMessageHandler(response, expectedUrl, HttpMethod.Get));
        var tokenValidatedContext = new TokenValidatedContext(
            new DefaultHttpContext(), new AuthenticationScheme(",", "", typeof(TestAuthHandler)),
            new OpenIdConnectOptions(), mockPrincipal, new AuthenticationProperties())
        {
            TokenEndpointResponse = new OpenIdConnectMessage
            {
                Parameters = { { "access_token", _accessToken } }
            },
            Principal = mockPrincipal
        };
        var customClaims = Substitute.For<ICustomClaims>();
        customClaims.GetClaims(tokenValidatedContext)
            .Returns(new List<Claim> { new("CustomClaim", _customClaimValue) });

        var service = new OidcService(
            client, Substitute.For<IAzureIdentityService>(), Substitute.For<IJwtSecurityTokenService>(), 
            _oidcConfig, customClaims, _mockSessionService, _mockedLogger);

        //Act
        await service.PopulateAccountClaims(tokenValidatedContext);

        //Assert
        tokenValidatedContext.Principal.Identities.First().Claims.First(c => c.Type.Equals(ClaimTypes.Email)).Value.Should()
            .Be(_user.Email);
        tokenValidatedContext.Principal.Identities.First().Claims.First(c => c.Type.Equals("CustomClaim")).Value.Should()
            .Be(_customClaimValue);
    }

    private class TestAuthHandler : IAuthenticationHandler
    {
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticateResult> AuthenticateAsync()
        {
            throw new NotImplementedException();
        }

        public Task ChallengeAsync(AuthenticationProperties? properties)
        {
            throw new NotImplementedException();
        }

        public Task ForbidAsync(AuthenticationProperties? properties)
        {
            throw new NotImplementedException();
        }
    }
}