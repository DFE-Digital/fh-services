using FamilyHubs.OpenReferral.Function.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Attribute = FamilyHubs.OpenReferral.Function.Repository.Entities.Attribute;

namespace FamilyHubs.OpenReferral.Function.Repository;

public class OpenReferralDbContextExtension
{
    public void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Table Mapping

        modelBuilder.Entity<Accessibility>(entity =>
        {
            entity.ToTable(nameof(Accessibility), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Url).HasMaxLength(2048);
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable(nameof(Address), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Attention).HasMaxLength(255);
            entity.Property(e => e.Address1).HasMaxLength(255);
            entity.Property(e => e.Address2).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Region).HasMaxLength(255);
            entity.Property(e => e.StateProvince).HasMaxLength(255);
            entity.Property(e => e.PostalCode).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.AddressType).HasMaxLength(255);
        });

        modelBuilder.Entity<Attribute>(entity =>
        {
            entity.ToTable(nameof(Attribute), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            //entity.Property(e => e.LinkId).IsRequired();
            //entity.Property(e => e.TaxonomyTermId).IsRequired();
            entity.Property(e => e.LinkType).HasMaxLength(50);
            entity.Property(e => e.LinkEntity).HasMaxLength(50);
            entity.Property(e => e.Value).HasMaxLength(50);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable(nameof(Contact), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Department).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
        });

        modelBuilder.Entity<CostOption>(entity =>
        {
            entity.ToTable(nameof(CostOption), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            //entity.Property(e => e.ServiceId).IsRequired();
            entity.Property(e => e.ValidFrom).HasColumnType("date");
            entity.Property(e => e.ValidTo).HasColumnType("date");
            entity.Property(e => e.Currency).HasColumnType("nchar(3)");
            entity.Property(e => e.Amount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Funding>(entity =>
        {
            entity.ToTable(nameof(Funding), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.ToTable(nameof(Language), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Code).HasMaxLength(50);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable(nameof(Location), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.LocationType).HasMaxLength(255);
            entity.Property(e => e.LocationType).HasMaxLength(2048);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.AlternateName).HasMaxLength(255);
            entity.Property(e => e.Transportation).HasMaxLength(255);
            entity.Property(e => e.Latitude).HasPrecision(18, 2);
            entity.Property(e => e.Longitude).HasPrecision(18, 2);
            entity.Property(e => e.ExternalIdentifier).HasMaxLength(255);
            entity.Property(e => e.ExternalIdentifierType).HasMaxLength(255);
        });

        modelBuilder.Entity<Metadata>(entity =>
        {
            entity.ToTable(nameof(Metadata), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            //entity.Property(e => e.ResourceId).IsRequired();
            entity.Property(e => e.ResourceType).HasMaxLength(50);
            entity.Property(e => e.LastActionDate).HasColumnType("date");
            entity.Property(e => e.LastActionType).HasMaxLength(255);
            entity.Property(e => e.FieldName).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasMaxLength(255);
        });

        modelBuilder.Entity<MetaTableDescription>(entity =>
        {
            entity.ToTable(nameof(MetaTableDescription), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Language).HasMaxLength(50);
            entity.Property(e => e.CharacterSet).HasMaxLength(50);
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.ToTable(nameof(Organization), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.AlternateName).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Website).HasMaxLength(2048);
            entity.Property(e => e.LegalStatus).HasMaxLength(255);
            entity.Property(e => e.Logo).HasMaxLength(2048);
            entity.Property(e => e.Uri).HasMaxLength(2048);
        });

        modelBuilder.Entity<OrganizationIdentifier>(entity =>
        {
            entity.ToTable(nameof(OrganizationIdentifier), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.IdentifierScheme).HasMaxLength(50);
            entity.Property(e => e.IdentifierType).HasMaxLength(50);
            entity.Property(e => e.Identifier).HasMaxLength(255);
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.ToTable(nameof(Phone), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Number).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Entities.Program>(entity =>
        {
            entity.ToTable(nameof(Program), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.AlternateName).HasMaxLength(255);
        });

        modelBuilder.Entity<RequiredDocument>(entity =>
        {
            entity.ToTable(nameof(RequiredDocument), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Document).HasMaxLength(255);
            entity.Property(e => e.Uri).HasMaxLength(2048);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable(nameof(Schedule), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ValidFrom).HasColumnType("date");
            entity.Property(e => e.ValidTo).HasColumnType("date");
            entity.Property(e => e.Dtstart).HasMaxLength(50);
            entity.Property(e => e.Until).HasMaxLength(50);
            entity.Property(e => e.Wkst).HasMaxLength(50);
            entity.Property(e => e.Freq).HasMaxLength(50);
            entity.Property(e => e.Byday).HasMaxLength(255);
            entity.Property(e => e.Byweekno).HasMaxLength(255);
            entity.Property(e => e.Bymonthday).HasMaxLength(255);
            entity.Property(e => e.Byyearday).HasMaxLength(255);
            entity.Property(e => e.OpensAt).HasColumnType("time");
            entity.Property(e => e.ClosesAt).HasColumnType("time");
            entity.Property(e => e.ScheduleLink).HasMaxLength(2048);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable(nameof(Service), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            //entity.Property(e => e.ProgramId).IsRequired();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.AlternateName).HasMaxLength(255);
            entity.Property(e => e.Url).HasMaxLength(2048);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.InterpretationServices).HasMaxLength(512);
            entity.Property(e => e.ApplicationProcess).HasMaxLength(512);
            entity.Property(e => e.AssuredDate).HasColumnType("date");
            entity.Property(e => e.AssurerEmail).HasMaxLength(255);
            entity.Property(e => e.Alert).HasMaxLength(255);
            entity.Property(e => e.LastModified).HasPrecision(7);
        });

        modelBuilder.Entity<ServiceArea>(entity =>
        {
            entity.ToTable(nameof(ServiceArea), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Extent).HasMaxLength(255);
            entity.Property(e => e.ExtentType).HasMaxLength(255);
            entity.Property(e => e.Uri).HasMaxLength(2048);
        });

        modelBuilder.Entity<ServiceAtLocation>(entity =>
        {
            entity.ToTable(nameof(ServiceAtLocation), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Taxonomy>(entity =>
        {
            entity.ToTable(nameof(Taxonomy), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Uri).HasMaxLength(2048);
            entity.Property(e => e.Version).HasMaxLength(50);
        });

        modelBuilder.Entity<TaxonomyTerm>(entity =>
        {
            entity.ToTable(nameof(TaxonomyTerm), schema: "deds");
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Code).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Taxonomy).HasMaxLength(255);
            entity.Property(e => e.Version).HasMaxLength(50);
            entity.Property(e => e.Language).HasMaxLength(50);
            entity.Property(e => e.TermUri).HasMaxLength(2048);
        });
    }
}