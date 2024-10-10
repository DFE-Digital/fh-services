using FamilyHubs.SharedKernel.Security;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using System.Security.Cryptography;
using NSubstitute;

namespace FamilyHubs.SharedKernel.UnitTests.Security;

public class WhenUsingCrytoHelperClass
{
    [Fact]
    public async Task ThenEncryptStringAndThenDecryptItBack()
    {
        //Arrange
        (string publicKey, string privateKey, string dbEncryptionKey, string dbEncryptionIvKey) = GenerateKeys();

        IKeyProvider provider = Substitute.For<IKeyProvider>();
        provider.GetPublicKey().Returns(publicKey);
        provider.GetPrivateKey().Returns(privateKey);
        provider.GetDbEncryptionKey().Returns(dbEncryptionKey);
        provider.GetDbEncryptionIVKey().Returns(dbEncryptionIvKey);

        string expected = "Hello, RSA encryption!";
        ICrypto crypto = new Crypto(provider);

        //Act
        string encryptedData = await crypto.EncryptData(expected);
        string decryptedData = await crypto.DecryptData(encryptedData);

        //Assert
        expected.Should().Be(decryptedData);
        expected.Should().NotBe(encryptedData);
        encryptedData.Length.Should().BeGreaterThan(expected.Length);
        dbEncryptionKey.Should().NotBeNullOrEmpty();
        dbEncryptionIvKey.Should().NotBeNullOrEmpty();
    }

    private (string publicKey, string privateKey, string dbEncryptionKey, string dbEncryptionIVKey) GenerateKeys()
    {
        using (var rsa = RSA.Create())
        {
            string publicKey = rsa.ToXmlString(false);
            string privateKey = rsa.ToXmlString(true);

            var result = AesProvider.GenerateKey(AesKeySize.AES256Bits);
            List<string> items = new List<string>();
            foreach (var item in result.IV)
            {
                items.Add(item.ToString());
            }
            string dbEncryptionIvKey = string.Join(',', items.ToArray());
            items.Clear();

            foreach (var item in result.Key)
            {
                items.Add(item.ToString());
            }
            string dbEncryptionKey = string.Join(',', items.ToArray());

            return (publicKey, privateKey, dbEncryptionKey, dbEncryptionIvKey);
        }
    }
}
