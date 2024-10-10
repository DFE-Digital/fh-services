using AutoMapper;
using FamilyHubs.Notification.Core;
using FamilyHubs.Notification.Data.Interceptors;
using FamilyHubs.Notification.Data.Repository;
using FamilyHubs.SharedKernel.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using FamilyHubs.Notification.Api.Contracts;
using FamilyHubs.Notification.Data.Entities;
using NSubstitute;

namespace FamilyHubs.Notification.UnitTests;

public abstract class BaseCreateDbUnitTest
{
    protected static ApplicationDbContext GetApplicationDbContext()
    {
        var options = CreateNewContextOptions();
        var mockIHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        var context = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([
                new Claim(ClaimTypes.Name, "John Doe"),
                new Claim("OrganisationId", "1"),
                new Claim("AccountId", "2"),
                new Claim("AccountStatus", "Active"),
                new Claim("Name", "John Doe"),
                new Claim("ClaimsValidTillTime", "2023-09-11T12:00:00Z"),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "john@example.com"),
                new Claim("PhoneNumber", "123456789")
            ], "test"))
        };

        mockIHttpContextAccessor.HttpContext.Returns(context);
        var auditableEntitySaveChangesInterceptor = new AuditableEntitySaveChangesInterceptor(mockIHttpContextAccessor);

        var inMemorySettings = new Dictionary<string, string?> {
            {"Crypto:UseKeyVault", "False"},
            {"Crypto:DbEncryptionKey", "188,7,221,249,250,101,147,86,47,246,21,252,145,56,161,150,195,184,64,43,55,0,196,200,98,220,95,186,225,8,224,75"},
            {"Crypto:DbEncryptionIVKey", "34,26,215,81,137,34,109,107,236,206,253,62,115,38,65,112"},
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var keyProvider = new KeyProvider(configuration);

        var mockApplicationDbContext = new ApplicationDbContext(options, auditableEntitySaveChangesInterceptor, keyProvider);

        return mockApplicationDbContext;
    }

    private static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
    {
        // Create a fresh service provider, and therefore a fresh
        // InMemory database instance.
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        // Create a new options instance telling the context to use an
        // InMemory database and the new service provider.
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseInMemoryDatabase("NotificationDb")
               .UseInternalServiceProvider(serviceProvider);

        return builder.Options;
    }

    protected static IMapper GetMapper()
    {
        var myProfile = new AutoMappingProfiles();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        IMapper mapper = new Mapper(configuration);
        return mapper;
    }

    protected static readonly List<SentNotification> NotificationList =
    [
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
    ];
}