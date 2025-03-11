using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FamilyHubs.Referral.Data.Repository;
using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FamilyHubs.Referral.FunctionalTests;

public class BaseWhenUsingOpenReferralApiUnitTests : IDisposable
{
    protected readonly HttpClient Client;
    protected readonly CustomWebApplicationFactory? WebAppFactory;
    private readonly ServiceDirectoryFactory? _serviceDirectoryFactory;
    protected readonly JwtSecurityToken? Token;
    protected readonly JwtSecurityToken? TokenLaManager;
    protected readonly JwtSecurityToken? TokenForOrganisationOne;
    protected readonly JwtSecurityToken? VcsToken;
    protected readonly JwtSecurityToken? ForbiddenToken;

    protected BaseWhenUsingOpenReferralApiUnitTests()
    {
        var confBuilder = new ConfigurationBuilder();
        ConfigSetup(confBuilder);
        var conf = confBuilder.Build();

        var jti = Guid.NewGuid().ToString();
        var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(conf["GovUkOidcConfiguration:BearerTokenSigningKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        Token = new JwtSecurityToken(
            claims: new List<Claim>
            {
                new("sub", conf["GovUkOidcConfiguration:Oidc:ClientId"] ?? ""),
                new("jti", jti),
                new(ClaimTypes.Role, RoleTypes.LaProfessional),
                new(FamilyHubsClaimTypes.OrganisationId, "1"),
                new(FamilyHubsClaimTypes.AccountId, "1")
            },
            signingCredentials: creds,
            expires: DateTime.UtcNow.AddMinutes(5)
        );

        TokenLaManager = new JwtSecurityToken(
            claims: new List<Claim>
            {
                new("sub", conf["GovUkOidcConfiguration:Oidc:ClientId"] ?? ""),
                new("jti", jti),
                new(ClaimTypes.Role, RoleTypes.LaManager),
                new(FamilyHubsClaimTypes.OrganisationId, "1"),
                new(FamilyHubsClaimTypes.AccountId, "1")
            },
            signingCredentials: creds,
            expires: DateTime.UtcNow.AddMinutes(5)
        );

        TokenForOrganisationOne = new JwtSecurityToken(
            claims: new List<Claim>
            {
                new("sub", conf["GovUkOidcConfiguration:Oidc:ClientId"] ?? ""),
                new("jti", jti),
                new("AccountId", "5"),
                new(ClaimTypes.Role, RoleTypes.LaProfessional),
                new(FamilyHubsClaimTypes.OrganisationId, "1"),
                new(FamilyHubsClaimTypes.AccountId, "1")
            },
            signingCredentials: creds,
            expires: DateTime.UtcNow.AddMinutes(5)
        );

        VcsToken = new JwtSecurityToken(
            claims: new List<Claim>
            {
                new("sub", conf["GovUkOidcConfiguration:Oidc:ClientId"] ?? ""),
                new("jti", jti),
                new(ClaimTypes.Role, RoleTypes.VcsProfessional),
                new(FamilyHubsClaimTypes.OrganisationId, "1"),
                new(FamilyHubsClaimTypes.AccountId, "1")
            },
            signingCredentials: creds,
            expires: DateTime.UtcNow.AddMinutes(5)
        );

        ForbiddenToken = new JwtSecurityToken(
            claims: new List<Claim>
            {
                new("sub", conf["GovUkOidcConfiguration:Oidc:ClientId"] ?? ""),
                new("jti", jti),
                new(ClaimTypes.Role, RoleTypes.VcsProfessional),
                new(FamilyHubsClaimTypes.OrganisationId, "-1"),
                new(FamilyHubsClaimTypes.AccountId, "-1")
            },
            signingCredentials: creds,
            expires: DateTime.UtcNow.AddMinutes(5)
        );

        _serviceDirectoryFactory = new ServiceDirectoryFactory();
        _serviceDirectoryFactory.SetupTestDatabaseAndSeedData();
        var sdClient = _serviceDirectoryFactory.CreateDefaultClient();

        WebAppFactory = new CustomWebApplicationFactory(ConfigSetup, sdClient);
        WebAppFactory.SetupTestDatabaseAndSeedData();

        Client = WebAppFactory.CreateDefaultClient();
        Client.BaseAddress = new Uri("https://localhost:7192/");
    }

    public static UserAccountDto GetUserAccount()
    {
        UserAccountDto userAccountDto = new UserAccountDto
        {
            Id = 2,
            EmailAddress = "FirstUser@email.com",
            Name = "First User",
            PhoneNumber = "0161 111 1111",
            Team = "Test Team"
        };

        userAccountDto.OrganisationUserAccounts = new List<UserAccountOrganisationDto>
        {
            new UserAccountOrganisationDto
            {
                UserAccount = default!,
                Organisation = new OrganisationDto
                {
                    Id = 2,
                    Name = "Organisation",
                    Description = "Organisation Description",
                }
            }
        };

        return userAccountDto;
    }

    private static void ConfigSetup(IConfigurationBuilder builder) =>
        builder.AddInMemoryCollection(new Dictionary<string, string?>
        {
            { "GovUkOidcConfiguration:BearerTokenSigningKey", "StubPrivateKey123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ" },
            { "Crypto:DbEncryptionKey", "188,7,221,249,250,101,147,86,47,246,21,252,145,56,161,150,195,184,64,43,55,0,196,200,98,220,95,186,225,8,224,75" },
            { "Crypto:DbEncryptionIVKey", "34,26,215,81,137,34,109,107,236,206,253,62,115,38,65,112" }
        });

    protected virtual void Dispose(bool disposing)
    { 
        using IServiceScope referralScope = WebAppFactory!.Services.CreateScope(); 
        using IServiceScope serviceDirectoryScope = _serviceDirectoryFactory!.Services.CreateScope();
        
        ApplicationDbContext referralDbContext = referralScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        ServiceDirectory.Data.Repository.ApplicationDbContext serviceDirectoryDbContext = serviceDirectoryScope.ServiceProvider.GetRequiredService<ServiceDirectory.Data.Repository.ApplicationDbContext>();
        
        referralDbContext.Database.EnsureDeleted(); 
        serviceDirectoryDbContext.Database.EnsureDeleted();

        Client.Dispose();
        WebAppFactory?.Dispose();
        _serviceDirectoryFactory?.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
