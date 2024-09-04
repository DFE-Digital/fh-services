using FamilyHubs.Notification.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FamilyHubs.Notification.Data.Entities;
using FamilyHubs.SharedKernel.Security;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.EntityFrameworkCore.DataEncryption;

namespace FamilyHubs.Notification.Data.Repository;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    private readonly IEncryptionProvider _provider;

    public ApplicationDbContext
        (
            DbContextOptions<ApplicationDbContext> options,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,
            IKeyProvider keyProvider
        )
        : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;

        byte[]? byteEncryptionKey;
        byte[]? byteEncryptionIV;

        string encryptionKey = keyProvider.GetDbEncryptionKey().Result;
        if (!string.IsNullOrEmpty(encryptionKey))
        {
            byteEncryptionKey = ConvertStringToByteArray(encryptionKey);
        }
        else
        {
            throw new ArgumentException("EncryptionKey is missing");
        }
        string encryptionIV = keyProvider.GetDbEncryptionIVKey().Result;
        if (!string.IsNullOrEmpty(encryptionIV))
        {
            byteEncryptionIV = ConvertStringToByteArray(encryptionIV);
        }
        else
        {
            throw new ArgumentException("EncryptionIV is missing");
        }
        _provider = new AesProvider(byteEncryptionKey, byteEncryptionIV);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        var sentNotificationEntity = modelBuilder.Entity<SentNotification>();
        sentNotificationEntity.Property(x => x.CreatedBy).IsEncrypted();
        sentNotificationEntity.Property(x => x.LastModifiedBy).IsEncrypted();

        var tokenValueEntity = modelBuilder.Entity<TokenValue>();
        tokenValueEntity.Property(x => x.CreatedBy).IsEncrypted();
        tokenValueEntity.Property(x => x.LastModifiedBy).IsEncrypted();

        var notifiedValueEntity = modelBuilder.Entity<Notified>();
        notifiedValueEntity.Property(x => x.Value).IsEncrypted();
        notifiedValueEntity.Property(x => x.CreatedBy).IsEncrypted();
        notifiedValueEntity.Property(x => x.LastModifiedBy).IsEncrypted();

        modelBuilder.UseEncryption(this._provider);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    private byte[] ConvertStringToByteArray(string value)
    {

        List<byte> bytes = new List<byte>();
        string[] parts = value.Split(',');
        foreach (string part in parts)
        {
            if (byte.TryParse(part, out byte b))
            {
                bytes.Add(b);
            }
        }
        return bytes.ToArray();
    }

    public DbSet<SentNotification> SentNotifications => Set<SentNotification>();
    public DbSet<TokenValue> TokenValues => Set<TokenValue>();
    public DbSet<Notified> Notified => Set<Notified>();
}
