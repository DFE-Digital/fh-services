using AutoMapper;
using FamilyHubs.Notification.Api;
using FamilyHubs.Notification.Api.Contracts;
using FamilyHubs.Notification.Core;
using FamilyHubs.Notification.Data.Entities;
using FamilyHubs.Notification.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FamilyHubs.Notification.FunctionalTests;

#pragma warning disable S3881
public abstract class BaseWhenUsingOpenReferralApiUnitTests : IDisposable
{
    private readonly bool _initSuccessful;
    private readonly IConfiguration? _configuration;
    private readonly CustomWebApplicationFactory? _webAppFactory;
    // ReSharper disable once NotAccessedField.Local
    private string? _emailRecipient;
    private bool _disposed;
    
    protected BaseWhenUsingOpenReferralApiUnitTests()
    {
        _disposed = false;

        try
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                 .AddEnvironmentVariables()
                 .Build();

            _configuration = config;

            _emailRecipient = config.GetValue<string>("EmailRecipient") ?? string.Empty;

            string[] keys = { "ProfessionalAcceptRequest", "ProfessionalDecineRequest", "ProfessionalSentRequest", "VcsNewRequest" };

            // ReSharper disable once CollectionNeverQueried.Local
            Dictionary<string, string> templates = new();
            foreach (string templateKey in keys)
            {
                var value = config.GetValue<string>(templateKey);
                if (value != null)
                    templates[templateKey] = value;
            }


            var jti = Guid.NewGuid().ToString();
            var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(config["GovUkOidcConfiguration:BearerTokenSigningKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            Token = new JwtSecurityToken(
                claims: new List<Claim>
                   {
                    new("sub", config["GovUkOidcConfiguration:Oidc:ClientId"] ?? ""),
                    new("jti", jti),
                    new(ClaimTypes.Role, "Professional")

                   },
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddMinutes(5)
                );

            _webAppFactory = new CustomWebApplicationFactory();
            _webAppFactory.SetupTestDatabaseAndSeedData();

            Client = _webAppFactory.CreateDefaultClient();
            Client.BaseAddress = new Uri("https://localhost:7073/");

            _initSuccessful = true;
        }
        catch
        {
            _initSuccessful = false;
        }
        
    }

    protected HttpClient? Client { get; }
    
    protected JwtSecurityToken? Token { get; }

    protected virtual void Dispose(bool disposing)
    {
        // Cleanup
        if (!_disposed &&  disposing)
        {
            Dispose();
        }
        _disposed = true;
    }

    public void Dispose()
    {
        if (!_initSuccessful)
        {
            return;
        }

        using var scope = _webAppFactory!.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureDeleted();

        if (Client != null)
        {
            Client.Dispose();
        }

        _webAppFactory?.Dispose();

        GC.SuppressFinalize(this);
    }

    protected IMapper GetMapper()
    {
        var myProfile = new AutoMappingProfiles();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        IMapper mapper = new Mapper(configuration);
        return mapper;
    }

    protected void SeedDatabase()
    {
        using var scope = _webAppFactory!.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.AddRange(GetNotificationList());
        context.SaveChanges();
    }

    protected List<SentNotification> GetNotificationList()
    {
        return new List<SentNotification>
        {
            new()
            {
                Id = 1,
                ApiKeyType = ApiKeyType.ManageKey,
                Notified = new List<Notified>
                {
                    new()
                    {
                        Id = 1,
                        NotificationId = 1,
                        Value = "Firstperson@email.com"
                    }
                },
                TemplateId = "11111",
                TokenValues = new List<TokenValue>
                {
                    new()
                    {
                        Id = 1,
                        NotificationId = 1,
                        Key = "Key1",
                        Value = "Value1"
                    }
                }

            },

            new()
            {
                Id = 2,
                ApiKeyType = ApiKeyType.ConnectKey,
                Notified = new List<Notified>
                {
                    new()
                    {
                        Id = 2,
                        NotificationId = 2,
                        Value = "Secondperson@email.com"
                    }
                },
                TemplateId = "2222",
                TokenValues = new List<TokenValue>
                {
                    new()
                    {
                        Id = 2,
                        NotificationId = 2,
                        Key = "Key2",
                        Value = "Value2"
                    }
                }
            }
        };
    }

    protected bool IsRunningLocally()
    {

        if (!_initSuccessful || _configuration == null)
        {
            return false;
        }

        try
        {
            string localMachineName = _configuration["LocalSettings:MachineName"] ?? string.Empty;

            if (!string.IsNullOrEmpty(localMachineName))
            {
                return Environment.MachineName.Equals(localMachineName, StringComparison.OrdinalIgnoreCase);
            }
        }
        catch
        {
            return false;
        }

        // Fallback to a default check if User Secrets file or machine name is not specified
        // For example, you can add additional checks or default behavior here
        return false;
    }
}

#pragma warning restore S3881