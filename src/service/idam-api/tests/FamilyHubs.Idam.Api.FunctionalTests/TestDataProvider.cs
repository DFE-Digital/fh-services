using FamilyHubs.Idam.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FamilyHubs.Idam.Api.FunctionalTests;

public static class TestDataProvider
{
    public static string AccountEmail = "Test@test.com";
    public static string BearerTokenSigningKey = "StubPrivateKey123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static Account GetTestAccount(bool many = false)
    {
        return new Account
        {
            Id = 1,
            OpenId = "TestOpenId",
            Name = "Test Name",
            Email = "Test@test.com",
            PhoneNumber = "01234567890",
            Status = AccountStatus.Active,
            Claims = GetTestAccountClaims(many).ToList(),
        };
    }

    public static IEnumerable<AccountClaim> GetTestAccountClaims(bool many = false)
    {
        var claims = new List<AccountClaim>
        {
            new AccountClaim
            {
                Name = "ClaimName",
                Value = "ClaimValue"
            }
        };

        if (many)
        {
            claims.Add(new AccountClaim
            {
                Name = "AnotherClaimName",
                Value = "AnotherClaimValue"
            });
        }

        return claims;
    }

    public static string CreateBearerToken(string role)
    {
        var claims = new List<Claim> { new Claim("role", role) };
        var identity = new ClaimsIdentity(claims,"Test");
        var user = new ClaimsPrincipal(identity);

        var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(BearerTokenSigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            claims: user.Claims,
            signingCredentials: creds,
            expires: DateTime.UtcNow.AddMinutes(5)
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}