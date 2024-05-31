﻿using AutoFixture;
using FamilyHubs.Idam.Core.Commands;
using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.Idam.Data.Interceptors;
using FamilyHubs.Idam.Data.Repository;
using FamilyHubs.SharedKernel.Security;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace FamilyHubs.Idam.Core.IntegrationTests;

public abstract class DataIntegrationTestBase<T> : DataIntegrationTestBase
{
    protected Mock<ILogger<T>> MockLogger { get; set; }

    protected DataIntegrationTestBase():base()
    {
        MockLogger = new Mock<ILogger<T>>();
    }
}

#pragma warning disable S3881
public abstract class DataIntegrationTestBase : IDisposable, IAsyncDisposable
{
    protected AccountClaim TestSingleAccountClaim { get; set; }
    protected ApplicationDbContext TestDbContext { get; }
    protected static NullLogger<T> GetLogger<T>() => new NullLogger<T>();
    protected Fixture Fixture { get; private set; }

    protected DataIntegrationTestBase()
    {
        Fixture = new Fixture();

        TestSingleAccountClaim = TestDataProvider.GetSingleTestAccountClaim();
        
        var serviceProvider = CreateNewServiceProvider();

        TestDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

        InitialiseDatabase();
    }

    protected async Task<AccountClaim> CreateAccountClaim(AccountClaim? accountClaim = null)
    {
        var entity = accountClaim ?? TestSingleAccountClaim;
        
        TestDbContext.AccountClaims.Add(entity);

        await TestDbContext.SaveChangesAsync();

        return entity;
    }

    private void InitialiseDatabase()
    {
        TestDbContext.Database.EnsureDeleted();
        TestDbContext.Database.EnsureCreated();
        TestDbContext.Accounts.Add(new Account
        {
            Id = 1,
            OpenId = "Test OpenId",
            Name = "Test Name",
            Email = "Test@test.com",
            PhoneNumber = "01234567890",
            Status = AccountStatus.Active,
        });
        TestDbContext.SaveChanges();
    }

    private static ServiceProvider CreateNewServiceProvider()
    {
        var serviceDirectoryConnection = $"Data Source=idam-{Random.Shared.Next().ToString()}.db;Mode=ReadWriteCreate;Cache=Shared;Foreign Keys=True;Recursive Triggers=True;Default Timeout=30;Pooling=True";
        var mockIHttpContextAccessor = Mock.Of<IHttpContextAccessor>();
        var auditableEntitySaveChangesInterceptor = new AuditableEntitySaveChangesInterceptor(mockIHttpContextAccessor);

        var inMemorySettings = new Dictionary<string, string?> {
            {"Crypto:UseKeyVault", "False"},
            {"Crypto:DbEncryptionKey", "188,7,221,249,250,101,147,86,47,246,21,252,145,56,161,150,195,184,64,43,55,0,196,200,98,220,95,186,225,8,224,75"},
            {"Crypto:DbEncryptionIVKey", "34,26,215,81,137,34,109,107,236,206,253,62,115,38,65,112"},
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        IKeyProvider keyProvider = new KeyProvider(configuration);

        return new ServiceCollection().AddEntityFrameworkSqlite()
            .AddDbContext<ApplicationDbContext>(dbContextOptionsBuilder =>
            {
                dbContextOptionsBuilder.UseSqlite(serviceDirectoryConnection,
                    opt => { opt.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.ToString()); });
            })
            .AddSingleton(keyProvider)
            .AddSingleton(auditableEntitySaveChangesInterceptor)
            .BuildServiceProvider();
    }

    public void Dispose()
    {
        DisposeAsync().GetAwaiter().GetResult();
    }

    public async ValueTask DisposeAsync()
    {
        await TestDbContext.Database.EnsureDeletedAsync();
    }

    public Mock<ISender> GetMockEventGridSender(object response, Action callback)
    {
        Mock<ISender> mockSender = new Mock<ISender>();

        if(response is string responseString)
        {
            mockSender.Setup(x => x.Send(It.IsAny<SendEventGridMessageCommand>(), It.IsAny<CancellationToken>()))
                .Callback(() => callback()).ReturnsAsync(responseString);
        }

        if(response is Exception responseException)
        {

            mockSender.Setup(x => x.Send(It.IsAny<SendEventGridMessageCommand>(), It.IsAny<CancellationToken>()))
                .Callback(() => callback()).Throws(responseException);
        }

        return mockSender;
    }
}
#pragma warning restore S3881