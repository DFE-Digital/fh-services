using Azure.Security.KeyVault.Secrets;

namespace FamilyHubs.ReferralUi.UnitTests.Extensions.StartupExtensions;

public class KeyVaultTests
{
    [Fact]
    public void Load_ReturnsTrue_WhenSecretNameStartsWithPrefix()
    {
        // Arrange
        const string prefix = "test-prefix";
        var secretProperties = new SecretProperties($"{prefix}-secret-name");
        var manager = new SharedKernel.Extensions.StartupExtensions.PrefixKeyVaultSecretManager(prefix);

        // Act
        var result = manager.Load(secretProperties);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Load_ReturnsFalse_WhenSecretNameDoesNotStartWithPrefix()
    {
        // Arrange
        const string prefix = "test-prefix";
        var secretProperties = new SecretProperties("other-secret-name");
        var manager = new SharedKernel.Extensions.StartupExtensions.PrefixKeyVaultSecretManager(prefix);

        // Act
        var result = manager.Load(secretProperties);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetKey_ReturnsExpectedKey()
    {
        // Arrange
        const string prefix = "test-prefix";
        var secretName = $"{prefix}-secret--name";
        var secret = new KeyVaultSecret(secretName, "secret-value");
        var manager = new SharedKernel.Extensions.StartupExtensions.PrefixKeyVaultSecretManager(prefix);

        // Act
        var result = manager.GetKey(secret);

        // Assert
        const string expectedKey = "secret:name";
        Assert.Equal(expectedKey, result);
    }
}