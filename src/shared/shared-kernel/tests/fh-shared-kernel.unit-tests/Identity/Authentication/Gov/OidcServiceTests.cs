using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Moq.Protected;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FamilyHubs.SharedKernel.UnitTests.Identity.Authentication.Gov
{
    public class OidcServiceTests
    {
        private readonly string _clientAssertion;
        private readonly Token _token;
        private readonly OpenIdConnectMessage _openIdConnectMessage;
        private readonly IConfiguration _iConfiguration;
        private readonly GovUkOidcConfiguration _oidcConfig;
        private readonly GovUkUser _user;
        private readonly string _accessToken;
        private readonly string _customClaimValue;
        private readonly List<ClaimsIdentity> _claimsIdentity;
        private readonly ILogger<OidcService> _mockedLogger;
        private readonly Mock<ISessionService> _mockSessionService;

        public OidcServiceTests()
        {
            var fixture = new Fixture();
            _clientAssertion = fixture.Create<string>();
            _token = fixture.Create<Token>();
            _openIdConnectMessage = fixture.Create<OpenIdConnectMessage>();
            _iConfiguration = FakeConfiguration.GetConfiguration();
            _oidcConfig = _iConfiguration.GetGovUkOidcConfiguration();
            _user = fixture.Create<GovUkUser>();
            _accessToken = fixture.Create<string>();
            _customClaimValue = fixture.Create<string>();

            _claimsIdentity = new List<ClaimsIdentity>();

            var claims = new List<Claim>();
            claims.Add(new Claim("sid", "1234"));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, _user.Sub));
            _claimsIdentity.Add(new ClaimsIdentity(claims));

            _mockedLogger = Mock.Of<ILogger<OidcService>>();
            _mockSessionService = new Mock<ISessionService>();
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
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, expectedUrl, HttpMethod.Post);

            var client = new HttpClient(httpMessageHandler.Object);
            var jwtService = new Mock<IJwtSecurityTokenService>();
            jwtService.Setup(x => x.CreateToken(_oidcConfig.Oidc.ClientId, $"{_oidcConfig.Oidc.BaseUrl}/token",
                    It.Is<ClaimsIdentity>(c => c.HasClaim("sub", _oidcConfig.Oidc.ClientId) && c.Claims.FirstOrDefault(f => f.Type.Equals("jti")) != null),
                    It.Is<SigningCredentials>(c => c.Kid.Equals(_oidcConfig.Oidc.KeyVaultIdentifier) && c.Algorithm.Equals("RS512"))))
                .Returns(_clientAssertion);
            var service = new OidcService(client, Mock.Of<IAzureIdentityService>(), jwtService.Object, _oidcConfig, Mock.Of<ICustomClaims>(), _mockSessionService.Object, _mockedLogger);

            //Act
            var actual = await service.GetToken(_openIdConnectMessage);

            //Assert
            httpMessageHandler.Protected()
                .Verify<Task<HttpResponseMessage>>(
                    "SendAsync", Times.Once(),
                    ItExpr.Is<HttpRequestMessage>(c =>
                        c.Content != null
                        && c.RequestUri != null
                        && c.Method.Equals(HttpMethod.Post)
                        && c.RequestUri.Equals(expectedUrl)
                    ),
                    ItExpr.IsAny<CancellationToken>()
                );
            Assert.Equivalent(_token, actual);
        }

        //todo: this test can cause deadlocks
        // we'll need to read the content of the response first using async, and then rewind the stream
        // see https://stackoverflow.com/questions/43403941/how-to-read-asp-net-core-response-body
        // alternatively, we take everything out of Content first that the method invocation check requires
        //[Fact]
        //public async Task GetToken_OpenIdConnectMessage_Passed_AsFormEncodedContent()
        //{
        //    //Arrange
        //    var response = new HttpResponseMessage
        //    {
        //        Content = new StringContent(JsonSerializer.Serialize(_token)),
        //        StatusCode = HttpStatusCode.Accepted
        //    };

        //    var expectedUrl = new Uri($"{_oidcConfig.Oidc.BaseUrl}/token");
        //    var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, expectedUrl, HttpMethod.Post);
        //    var client = new HttpClient(httpMessageHandler.Object);
        //    var jwtService = new Mock<IJwtSecurityTokenService>();
        //    jwtService.Setup(x => x.CreateToken(_oidcConfig.Oidc.ClientId, $"{_oidcConfig.Oidc.BaseUrl}/token",
        //            It.Is<ClaimsIdentity>(c => c.HasClaim("sub", _oidcConfig.Oidc.ClientId) && c.Claims.FirstOrDefault(f => f.Type.Equals("jti")) != null),
        //            It.Is<SigningCredentials>(c => c.Kid.Equals(_oidcConfig.Oidc.KeyVaultIdentifier) && c.Algorithm.Equals("RS512"))))
        //        .Returns(_clientAssertion);
        //    var service = new OidcService(client, Mock.Of<IAzureIdentityService>(), jwtService.Object, _oidcConfig, Mock.Of<ICustomClaims>(), _mockSessionService.Object, _mockedLogger);

        //    //Act
        //    await service.GetToken(_openIdConnectMessage);

        //    //Assert
        //    httpMessageHandler.Protected()
        //        .Verify<Task<HttpResponseMessage>>(
        //            "SendAsync", Times.Once(),
        //            ItExpr.Is<HttpRequestMessage>(c =>
        //                c.Content != null
        //                && c.Headers.Accept.Any(x => x.MediaType != null && x.MediaType.Equals("application/x-www-form-urlencoded"))
        //                && c.Headers.Accept.Any(x => x.MediaType != null && x.MediaType.Equals("*/*"))
        //                && c.Headers.UserAgent.FirstOrDefault(x =>
        //                    x.Product != null && x.Product.Version != null && x.Product.Name.Equals("DfE") && x.Product.Version.Equals("1")) != null
        //                && c.Content.Headers.Count() == 1
        //                && c.Content.Headers.Any(x => x.Key.Equals("Content-Type") && x.Value.First().Equals("application/x-www-form-urlencoded"))
        //                && c.Content.ReadAsStringAsync().Result.Contains("grant_type=authorization_code")
        //                && c.Content.ReadAsStringAsync().Result.Contains("client_assertion_type=urn%3Aietf%3Aparams%3Aoauth%3Aclient-assertion-type%3Ajwt-bearer")
        //                && c.Content.ReadAsStringAsync().Result.Contains($"redirect_uri={_openIdConnectMessage.RedirectUri}", StringComparison.CurrentCultureIgnoreCase)
        //                && c.Content.ReadAsStringAsync().Result.Contains($"code={_openIdConnectMessage.Code}", StringComparison.CurrentCultureIgnoreCase)
        //                && c.Content.ReadAsStringAsync().Result.Contains($"client_assertion={_clientAssertion}", StringComparison.CurrentCultureIgnoreCase)
        //    ),
        //            ItExpr.IsAny<CancellationToken>()
        //        );
        //}

        [Fact]
        public async Task PopulateAccountClaims_TokenEndpointPrincipal_IsNull_ThenNotUpdated()
        {
            //Arrange
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonSerializer.Serialize(_user)),
                StatusCode = HttpStatusCode.Accepted
            };
            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.Setup(x => x.Identities).Returns(new List<ClaimsIdentity>());
            var expectedUrl = new Uri($"{_oidcConfig.Oidc.BaseUrl}/userinfo");
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, expectedUrl, HttpMethod.Get);
            var tokenValidatedContext = new TokenValidatedContext(new DefaultHttpContext(), new AuthenticationScheme(",", "", typeof(TestAuthHandler)),
                new OpenIdConnectOptions(), mockPrincipal.Object, new AuthenticationProperties())
            {
                TokenEndpointResponse = new OpenIdConnectMessage
                {
                    Parameters = { { "access_token", _accessToken } }
                },
                Principal = null
            };
            var service = new OidcService(Mock.Of<HttpClient>(), Mock.Of<IAzureIdentityService>(), Mock.Of<IJwtSecurityTokenService>(), _oidcConfig, Mock.Of<ICustomClaims>(), _mockSessionService.Object, _mockedLogger);

            //Act
            await service.PopulateAccountClaims(tokenValidatedContext);

            //Assert
            httpMessageHandler.Protected()
                .Verify<Task<HttpResponseMessage>>(
                    "SendAsync", Times.Never(),
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                );
        }

        [Fact]
        public async Task PopulateAccountClaims_TokenEndpointResponse_IsNull_ThenNotUpdated()
        {
            //Arrange
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonSerializer.Serialize(_user)),
                StatusCode = HttpStatusCode.Accepted
            };
            var mockPrincipal = new Mock<ClaimsPrincipal>();
            var expectedUrl = new Uri($"{_oidcConfig.Oidc.BaseUrl}/userinfo");
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, expectedUrl, HttpMethod.Get);
            var tokenValidatedContext = new TokenValidatedContext(new DefaultHttpContext(), new AuthenticationScheme(",", "", typeof(TestAuthHandler)),
                new OpenIdConnectOptions(), Mock.Of<ClaimsPrincipal>(), new AuthenticationProperties())
            {
                Principal = mockPrincipal.Object
            };
            var service = new OidcService(Mock.Of<HttpClient>(), Mock.Of<IAzureIdentityService>(), Mock.Of<IJwtSecurityTokenService>(), _oidcConfig, Mock.Of<ICustomClaims>(), _mockSessionService.Object, _mockedLogger);

            //Act
            await service.PopulateAccountClaims(tokenValidatedContext);

            //Assert
            httpMessageHandler.Protected()
                .Verify<Task<HttpResponseMessage>>(
                    "SendAsync", Times.Never(),
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                );
        }

        [Fact]
        public async Task PopulateAccountClaims_UserEndpoint_IsCalled_With_AccessToken()
        {
            //Arrange
            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.Setup(x => x.Identities).Returns(_claimsIdentity);
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonSerializer.Serialize(_user)),
                StatusCode = HttpStatusCode.Accepted
            };
            var expectedUrl = new Uri($"{_oidcConfig.Oidc.BaseUrl}/userinfo");
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, expectedUrl, HttpMethod.Get);
            var client = new HttpClient(httpMessageHandler.Object);
            var tokenValidatedContext = new TokenValidatedContext(new DefaultHttpContext(), new AuthenticationScheme(",", "", typeof(TestAuthHandler)),
                new OpenIdConnectOptions(), mockPrincipal.Object, new AuthenticationProperties())
            {
                TokenEndpointResponse = new OpenIdConnectMessage
                {
                    Parameters = { { "access_token", _accessToken } }
                },
                Principal = mockPrincipal.Object
            };

            var service = new OidcService(client, Mock.Of<IAzureIdentityService>(), Mock.Of<IJwtSecurityTokenService>(), _oidcConfig, Mock.Of<ICustomClaims>(), _mockSessionService.Object, _mockedLogger);

            //Act
            await service.PopulateAccountClaims(tokenValidatedContext);

            //Assert
            httpMessageHandler.Protected()
                .Verify<Task<HttpResponseMessage>>(
                    "SendAsync", Times.Once(),
                    ItExpr.Is<HttpRequestMessage>(c =>
                        c.Headers.Authorization != null
                        && c.Headers.Authorization.Parameter != null
                        && c.Headers.Authorization.Scheme.Equals("Bearer")
                        && c.Headers.Authorization.Parameter.Equals(_accessToken)
                    ),
                    ItExpr.IsAny<CancellationToken>()
                );
        }

        [Fact]
        public async Task PopulateAccountClaims_UserEndpoint_IsCalled_EmailClaimPopulated()
        {
            //Arrange
            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.Setup(x => x.Identities).Returns(_claimsIdentity);
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonSerializer.Serialize(_user)),
                StatusCode = HttpStatusCode.Accepted
            };
            var expectedUrl = new Uri($"{_oidcConfig.Oidc.BaseUrl}/userinfo");
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, expectedUrl, HttpMethod.Get);
            var client = new HttpClient(httpMessageHandler.Object);
            var tokenValidatedContext = new TokenValidatedContext(new DefaultHttpContext(), new AuthenticationScheme(",", "", typeof(TestAuthHandler)),
                new OpenIdConnectOptions(), mockPrincipal.Object, new AuthenticationProperties())
            {
                TokenEndpointResponse = new OpenIdConnectMessage
                {
                    Parameters = { { "access_token", _accessToken } }
                },
                Principal = mockPrincipal.Object
            };

            var service = new OidcService(client, Mock.Of<IAzureIdentityService>(), Mock.Of<IJwtSecurityTokenService>(), _oidcConfig, Mock.Of<ICustomClaims>(), _mockSessionService.Object, _mockedLogger);

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
            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.Setup(x => x.Identities).Returns(_claimsIdentity);
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonSerializer.Serialize(_user)),
                StatusCode = HttpStatusCode.Accepted
            };
            var expectedUrl = new Uri($"{_oidcConfig.Oidc.BaseUrl}/userinfo");
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, expectedUrl, HttpMethod.Get);
            var client = new HttpClient(httpMessageHandler.Object);
            var tokenValidatedContext = new TokenValidatedContext(new DefaultHttpContext(), new AuthenticationScheme(",", "", typeof(TestAuthHandler)),
                new OpenIdConnectOptions(), mockPrincipal.Object, new AuthenticationProperties())
            {
                TokenEndpointResponse = new OpenIdConnectMessage
                {
                    Parameters = { { "access_token", _accessToken } }
                },
                Principal = mockPrincipal.Object
            };
            var customClaims = new Mock<ICustomClaims>();
            customClaims.Setup(x => x.GetClaims(tokenValidatedContext))
                .ReturnsAsync(new List<Claim> { new Claim("CustomClaim", _customClaimValue) });

            var service = new OidcService(client, Mock.Of<IAzureIdentityService>(), Mock.Of<IJwtSecurityTokenService>(), _oidcConfig, customClaims.Object, _mockSessionService.Object, _mockedLogger);

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

}
