using FamilyHubs.SharedKernel.OpenReferral.Entities;
using Microsoft.EntityFrameworkCore;
using Attribute = FamilyHubs.SharedKernel.OpenReferral.Entities.Attribute;

namespace FamilyHubs.SharedKernel.OpenReferral.Repository;

public static class OpenReferralDbContextExtension
{
    private const string Deds = "deds";
    private const string DedsMeta = "dedsmeta";

    private static void CreateTableMapping(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accessibility>(entity =>
        {
            entity.ToTable("Accessibilities", schema: Deds);
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Url).HasMaxLength(2048);
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Addresses", schema: Deds);
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
            entity.ToTable("Attributes", schema: Deds);
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.LinkType).HasMaxLength(50);
            entity.Property(e => e.LinkEntity).HasMaxLength(50);
            entity.Property(e => e.Value).HasMaxLength(50);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("Contacts", schema: Deds);
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Department).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
        });

        modelBuilder.Entity<CostOption>(entity =>
        {
            entity.ToTable("CostOptions", schema: Deds);
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ValidFrom).HasColumnType("date");
            entity.Property(e => e.ValidTo).HasColumnType("date");
            entity.Property(e => e.Currency).HasColumnType("nchar(3)");
            entity.Property(e => e.Amount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Funding>(entity =>
        {
            entity.ToTable("Fundings", schema: Deds);
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.ToTable("Languages", schema: Deds);
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Code).HasMaxLength(50);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Locations", schema: Deds);
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
            entity.ToTable("Metadata", schema: Deds);
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ResourceType).HasMaxLength(50);
            entity.Property(e => e.LastActionDate).HasColumnType("date");
            entity.Property(e => e.LastActionType).HasMaxLength(255);
            entity.Property(e => e.FieldName).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasMaxLength(255);
        });

        modelBuilder.Entity<MetaTableDescription>(entity =>
        {
            entity.ToTable("MetaTableDescriptions", schema: Deds);
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Language).HasMaxLength(50);
            entity.Property(e => e.CharacterSet).HasMaxLength(50);
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.ToTable("Organizations", schema: Deds);
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
            entity.ToTable("OrganizationIdentifiers", schema: Deds);
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.IdentifierScheme).HasMaxLength(50);
            entity.Property(e => e.IdentifierType).HasMaxLength(50);
            entity.Property(e => e.Identifier).HasMaxLength(255);
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.ToTable("Phones", schema: Deds);
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Number).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Program>(entity =>
        {
            entity.ToTable("Programs", schema: Deds);
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.AlternateName).HasMaxLength(255);
        });

        modelBuilder.Entity<RequiredDocument>(entity =>
        {
            entity.ToTable("RequiredDocuments", schema: Deds);
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Uri).HasMaxLength(2048);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("Schedules", schema: Deds);
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ValidFrom).HasColumnType("date");
            entity.Property(e => e.ValidTo).HasColumnType("date");
            entity.Property(e => e.DtStart).HasMaxLength(50);
            entity.Property(e => e.Until).HasMaxLength(50);
            entity.Property(e => e.Wkst).HasMaxLength(50);
            entity.Property(e => e.Freq).HasMaxLength(50);
            entity.Property(e => e.ByDay).HasMaxLength(255);
            entity.Property(e => e.ByWeekNo).HasMaxLength(255);
            entity.Property(e => e.ByMonthDay).HasMaxLength(255);
            entity.Property(e => e.ByYearDay).HasMaxLength(255);
            entity.Property(e => e.OpensAt).HasColumnType("time");
            entity.Property(e => e.ClosesAt).HasColumnType("time");
            entity.Property(e => e.ScheduleLink).HasMaxLength(2048);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Services", schema: Deds);
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
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
            entity.ToTable("ServiceAreas", schema: Deds);
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Extent).HasMaxLength(255);
            entity.Property(e => e.ExtentType).HasMaxLength(255);
            entity.Property(e => e.Uri).HasMaxLength(2048);
        });

        modelBuilder.Entity<ServiceAtLocation>(entity =>
        {
            entity.ToTable("ServiceAtLocations", schema: Deds);
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Taxonomy>(entity =>
        {
            entity.ToTable("Taxonomies", schema: Deds);
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Uri).HasMaxLength(2048);
            entity.Property(e => e.Version).HasMaxLength(50);
        });

        modelBuilder.Entity<TaxonomyTerm>(entity =>
        {
            entity.ToTable("TaxonomyTerms", schema: Deds);
            entity.HasKey(e => e.Id).IsClustered(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Code).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Taxonomy).HasMaxLength(255);
            entity.Property(e => e.Language).HasMaxLength(50);
            entity.Property(e => e.TermUri).HasMaxLength(2048);
        });
    }

    private static void CreateEntityAttributeRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accessibility>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Accessibility) + nameof(Attribute) + "s");
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Address>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Address) + nameof(Attribute) + "s");
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Contact>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Contact) + nameof(Attribute) + "s");
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<CostOption>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(CostOption) + nameof(Attribute) + "s");
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Funding>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Funding) + nameof(Attribute) + "s");
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Language>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Language) + nameof(Attribute) + "s");
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Location>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Location) + nameof(Attribute) + "s");
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<MetaTableDescription>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(MetaTableDescription) + nameof(Attribute) + "s");
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Organization>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Organization) + nameof(Attribute) + "s");
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<OrganizationIdentifier>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(OrganizationIdentifier) + nameof(Attribute) + "s");
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Phone>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Phone) + nameof(Attribute) + "s");
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Program>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Program) + nameof(Attribute) + "s");
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<RequiredDocument>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(RequiredDocument) + nameof(Attribute) + "s");
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Schedule>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Schedule) + nameof(Attribute) + "s");
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Service>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Service) + nameof(Attribute) + "s");
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<ServiceArea>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(ServiceArea) + nameof(Attribute) + "s");
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<ServiceAtLocation>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(ServiceAtLocation) + nameof(Attribute) + "s");
                    e.Metadata.SetSchema(DedsMeta);
                });
    }

    private static void CreateEntityMetadataRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accessibility>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Accessibility) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Address>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Address) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Attribute>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Attribute) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Contact>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Contact) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<CostOption>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(CostOption) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Funding>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Funding) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Language>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Language) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Location>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Location) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<MetaTableDescription>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(MetaTableDescription) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Organization>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Organization) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<OrganizationIdentifier>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(OrganizationIdentifier) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Phone>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Phone) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Program>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Program) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<RequiredDocument>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(RequiredDocument) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Schedule>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Schedule) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Service>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Service) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<ServiceArea>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(ServiceArea) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<ServiceAtLocation>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(ServiceAtLocation) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<Taxonomy>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(Taxonomy) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });

        modelBuilder.Entity<TaxonomyTerm>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e =>
                {
                    e.Metadata.SetTableName(nameof(TaxonomyTerm) + nameof(Metadata));
                    e.Metadata.SetSchema(DedsMeta);
                });
    }

    private static void CreateOrganizationRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Organization>()
            .HasMany<Service>()
            .WithOne(e => e.Organization)
            .HasForeignKey(e => e.OrganizationId);

        modelBuilder.Entity<Organization>()
            .HasMany<Location>(e => e.Locations)
            .WithOne()
            .HasForeignKey(e => e.OrganizationId);

        modelBuilder.Entity<Organization>()
            .HasMany<Program>(e => e.Programs)
            .WithOne()
            .HasForeignKey(e => e.OrganizationId);

        modelBuilder.Entity<Organization>()
            .HasMany<Funding>(e => e.Funding)
            .WithOne()
            .HasForeignKey(e => e.OrganizationId);

        modelBuilder.Entity<Organization>()
            .HasMany<OrganizationIdentifier>(e => e.OrganizationIdentifiers)
            .WithOne()
            .HasForeignKey(e => e.OrganizationId);

        modelBuilder.Entity<Organization>()
            .HasMany<Phone>(e => e.Phones)
            .WithOne()
            .HasForeignKey(e => e.OrganizationId);

        modelBuilder.Entity<Organization>()
            .HasMany<Contact>(e => e.Contacts)
            .WithOne()
            .HasForeignKey(e => e.OrganizationId);
    }

    private static void CreateServiceRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Service>()
            .HasMany<CostOption>(e => e.CostOptions)
            .WithOne()
            .HasForeignKey(e => e.ServiceId);

        modelBuilder.Entity<Service>()
            .HasMany<ServiceArea>(e => e.ServiceAreas)
            .WithOne()
            .HasForeignKey(e => e.ServiceId);

        modelBuilder.Entity<Service>()
            .HasMany<RequiredDocument>(e => e.RequiredDocuments)
            .WithOne()
            .HasForeignKey(e => e.ServiceId);

        modelBuilder.Entity<Service>()
            .HasMany<Funding>(e => e.Funding)
            .WithOne()
            .HasForeignKey(e => e.ServiceId);

        modelBuilder.Entity<Service>()
            .HasMany<Schedule>(e => e.Schedules)
            .WithOne()
            .HasForeignKey(e => e.ServiceId);

        modelBuilder.Entity<Service>()
            .HasMany<ServiceAtLocation>(e => e.ServiceAtLocations)
            .WithOne()
            .HasForeignKey(e => e.ServiceId);

        modelBuilder.Entity<Service>()
            .HasMany<Contact>(e => e.Contacts)
            .WithOne()
            .HasForeignKey(e => e.ServiceId);

        modelBuilder.Entity<Service>()
            .HasMany<Phone>(e => e.Phones)
            .WithOne()
            .HasForeignKey(e => e.ServiceId);

        modelBuilder.Entity<Service>()
            .HasMany<Language>(e => e.Languages)
            .WithOne()
            .HasForeignKey(e => e.ServiceId);
    }

    private static void CreateLocationRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>()
            .HasMany<Accessibility>(e => e.Accessibilities)
            .WithOne()
            .HasForeignKey(e => e.LocationId);

        modelBuilder.Entity<Location>()
            .HasMany<Address>(e => e.Addresses)
            .WithOne()
            .HasForeignKey(e => e.LocationId);

        modelBuilder.Entity<Location>()
            .HasMany<Phone>(e => e.Phones)
            .WithOne()
            .HasForeignKey(e => e.LocationId);

        modelBuilder.Entity<Location>()
            .HasMany<Language>(e => e.Languages)
            .WithOne()
            .HasForeignKey(e => e.LocationId);

        modelBuilder.Entity<Location>()
            .HasMany<Schedule>(e => e.Schedules)
            .WithOne()
            .HasForeignKey(e => e.LocationId);

        modelBuilder.Entity<Location>()
            .HasMany<ServiceAtLocation>()
            .WithOne(e => e.Location)
            .HasForeignKey(e => e.LocationId);
    }

    private static void CreateServiceAtLocationRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ServiceAtLocation>()
            .HasMany<Schedule>(e => e.Schedules)
            .WithOne()
            .HasForeignKey(e => e.ServiceAtLocationId);

        modelBuilder.Entity<ServiceAtLocation>()
            .HasMany<Contact>(e => e.Contacts)
            .WithOne()
            .HasForeignKey(e => e.ServiceAtLocationId);

        modelBuilder.Entity<ServiceAtLocation>()
            .HasMany<Phone>(e => e.Phones)
            .WithOne()
            .HasForeignKey(e => e.ServiceAtLocationId);
    }

    private static void CreateProgramRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Program>()
            .HasMany<Service>()
            .WithOne(e => e.Program)
            .HasForeignKey(e => e.ProgramId);
    }

    private static void CreateContactRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .HasMany<Phone>(e => e.Phones)
            .WithOne()
            .HasForeignKey(e => e.ContactId);
    }

    private static void CreatePhoneRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Phone>()
            .HasMany<Language>(e => e.Languages)
            .WithOne()
            .HasForeignKey(e => e.PhoneId);
    }

    private static void CreateAttributeRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attribute>()
            .HasOne<TaxonomyTerm>(e => e.TaxonomyTerm)
            .WithOne();
    }

    private static void CreateTaxonomyTermRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaxonomyTerm>()
            .HasOne<Taxonomy>(e => e.TaxonomyDetail)
            .WithOne();
    }

    private static void CreateNavigationAutoIncludes(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accessibility>().Navigation(e => e.Attributes).AutoInclude();
        modelBuilder.Entity<Accessibility>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<Address>().Navigation(e => e.Attributes).AutoInclude();
        modelBuilder.Entity<Address>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<Attribute>().Navigation(e => e.TaxonomyTerm).AutoInclude();
        modelBuilder.Entity<Attribute>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<Contact>().Navigation(e => e.Phones).AutoInclude();
        modelBuilder.Entity<Contact>().Navigation(e => e.Attributes).AutoInclude();
        modelBuilder.Entity<Contact>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<CostOption>().Navigation(e => e.Attributes).AutoInclude();
        modelBuilder.Entity<CostOption>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<Funding>().Navigation(e => e.Attributes).AutoInclude();
        modelBuilder.Entity<Funding>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<Language>().Navigation(e => e.Attributes).AutoInclude();
        modelBuilder.Entity<Language>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<Location>().Navigation(e => e.Languages).AutoInclude();
        modelBuilder.Entity<Location>().Navigation(e => e.Addresses).AutoInclude();
        modelBuilder.Entity<Location>().Navigation(e => e.Contacts).AutoInclude();
        modelBuilder.Entity<Location>().Navigation(e => e.Accessibilities).AutoInclude();
        modelBuilder.Entity<Location>().Navigation(e => e.Phones).AutoInclude();
        modelBuilder.Entity<Location>().Navigation(e => e.Schedules).AutoInclude();
        modelBuilder.Entity<Location>().Navigation(e => e.Attributes).AutoInclude();
        modelBuilder.Entity<Location>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<MetaTableDescription>().Navigation(e => e.Attributes).AutoInclude();
        modelBuilder.Entity<MetaTableDescription>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<Organization>().Navigation(e => e.Funding).AutoInclude();
        modelBuilder.Entity<Organization>().Navigation(e => e.Contacts).AutoInclude();
        modelBuilder.Entity<Organization>().Navigation(e => e.Phones).AutoInclude();
        modelBuilder.Entity<Organization>().Navigation(e => e.Locations).AutoInclude();
        modelBuilder.Entity<Organization>().Navigation(e => e.Programs).AutoInclude();
        modelBuilder.Entity<Organization>().Navigation(e => e.OrganizationIdentifiers).AutoInclude();
        modelBuilder.Entity<Organization>().Navigation(e => e.Attributes).AutoInclude();
        modelBuilder.Entity<Organization>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<OrganizationIdentifier>().Navigation(e => e.Attributes).AutoInclude();
        modelBuilder.Entity<OrganizationIdentifier>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<Phone>().Navigation(e => e.Languages).AutoInclude();
        modelBuilder.Entity<Phone>().Navigation(e => e.Attributes).AutoInclude();
        modelBuilder.Entity<Phone>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<Program>().Navigation(e => e.Attributes).AutoInclude();
        modelBuilder.Entity<Program>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<RequiredDocument>().Navigation(e => e.Attributes).AutoInclude();
        modelBuilder.Entity<RequiredDocument>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<Schedule>().Navigation(e => e.Attributes).AutoInclude();
        modelBuilder.Entity<Schedule>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<Service>().Navigation(e => e.Phones).AutoInclude();
        modelBuilder.Entity<Service>().Navigation(e => e.Schedules).AutoInclude();
        modelBuilder.Entity<Service>().Navigation(e => e.ServiceAreas).AutoInclude();
        modelBuilder.Entity<Service>().Navigation(e => e.ServiceAtLocations).AutoInclude();
        modelBuilder.Entity<Service>().Navigation(e => e.Languages).AutoInclude();
        modelBuilder.Entity<Service>().Navigation(e => e.Organization).AutoInclude();
        modelBuilder.Entity<Service>().Navigation(e => e.Funding).AutoInclude();
        modelBuilder.Entity<Service>().Navigation(e => e.CostOptions).AutoInclude();
        modelBuilder.Entity<Service>().Navigation(e => e.Program).AutoInclude();
        modelBuilder.Entity<Service>().Navigation(e => e.RequiredDocuments).AutoInclude();
        modelBuilder.Entity<Service>().Navigation(e => e.Contacts).AutoInclude();
        modelBuilder.Entity<Service>().Navigation(e => e.Attributes).AutoInclude();
        modelBuilder.Entity<Service>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<ServiceArea>().Navigation(e => e.Attributes).AutoInclude();
        modelBuilder.Entity<ServiceArea>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<ServiceAtLocation>().Navigation(e => e.Contacts).AutoInclude();
        modelBuilder.Entity<ServiceAtLocation>().Navigation(e => e.Phones).AutoInclude();
        modelBuilder.Entity<ServiceAtLocation>().Navigation(e => e.Schedules).AutoInclude();
        modelBuilder.Entity<ServiceAtLocation>().Navigation(e => e.Location).AutoInclude();
        modelBuilder.Entity<ServiceAtLocation>().Navigation(e => e.Attributes).AutoInclude();
        modelBuilder.Entity<ServiceAtLocation>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<Taxonomy>().Navigation(e => e.Metadata).AutoInclude();

        modelBuilder.Entity<TaxonomyTerm>().Navigation(e => e.TaxonomyDetail).AutoInclude();
        modelBuilder.Entity<TaxonomyTerm>().Navigation(e => e.Metadata).AutoInclude();
    }

    /*
     *
     * -- Table Mapping --
     *
     * These are the initial table mappings, defining the keys and things such as data types and string lengths.
     *
     * -- Core Object Relationships --
     *
     * The schema defines four core objects / relationships: Organization, Service, Location, ServiceAtLocation.
     *
     * These are the "top level" objects in the schema and so have one-to-many relationships with most other
     * objects, e.g., "one Service has many Phones", so most of the information about one of these core objects
     * is held across the different sub-objects, so that's why the split has been done in this way logically.
     *
     * -- Other Object Relationships --
     *
     * Most other objects simply have one-to-many relationships with the core objects, but there are a small number
     * with additional information about them: Program, Phone, Contact. That is because, for example, "one Phone
     * has many Languages", so those additional relationships are mapped afterwards logically.
     *
     * -- Attributes & Metadata Relationships --
     *
     * Each of the core and other objects have many attributes and/or metadata. The schema is incomplete on
     * how to represent these relationships, so we have decided to map them out as many-to-many. These have however
     * been normalised by EF Core, and so in a separate schema there will be intermediate one-to-many tables, e.g.,
     * "AttributeService", "MetadataService", "AttributeSchedule", "MetadataSchedule", etc.
     *
     * -- Navigations --
     *
     * Necessary for traversing the full entity graph when reading data out from the Db
     *
     */
    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Table Mapping
        CreateTableMapping(modelBuilder);


        // Core Relationships
        CreateOrganizationRelationships(modelBuilder);
        CreateServiceRelationships(modelBuilder);
        CreateLocationRelationships(modelBuilder);
        CreateServiceAtLocationRelationships(modelBuilder);

        // Other Relationships
        CreatePhoneRelationships(modelBuilder);
        CreateProgramRelationships(modelBuilder);
        CreateContactRelationships(modelBuilder);
        CreateAttributeRelationships(modelBuilder);
        CreateTaxonomyTermRelationships(modelBuilder);

        // Attributes & Metadata Relationships
        CreateEntityAttributeRelationships(modelBuilder);
        CreateEntityMetadataRelationships(modelBuilder);


        // Navigations
        CreateNavigationAutoIncludes(modelBuilder);
    }
}