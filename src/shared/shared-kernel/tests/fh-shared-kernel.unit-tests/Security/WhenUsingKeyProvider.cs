using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Security.KeyVault.Secrets;
using FamilyHubs.SharedKernel.Security;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace FamilyHubs.SharedKernel.UnitTests.Security;

public class WhenUsingKeyProvider
{
    private IConfiguration _configuration;
    private KeyProvider _keyProvider;

    public WhenUsingKeyProvider()
    {
        var inMemorySettings = new Dictionary<string, string?> {
            {"Crypto:UseKeyVault", "True"},
            {"Crypto:PublicKey", "public_key"},
            {"Crypto:PrivateKey", "private_key"},
            {"Crypto:DbEncryptionKey", "DbEncryptionKey"},
            {"Crypto:DbEncryptionIVKey", "DbEncryptionIVKey"},

        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _keyProvider = new KeyProvider(_configuration);
    }

    [Fact]
    public async Task GetDbEncryptionKey_ShouldReturnKey_WhenUseKeyVaultIsFalse()
    {
        // Arrange
        var inMemorySettings = new Dictionary<string, string?> {
            {"Crypto:UseKeyVault", "False"},
            {"Crypto:DbEncryptionKey", "DbEncryptionKey"},
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _keyProvider = new KeyProvider(_configuration);


        // Act
        string publicKey = await _keyProvider.GetDbEncryptionKey();

        // Assert
        publicKey.Should().Be("DbEncryptionKey");
    }

    [Fact]
    public async Task GetDbEncryptionIVKey_ShouldReturnIVKey_WhenUseKeyVaultIsFalse()
    {
        // Arrange
        var inMemorySettings = new Dictionary<string, string?> {
            {"Crypto:UseKeyVault", "False"},
            {"Crypto:DbEncryptionIVKey", "DbEncryptionIVKey"},
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _keyProvider = new KeyProvider(_configuration);


        // Act
        string publicKey = await _keyProvider.GetDbEncryptionIVKey();

        // Assert
        publicKey.Should().Be("DbEncryptionIVKey");
    }

    [Fact]
    public async Task GetPublicKey_ShouldReturnPublicKey_WhenUseKeyVaultIsFalse()
    {
        // Arrange
        var inMemorySettings = new Dictionary<string, string?> {
            {"Crypto:UseKeyVault", "False"},
            {"Crypto:PublicKey", "public_key"},
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _keyProvider = new KeyProvider(_configuration);


        // Act
        string publicKey = await _keyProvider.GetPublicKey();

        // Assert
        publicKey.Should().Be("public_key");
    }

    [Fact]
    public async Task GetPrivateKey_ShouldReturnPublicKey_WhenUseKeyVaultIsFalse()
    {
        // Arrange
        var inMemorySettings = new Dictionary<string, string?> {
            {"Crypto:UseKeyVault", "False"},
            {"Crypto:PrivateKey", "private_key"},
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _keyProvider = new KeyProvider(_configuration);


        // Act
        string publicKey = await _keyProvider.GetPrivateKey();

        // Assert
        publicKey.Should().Be("private_key");
    }

    [Theory]
    [InlineData("Crypto:PublicKeySecretName", "PublicKeySecretName value missing.")]
    [InlineData("Crypto:KeyVaultIdentifier", "KeyVaultIdentifier value missing.")]
    [InlineData("Crypto:tenantId", "tenantId value missing.")]
    [InlineData("Crypto:clientId", "clientId value missing.")]
    [InlineData("Crypto:clientSecret", "clientSecret value missing.")]
    public async Task GetPublicKey_ShouldThrowArgumentException_WhenConfigIsMissing(string configKeyToDelete, string expectedExceptionString)
    {
        var inMemorySettings = new Dictionary<string, string?> {
            {"Crypto:UseKeyVault", "True"},
            {"Crypto:PublicKeySecretName", "PublicKeySecretName"},
            {"Crypto:KeyVaultIdentifier", "KeyVaultIdentifier"},
            {"Crypto:tenantId", "tenantId"},
            {"Crypto:clientId", "clientId"},
            {"Crypto:clientSecret", "clientSecret"}
        };

        bool keyRemoved = inMemorySettings.Remove(configKeyToDelete);

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _keyProvider = new KeyProvider(_configuration);


        // Act
        Func<Task> act = async () => await _keyProvider.GetPublicKey();

        // Assert
        keyRemoved.Should().BeTrue();
        await act.Should().ThrowAsync<ArgumentException>().WithMessage(expectedExceptionString);
    }

    [Theory]
    [InlineData("Crypto:PrivateKeySecretName", "PrivateKeySecretName value missing.")]
    [InlineData("Crypto:KeyVaultIdentifier", "KeyVaultIdentifier value missing.")]
    [InlineData("Crypto:tenantId", "tenantId value missing.")]
    [InlineData("Crypto:clientId", "clientId value missing.")]
    [InlineData("Crypto:clientSecret", "clientSecret value missing.")]
    public async Task GetPrivateKey_ShouldThrowArgumentException_WhenConfigIsMissing(string configKeyToDelete, string expectedExceptionString)
    {
        var inMemorySettings = new Dictionary<string, string?> {
            {"Crypto:UseKeyVault", "True"},
            {"Crypto:PrivateKeySecretName", "PrivateKeySecretName"},
            {"Crypto:KeyVaultIdentifier", "KeyVaultIdentifier"},
            {"Crypto:tenantId", "tenantId"},
            {"Crypto:clientId", "clientId"},
            {"Crypto:clientSecret", "clientSecret"}
        };

        bool keyRemoved = inMemorySettings.Remove(configKeyToDelete);

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _keyProvider = new KeyProvider(_configuration);


        // Act
        Func<Task> act = async () => await _keyProvider.GetPrivateKey();

        // Assert
        keyRemoved.Should().BeTrue();
        await act.Should().ThrowAsync<ArgumentException>().WithMessage(expectedExceptionString);
    }

    [Fact]
    public async Task GetPublicKey_ShouldThrowArgumentException_WhenPublicKeyIsMissing()
    {
       
        // Act
        Func<Task> act = async () => await _keyProvider.GetPublicKey();

        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("PublicKeySecretName value missing.");
    }

    [Fact]
    public async Task GetPrivateKey_ShouldThrowArgumentException_WhenKeyConfigurationValuesAreMissing()
    {
        // Act
        Func<Task> act = async () => await _keyProvider.GetPrivateKey();

        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("PrivateKeySecretName value missing.");
    }



    [Fact(Skip = "Use real config values when doing this test")]
    public async Task GetKeyValue_ShouldReturnSecretValue_WhenSecretPublicExists()
    {
        // Arrange
        var inMemorySettings = new Dictionary<string, string?> {
            {"Crypto:UseKeyVault", "True"},
            //Use real values
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _keyProvider = new KeyProvider(_configuration);

        var clientMock = new Mock<SecretClient>();
        clientMock.Setup(c => c.GetSecretAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(
            Response.FromValue(new KeyVaultSecret("name", "secret_value"), default!));

        var result = await _keyProvider.GetPublicKey();

        //Assert
        result.Should().NotBeNull();
        result.Length.Should().BeGreaterThan(10);
    }

    [Fact(Skip = "Use real config values when doing this test")]
    public async Task GetKeyValue_ShouldReturnSecretValue_WhenPrivateSecretExists()
    {
        // Arrange
        var inMemorySettings = new Dictionary<string, string?> {
            {"Crypto:UseKeyVault", "True"},
            //Use real values
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _keyProvider = new KeyProvider(_configuration);

        var clientMock = new Mock<SecretClient>();
        clientMock.Setup(c => c.GetSecretAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(
            Response.FromValue(new KeyVaultSecret("name", "secret_value"), default!));

        var result = await _keyProvider.GetPrivateKey();

        //Assert
        result.Should().NotBeNull();
        result.Length.Should().BeGreaterThan(10);
    }
}
