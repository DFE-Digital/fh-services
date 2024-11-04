using Azure.Identity;
using FamilyHubs.SharedKernel.Health;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FamilyHubs.RequestForSupport.Infrastructure.Health;

public static class HealthCheck
{
    public static IServiceCollection AddSiteHealthChecks(
        this IServiceCollection services,
        IConfiguration config)
    {
        // Handle API failures as Degraded, so that App Services doesn't remove or replace the instance (all instances!) due to an API being down
        services.AddFamilyHubsHealthChecks(config)
            .AddIdentityServerHealthCheck(config)
            .AddKeyVaultHealthCheck(config)
            .AddNotificationApiHealthCheck(config);
        
        return services;
    }

    private static IHealthChecksBuilder AddIdentityServerHealthCheck(
        this IHealthChecksBuilder healthChecksBuilder, 
        IConfiguration configuration)
    {
        var oneLoginUrl = configuration.GetValue<string>("GovUkOidcConfiguration:Oidc:BaseUrl");
        healthChecksBuilder.AddIdentityServer(
            new Uri(oneLoginUrl!),
            name: "One Login",
            failureStatus: HealthStatus.Degraded,
            tags: [FhHealthChecksBuilder.UrlType.ExternalApi.ToString()]);

        return healthChecksBuilder;
    }
    
    private static IHealthChecksBuilder AddKeyVaultHealthCheck(
        this IHealthChecksBuilder healthChecksBuilder, 
        IConfiguration configuration)
    {
        var keyVaultKey = configuration.GetValue<string>("DataProtection:KeyIdentifier");

        if (!string.IsNullOrWhiteSpace(keyVaultKey))
        {
            var keysIndex = keyVaultKey.IndexOf("/keys/", StringComparison.OrdinalIgnoreCase);
            var keyVaultUrl = keyVaultKey[..keysIndex];
            var keyName = keyVaultKey[(keysIndex + 6)..];

            healthChecksBuilder.AddAzureKeyVault(
                new Uri(keyVaultUrl),
                new ManagedIdentityCredential(),
                s => s.AddKey(keyName), name: "Azure Key Vault",
                failureStatus: HealthStatus.Degraded,
                tags: ["Infrastructure"]);
        }
        
        return healthChecksBuilder;
    }

    private static void AddNotificationApiHealthCheck(
        this IHealthChecksBuilder healthChecksBuilder,
        IConfiguration configuration)
    {
        var notificationApiUrl = configuration.GetValue<string>("Notification:Endpoint");
        
        if (!string.IsNullOrEmpty(notificationApiUrl))
        {
            notificationApiUrl = notificationApiUrl.Replace("/api/notify", "/api/info");
            healthChecksBuilder.AddUrlGroup(new Uri(notificationApiUrl), "Notification API", HealthStatus.Degraded,
                [FhHealthChecksBuilder.UrlType.InternalApi.ToString()]);
        }
    }
}