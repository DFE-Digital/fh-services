using System.Security.Claims;
using FamilyHubs.Referral.Api.AcceptanceTests.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace FamilyHubs.Referral.Api.AcceptanceTests.Fixtures;

public class BearerTokenGenerator
{
    private readonly string _bearerTokenSigningKey;

    public BearerTokenGenerator()
    {
        var config = ConfigAccessor.GetApplicationConfiguration();
        _bearerTokenSigningKey = config.BearerTokenSigningKey;
    }

    public string CreateBearerToken(string role)
    {
        var claims = new List<Claim> { new("role", role), new( "OrganisationId", "6"), new("AccountId", "6282")};        
        var identity = new ClaimsIdentity(claims, "Test");
        var user = new ClaimsPrincipal(identity);
        
        var key =
            new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(_bearerTokenSigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            claims: user.Claims,
            signingCredentials: creds,
            expires: DateTime.UtcNow.AddMinutes(5)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}