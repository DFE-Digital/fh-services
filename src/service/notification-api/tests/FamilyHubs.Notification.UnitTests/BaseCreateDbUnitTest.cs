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
                new(ClaimTypes.Name, "John Doe"),
                new("OrganisationId", "1"),
                new("AccountId", "2"),
                new("AccountStatus", "Active"),
                new("Name", "John Doe"),
                new("ClaimsValidTillTime", "2023-09-11T12:00:00Z"),
                new("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "john@example.com"),
                new("PhoneNumber", "123456789")
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
}
