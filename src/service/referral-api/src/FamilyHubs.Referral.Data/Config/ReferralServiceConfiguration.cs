using FamilyHubs.Referral.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyHubs.Referral.Data.Config;

public class ReferralServiceConfiguration : IEntityTypeConfiguration<Entities.ReferralService>
{
    public void Configure(EntityTypeBuilder<Entities.ReferralService> builder)
    {
        builder.Navigation(e => e.Organisation).AutoInclude();

        builder.HasOne<Organisation>(e => e.Organisation)
            .WithMany()
            .HasForeignKey(e => e.OrganizationId)
            .IsRequired();

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(t => t.Created)
            .IsRequired();
        builder.Property(t => t.CreatedBy)
            .HasMaxLength(MaxLength.Email)
            .IsRequired();

        builder.Property(t => t.LastModifiedBy)
            .HasMaxLength(MaxLength.Email);
    }
}

