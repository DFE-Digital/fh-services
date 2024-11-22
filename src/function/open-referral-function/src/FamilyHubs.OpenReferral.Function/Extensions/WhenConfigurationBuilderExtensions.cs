using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace FamilyHubs.OpenReferral.Function.Extensions;

public static class WhenConfigurationBuilderExtensions
{
    public static void ConfigureAzureKeyVault(this IConfigurationBuilder configurationBuilder)
    {
        var configuration = configurationBuilder.Build();
        var keyVaultEndpoint = configuration["AppConfiguration:KeyVaultPrefix"];
        var keyVaultIdentifier = configuration["AppConfiguration:KeyVaultIdentifier"];

        if (!string.IsNullOrEmpty(keyVaultEndpoint) && !string.IsNullOrEmpty(keyVaultIdentifier))
        {
            configurationBuilder.AddAzureKeyVault(
                new Uri($"https://{keyVaultIdentifier}.vault.azure.net/"),
                new DefaultAzureCredential(),
                new PrefixKeyVaultSecretManager(keyVaultEndpoint));
        }
    }

    public sealed class PrefixKeyVaultSecretManager(string prefix) : KeyVaultSecretManager
    {
        private readonly string _prefix = $"{prefix}-";
        
        public override bool Load(SecretProperties secret)
            => secret.Name.StartsWith(_prefix);

        public override string GetKey(KeyVaultSecret secret)
            => secret.Name[_prefix.Length..].Replace("--", ConfigurationPath.KeyDelimiter);
    }   
}