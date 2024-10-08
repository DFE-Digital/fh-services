using FamilyHubs.SharedKernel.GovLogin.AppStart;
using FamilyHubs.SharedKernel.GovLogin.Configuration;
using FamilyHubs.SharedKernel.Identity.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.KeyVaultExtensions;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace FamilyHubs.SharedKernel.Identity.Authentication.Gov
{
    internal static class ServiceCollectionExtension
    {
        internal static void AddGovUkAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var govUkConfiguration = configuration.GetGovUkOidcConfiguration();
            if (string.IsNullOrWhiteSpace(govUkConfiguration.CookieName))
                throw new AuthConfigurationException($"CookieName is not configured in {nameof(GovUkOidcConfiguration)} section of appsettings");

            services
                .AddAuthentication(options => ConfigureAuthenticationOptions(options))
                .AddOpenIdConnect(options => ConfigureOpenIdConnect(options, govUkConfiguration))
                .AddAuthenticationCookie(govUkConfiguration.CookieName, govUkConfiguration);

            services
                .AddOptions<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme)
                .Configure<IOidcService, IAzureIdentityService, GovUkOidcConfiguration>(ConfigureToken);
        }

        private static void ConfigureAuthenticationOptions(AuthenticationOptions options)
        {
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            options.DefaultSignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;

        }

        private static void ConfigureOpenIdConnect(OpenIdConnectOptions options, GovUkOidcConfiguration govUkConfiguration)
        {
            options.ClientId = govUkConfiguration.Oidc.ClientId;
            options.MetadataAddress = $"{govUkConfiguration.Oidc.BaseUrl}/.well-known/openid-configuration";
            options.ResponseType = "code";
            options.AuthenticationMethod = OpenIdConnectRedirectBehavior.RedirectGet;
            options.SignedOutRedirectUri = $"{govUkConfiguration.AppHost}/Account/logout-callback";
            options.SignedOutCallbackPath = "/Account/logout-callback";
            options.CallbackPath = "/Account/login-callback";
            options.ResponseMode = string.Empty;
            options.SaveTokens = true;
            

            var scopes = "openid email phone".Split(' ');
            options.Scope.Clear();
            foreach (var scope in scopes)
            {
                options.Scope.Add(scope);
            }

            options.Events.OnRedirectToIdentityProvider = c =>
            {
                if (!govUkConfiguration.Oidc.TwoFactorEnabled)
                {
                    // If TwoFactorDisabled this parameter will lower the authenitcation level and not
                    // require a two-factor code
                    c.ProtocolMessage.SetParameter("vtr", "[\"Cl\"]");
                }

                c.ProtocolMessage.SetParameter("prompt", "login");

                c.ProtocolMessage.RedirectUri = $"{govUkConfiguration.AppHost}/Account/login-callback";
                return Task.CompletedTask;
            };

            options.Events.OnRedirectToIdentityProviderForSignOut = c =>
            {
                c.ProtocolMessage.RedirectUri = $"{govUkConfiguration.AppHost}/Account/logout-callback";
                return Task.CompletedTask;
            };

            options.Events.OnRemoteFailure = c =>
            {
                if (c.Failure != null && c.Failure.Message.Contains("Correlation failed"))
                {
                    c.Response.Redirect("/");
                    c.HandleResponse();
                }

                return Task.CompletedTask;
            };

            options.Events.OnSignedOutCallbackRedirect = c =>
            {
                c.Response.Cookies.Delete(govUkConfiguration.CookieName!);

                if (c.Request.Cookies[AuthenticationConstants.UnauthorizedCookie] != null)
                {
                    c.Response.Cookies.Delete(AuthenticationConstants.UnauthorizedCookie);
                    c.Response.Redirect(govUkConfiguration.Urls.NoClaimsRedirect);
                }
                else
                {
                    c.Response.Redirect(govUkConfiguration.Urls.SignedOutRedirect);
                }

                c.HandleResponse();
                return Task.CompletedTask;
            };

        }

        private static void ConfigureToken(OpenIdConnectOptions options, IOidcService oidcService, IAzureIdentityService azureIdentityService, GovUkOidcConfiguration config)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                AuthenticationType = "private_key_jwt",
                IssuerSigningKey = GetIssuerSigningKey(config, azureIdentityService),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                SaveSigninToken = true,
                ValidateLifetime = true,
                ValidIssuer = $"{config.Oidc.BaseUrl}/"
            };
            options.Events.OnAuthorizationCodeReceived = async (ctx) =>
            {
                // the real RedirectUri is here at this point
                string? discriminatorPath = config.PathBasedRouting?.DiscriminatorPath;
                if (!string.IsNullOrEmpty(discriminatorPath))
                {
                    var subSiteTriggerPaths = config.PathBasedRouting!.SubSiteTriggerPaths?.Split(',');
                    //todo: rewrite and don't disable the warning
#pragma warning disable S3267
                    foreach (var subSiteTriggerPath in subSiteTriggerPaths!)
                    {
                        if (ctx.Properties!.RedirectUri!.StartsWith(subSiteTriggerPath, StringComparison.InvariantCultureIgnoreCase))
                        {
                            ctx.Properties!.RedirectUri = $"{config.PathBasedRouting.DiscriminatorPath}{ctx.Properties!.RedirectUri}";
                            break;
                        }
                    }
#pragma warning restore S3267
                }

                var token = await oidcService.GetToken(ctx.TokenEndpointRequest!);
                if (token?.AccessToken != null && token.IdToken != null)
                {
                    ctx.HandleCodeRedemption(token.AccessToken, token.IdToken);
                }
            };

            options.Events.OnTokenValidated = async ctx => await oidcService.PopulateAccountClaims(ctx);
        }

        private static SecurityKey GetIssuerSigningKey(GovUkOidcConfiguration config, IAzureIdentityService azureIdentityService)
        {
            if (config.UseKeyVault())
            {
                return new KeyVaultSecurityKey(config.Oidc.KeyVaultIdentifier!, azureIdentityService.AuthenticationCallback);
            }

            var unencodedKey = config.Oidc.PrivateKey!;
            var privateKeyBytes = Convert.FromBase64String(unencodedKey);

            var rsa = RSA.Create();
            rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);
            var key = new RsaSecurityKey(rsa);
            return key;
        }
    }
}
