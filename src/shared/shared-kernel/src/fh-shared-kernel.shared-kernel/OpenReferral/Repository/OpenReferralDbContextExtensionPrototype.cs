using FamilyHubs.SharedKernel.OpenReferral.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.SharedKernel.OpenReferral.Repository;

public static class OpenReferralDbContextExtensionPrototype
{
    private const string DedsSchema = "deds";
    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ThirdParty>(entity =>
        {
            entity.ToTable("ThirdParties", schema: DedsSchema);
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(255);
            entity.Property(x => x.OpenReferralUrl).HasMaxLength(255);
            entity.HasMany<ThirdPartyService>()
                .WithOne()
                .HasForeignKey(x => x.ThirdPartyId);

            entity.HasData(
                new ThirdParty() { Id = 1, Name = "MockHsds", OpenReferralUrl = "https://dfe-mock-hsds.com" }
            );
        });
        
        modelBuilder.Entity<ThirdPartyService>(entity =>
        {
            entity.ToTable("ThirdPartyServices", schema: DedsSchema);
            entity.HasKey(x => x.Id);
            entity.HasAlternateKey(x => new { x.ServiceId, x.ThirdPartyId }); 
            entity.HasIndex(x => x.ThirdPartyId);
            entity.HasIndex(x => x.ServiceId);
            entity.Property(x => x.Document).HasColumnType("NVARCHAR(max)");
            entity.Property(x => x.StandardVersionId)
                .HasConversion<byte>();
            
            
            entity.Property(x => x.ServiceId).HasMaxLength(36); // Length of Guid
        });
        
        modelBuilder.Entity<StandardVersion>(entity =>
        {
            entity.ToTable("StandardVersions", schema: DedsSchema);
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(255);
            entity.HasMany<ThirdPartyService>()
                .WithOne()
                .HasForeignKey(x => x.StandardVersionId);
            
            entity.HasData(
                new StandardVersion { Id = (byte)StandardDocumentVersions.OpenReferralUkV2, Name = "Open Referral UK V2" },
                new StandardVersion { Id = (byte)StandardDocumentVersions.OpenReferralInternationalV3, Name = "Open Referral International V3.0" }
            );
        });
    }
}