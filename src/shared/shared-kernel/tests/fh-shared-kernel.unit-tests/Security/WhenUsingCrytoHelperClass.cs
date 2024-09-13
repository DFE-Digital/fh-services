using System.Collections.Generic;
using FamilyHubs.SharedKernel.Security;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Moq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace FamilyHubs.SharedKernel.UnitTests.Security;

public class WhenUsingCrytoHelperClass
{
    
    [Fact]
    public async Task ThenEncryptStringAndThenDecryptItBack()
    {
        //Arrange
        (string publicKey, string privateKey, string dbEncryptionKey, string dbEncryptionIVKey) = GenerateKeys();

        Mock<IKeyProvider> provider = new Mock<IKeyProvider>();
        provider.Setup(x => x.GetPublicKey()).ReturnsAsync(publicKey);
        provider.Setup(x => x.GetPrivateKey()).ReturnsAsync(privateKey);
        provider.Setup(x => x.GetDbEncryptionKey()).ReturnsAsync(dbEncryptionKey);
        provider.Setup(x => x.GetDbEncryptionIVKey()).ReturnsAsync(dbEncryptionIVKey);

        string expected = "Hello, RSA encryption!";
        ICrypto crypto = new Crypto(provider.Object);

        //Act
        string encryptedData = await crypto.EncryptData(expected);
        string decryptedData = await crypto.DecryptData(encryptedData);

        //Assert
        expected.Should().Be(decryptedData);
        expected.Should().NotBe(encryptedData);
        encryptedData.Length.Should().BeGreaterThan(expected.Length);
        dbEncryptionKey.Should().NotBeNullOrEmpty();
        dbEncryptionIVKey.Should().NotBeNullOrEmpty();
    }
    

    public (string publicKey, string privateKey, string dbEncryptionKey, string dbEncryptionIVKey) GenerateKeys()
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
            string dbEncryptionIVKey = string.Join(',', items.ToArray());
            items.Clear();

            foreach (var item in result.Key)
            {
                items.Add(item.ToString());
            }
            string dbEncryptionKey = string.Join(',', items.ToArray());

            return (publicKey, privateKey, dbEncryptionKey, dbEncryptionIVKey);
        }
    }
}
