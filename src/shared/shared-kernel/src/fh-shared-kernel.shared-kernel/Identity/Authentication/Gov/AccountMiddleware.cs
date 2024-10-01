using FamilyHubs.SharedKernel.GovLogin.Configuration;
using FamilyHubs.SharedKernel.Identity.Authorisation.FamilyHubs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Web;

namespace FamilyHubs.SharedKernel.Identity.Authentication.Gov
{
    public class AccountMiddleware : AccountMiddlewareBase
    {
        private readonly RequestDelegate _next;
        private readonly GovUkOidcConfiguration _configuration;
        private readonly ISessionService _sessionService;
        private readonly ILogger<AccountMiddleware> _logger;

        public AccountMiddleware(
            RequestDelegate next,
            GovUkOidcConfiguration configuration,
            ISessionService sessionService,
            ILogger<AccountMiddleware> logger) : base(configuration)
        {
            _next = next;
            _configuration = configuration;
            _sessionService = sessionService;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            LogAccountRequests(context);

            if (ShouldSignOut(context))
            {
                await SignOut(context);
                return;
            }

            if (ShouldRedirectToNoClaims(context))
            {
                context.Response.Cookies.Append(AuthenticationConstants.UnauthorizedCookie, "true", new CookieOptions()
                {
                    HttpOnly = true
                });

                await SignOut(context);
                return;
            }

            if (ShouldRedirectToTermsAndConditions(context))
            {
                var returnPath = HttpUtility.UrlEncode(GetReturnPath(context));
                context.Response.Redirect($"{_configuration.Urls.TermsAndConditionsRedirect}?returnpath={returnPath}");
                return;
            }

            var isSessionValid = await ValidateSession(context);
            if (!isSessionValid)
            {
                context.Response.Redirect(_configuration.Urls.SignedOutRedirect);
                return;
            }

            SetBearerToken(context);

            await _next(context);
        }

        private async Task SignOut(HttpContext httpContext)
        {
            var email = httpContext.GetClaimValue(ClaimTypes.Email);
            await _sessionService.EndAllUserSessions(email);

            var idToken = await httpContext.GetTokenAsync(AuthenticationConstants.IdToken);
            var postLogOutUrl = HttpUtility.UrlEncode($"{_configuration.AppHost}{AuthenticationConstants.AccountLogoutCallback}");
            var logoutRedirect = $"{_configuration.Oidc.BaseUrl}/logout?id_token_hint={idToken}&post_logout_redirect_uri={postLogOutUrl}";
            httpContext.Response.Redirect(logoutRedirect);
        }

        /// <summary>
        /// Only logs requests related to account activity
        /// </summary>
        private void LogAccountRequests(HttpContext httpContext)
        {
            if (!_configuration.EnableDebugLogging)
                return;

            if (!httpContext.Request.Path.HasValue)
                return;

            if (!httpContext.Request.Path.Value.Contains(AuthenticationConstants.AccountPaths, StringComparison.CurrentCultureIgnoreCase))
                return;

            _logger.LogInformation("Account Request Path:{Path} Headers:{@Headers}", httpContext.Request.Path.Value, httpContext.Request.Headers);
        }

        private string GetReturnPath(HttpContext context)
        {
            var path = context.Request.Path.ToString();
            var discriminatorPath = _configuration.PathBasedRouting?.DiscriminatorPath;

            if (!string.IsNullOrEmpty(discriminatorPath))
            {
                var subSiteTriggerPaths = _configuration.PathBasedRouting!.SubSiteTriggerPaths?.Split(',');
                //todo: rewrite and don't disable the warning
#pragma warning disable S3267
                foreach (var subSiteTriggerPath in subSiteTriggerPaths!)
                {
                    if (path.StartsWith(subSiteTriggerPath, StringComparison.InvariantCultureIgnoreCase))
                    {
                        path = $"{_configuration.PathBasedRouting.DiscriminatorPath}{path}";
                        break;
                    }
                }
#pragma warning restore S3267
            }

            return $"{path}{context.Request.QueryString}";
        }

        private async Task<bool> ValidateSession(HttpContext context)
        {
            if (!PageRequiresAuthorization(context))
            {
                return true;
            }

            if (!context.IsUserLoggedIn())
            {
                return true;
            }

            var sid = context.GetClaimValue(OneLoginClaimTypes.Sid);
            var isSessionActive = await _sessionService.IsSessionActive(sid);

            if (isSessionActive)
            {
                _ = _sessionService.RefreshSession(sid);// Do not await
            }
            else
            {
                context.Response.Cookies.Delete(_configuration.CookieName!);
                _logger.LogInformation("Session Id {Sid} not found in IDams", sid);
            }

            return isSessionActive;
        }
    }
}
