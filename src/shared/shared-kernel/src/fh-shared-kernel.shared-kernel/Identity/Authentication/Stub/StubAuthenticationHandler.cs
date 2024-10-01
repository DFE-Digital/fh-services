using FamilyHubs.SharedKernel.GovLogin.Configuration;
using FamilyHubs.SharedKernel.Identity.Authorisation;
using FamilyHubs.SharedKernel.Identity.Exceptions;
using FamilyHubs.SharedKernel.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Web;

namespace FamilyHubs.SharedKernel.Identity.Authentication.Stub
{
    internal class StubAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ICustomClaims _customClaims;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GovUkOidcConfiguration _configuration;

        public StubAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ICustomClaims customClaims,
            IHttpContextAccessor httpContextAccessor,
            GovUkOidcConfiguration configuration) : base(options, logger, encoder)
        {
            _customClaims = customClaims;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            var redirect = HttpUtility.UrlEncode($"{request.Path}{request.QueryString}");
            var stubLoginPage = $"{_configuration.AppHost}{StubConstants.LoginPagePath}{redirect}";

            _httpContextAccessor.HttpContext!.Response.Redirect(stubLoginPage);

            return Task.CompletedTask;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (string.IsNullOrWhiteSpace(_configuration.CookieName))
                throw new AuthConfigurationException($"CookieName is not configured in {nameof(GovUkOidcConfiguration)} section of appsettings");

            var cookieJson = _httpContextAccessor.HttpContext!.Request.Cookies[_configuration.CookieName];

            if (!TryGetClaimsFromCookie(cookieJson, out var claims))
            {
                return AuthenticateResult.Fail("Cookie not found");
            }

            var identity = new ClaimsIdentity(claims, "Employer-stub");
            var principal = new ClaimsPrincipal(identity);

            if (_customClaims != null)
            {
                var additionalClaims = await _customClaims.GetClaims(new TokenValidatedContext(_httpContextAccessor.HttpContext, Scheme, new OpenIdConnectOptions(), principal, new AuthenticationProperties()));
                claims.AddRange(additionalClaims);
                principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Employer-stub"));
            }

            var ticket = new AuthenticationTicket(principal, "Employer-stub");

            var result = AuthenticateResult.Success(ticket);

            return result;
        }

        private static bool TryGetClaimsFromCookie(string? cookieJson, out List<Claim> claims)
        {
            claims = new List<Claim>();

            if (string.IsNullOrEmpty(cookieJson))
            {
                return false;
            }

            var cookieClaims = JsonConvert.DeserializeObject<StubClaimsCookie>(cookieJson);

            if (cookieClaims == null || cookieClaims.Claims == null)
            {
                return false;
            }

            foreach (var claim in cookieClaims.Claims)
            {
                if (!string.IsNullOrEmpty(claim.Name) && claim.Value != null)
                {
                    claims.Add(new Claim(claim.Name, claim.Value));
                }
            }

            return true;
        }
    }
}
