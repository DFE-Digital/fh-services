using System.Reflection;
using FamilyHubs.ServiceDirectory.Data.DEDS_Temp;
using FamilyHubs.ServiceDirectory.Data.Entities;
using FamilyHubs.ServiceDirectory.Data.Interceptors;
using FamilyHubs.SharedKernel.OpenReferral;
using Enums = FamilyHubs.ServiceDirectory.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Attribute = FamilyHubs.ServiceDirectory.Data.DEDS_Temp.Attribute;
using Contact = FamilyHubs.ServiceDirectory.Data.Entities.Contact;
using CostOption = FamilyHubs.ServiceDirectory.Data.Entities.CostOption;
using Funding = FamilyHubs.ServiceDirectory.Data.Entities.Funding;
using Language = FamilyHubs.ServiceDirectory.Data.Entities.Language;
using Location = FamilyHubs.ServiceDirectory.Data.Entities.Location;
using Schedule = FamilyHubs.ServiceDirectory.Data.Entities.Schedule;
using Service = FamilyHubs.ServiceDirectory.Data.Entities.Service;
using ServiceArea = FamilyHubs.ServiceDirectory.Data.Entities.ServiceArea;
using ServiceAtLocation = FamilyHubs.ServiceDirectory.Data.Entities.ManyToMany.ServiceAtLocation;
using Taxonomy = FamilyHubs.ServiceDirectory.Data.Entities.Taxonomy;

namespace FamilyHubs.ServiceDirectory.Data.Repository
{
    public class ApplicationDbContext : DbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        // TODO: Rename & move this over to the shared kernel along with everything in "DEDS_Temp"
        private static void OnModelCreatingTemp(ModelBuilder modelBuilder)
        {
            // Table Mapping
            modelBuilder.Entity<DEDS_Temp.Accessibility>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Url).HasMaxLength(2048);
            });

            modelBuilder.Entity<DEDS_Temp.Address>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
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

            modelBuilder.Entity<DEDS_Temp.Attribute>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.LinkType).HasMaxLength(50);
                entity.Property(e => e.LinkEntity).HasMaxLength(50);
                entity.Property(e => e.Value).HasMaxLength(50);
            });

            modelBuilder.Entity<DEDS_Temp.Contact>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Title).HasMaxLength(255);
                entity.Property(e => e.Department).HasMaxLength(255);
                entity.Property(e => e.Email).HasMaxLength(255);
            });

            modelBuilder.Entity<DEDS_Temp.CostOption>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.ValidFrom).HasColumnType("date");
                entity.Property(e => e.ValidTo).HasColumnType("date");
                entity.Property(e => e.Currency).HasColumnType("nchar(3)");
                entity.Property(e => e.Amount).HasPrecision(18, 2);
            });

            modelBuilder.Entity<DEDS_Temp.Funding>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<DEDS_Temp.Language>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Code).HasMaxLength(50);
            });

            modelBuilder.Entity<DEDS_Temp.Location>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
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

            modelBuilder.Entity<DEDS_Temp.Metadata>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.ResourceType).HasMaxLength(50);
                entity.Property(e => e.LastActionDate).HasColumnType("date");
                entity.Property(e => e.LastActionType).HasMaxLength(255);
                entity.Property(e => e.FieldName).HasMaxLength(50);
                entity.Property(e => e.UpdatedBy).HasMaxLength(255);
            });

            modelBuilder.Entity<DEDS_Temp.MetaTableDescription>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Language).HasMaxLength(50);
                entity.Property(e => e.CharacterSet).HasMaxLength(50);
            });

            modelBuilder.Entity<DEDS_Temp.Organization>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.AlternateName).HasMaxLength(255);
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.Website).HasMaxLength(2048);
                entity.Property(e => e.LegalStatus).HasMaxLength(255);
                entity.Property(e => e.Logo).HasMaxLength(2048);
                entity.Property(e => e.Uri).HasMaxLength(2048);
            });

            modelBuilder.Entity<DEDS_Temp.OrganizationIdentifier>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.IdentifierScheme).HasMaxLength(50);
                entity.Property(e => e.IdentifierType).HasMaxLength(50);
                entity.Property(e => e.Identifier).HasMaxLength(255);
            });

            modelBuilder.Entity<DEDS_Temp.Phone>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Number).HasMaxLength(50);
                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<DEDS_Temp.Program>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.AlternateName).HasMaxLength(255);
            });

            modelBuilder.Entity<DEDS_Temp.RequiredDocument>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Document).HasMaxLength(255);
                entity.Property(e => e.Uri).HasMaxLength(2048);
            });

            modelBuilder.Entity<DEDS_Temp.Schedule>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
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
                entity.Property(e => e.OpensAt).HasMaxLength(50);
                entity.Property(e => e.ClosesAt).HasMaxLength(50);
                entity.Property(e => e.ScheduleLink).HasMaxLength(2048);
            });

            modelBuilder.Entity<DEDS_Temp.Service>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
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

            modelBuilder.Entity<DEDS_Temp.ServiceArea>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Extent).HasMaxLength(255);
                entity.Property(e => e.ExtentType).HasMaxLength(255);
                entity.Property(e => e.Uri).HasMaxLength(2048);
            });

            modelBuilder.Entity<DEDS_Temp.ServiceAtLocation>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<DEDS_Temp.Taxonomy>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Uri).HasMaxLength(2048);
                entity.Property(e => e.Version).HasMaxLength(50);
            });

            modelBuilder.Entity<DEDS_Temp.TaxonomyTerm>(entity =>
            {
                entity.HasKey(e => e.Id).IsClustered(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Code).HasMaxLength(255);
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Taxonomy).HasMaxLength(255);
                entity.Property(e => e.Version).HasMaxLength(50);
                entity.Property(e => e.Language).HasMaxLength(50);
                entity.Property(e => e.TermUri).HasMaxLength(2048);
            });

            // Relationship Mapping

            //  Accessibility Table

            modelBuilder.Entity<DEDS_Temp.Accessibility>()
                .HasMany(e => e.Attributes)
                .WithOne()
                .HasForeignKey(e => e.LinkId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Accessibility>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            //  Address Table

            modelBuilder.Entity<DEDS_Temp.Address>()
                .HasMany(e => e.Attributes)
                .WithOne()
                .HasForeignKey(e => e.LinkId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Address>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            //  Attribute Table

            modelBuilder.Entity<DEDS_Temp.Attribute>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Attribute>()
                .HasOne(e => e.TaxonomyTerm)
                .WithMany()
                .HasForeignKey(e => e.Id)
                .IsRequired(false);

            //  Contact Table

            modelBuilder.Entity<DEDS_Temp.Contact>()
                .HasMany(e => e.Phones)
                .WithOne()
                .HasForeignKey(e => e.ContactId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Contact>()
                .HasMany(e => e.Attributes)
                .WithOne()
                .HasForeignKey(e => e.LinkId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Contact>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            //  Cost Option Table

            modelBuilder.Entity<DEDS_Temp.CostOption>()
                .HasMany(e => e.Attributes)
                .WithOne()
                .HasForeignKey(e => e.LinkId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.CostOption>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            //  Funding Table

            modelBuilder.Entity<DEDS_Temp.Funding>()
                .HasMany(e => e.Attributes)
                .WithOne()
                .HasForeignKey(e => e.LinkId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Funding>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            //  Language Table

            modelBuilder.Entity<DEDS_Temp.Language>()
                .HasMany(e => e.Attributes)
                .WithOne()
                .HasForeignKey(e => e.LinkId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Language>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            //  Location Table

            modelBuilder.Entity<DEDS_Temp.Location>()
                .HasMany(e => e.Languages)
                .WithOne()
                .HasForeignKey(e => e.LocationId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Location>()
                .HasMany(e => e.Addresses)
                .WithOne()
                .HasForeignKey(e => e.LocationId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Location>()
                .HasMany(e => e.Contacts)
                .WithOne()
                .HasForeignKey(e => e.LocationId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Location>()
                .HasMany(e => e.Accessibility)
                .WithOne()
                .HasForeignKey(e => e.LocationId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Location>()
                .HasMany(e => e.Phones)
                .WithOne()
                .HasForeignKey(e => e.LocationId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Location>()
                .HasMany(e => e.Schedules)
                .WithOne()
                .HasForeignKey(e => e.LocationId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Location>()
                .HasMany(e => e.Attributes)
                .WithOne()
                .HasForeignKey(e => e.LinkId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Location>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            //  Meta Table Description Table

            modelBuilder.Entity<DEDS_Temp.MetaTableDescription>()
                .HasMany(e => e.Attributes)
                .WithOne()
                .HasForeignKey(e => e.LinkId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.MetaTableDescription>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            //  Organization Table

            modelBuilder.Entity<DEDS_Temp.Organization>()
                .HasMany(e => e.Funding)
                .WithOne()
                .HasForeignKey(e => e.OrganizationId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Organization>()
                .HasMany(e => e.Contacts)
                .WithOne()
                .HasForeignKey(e => e.OrganizationId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Organization>()
                .HasMany(e => e.Phones)
                .WithOne()
                .HasForeignKey(e => e.OrganizationId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Organization>()
                .HasMany(e => e.Locations)
                .WithOne()
                .HasForeignKey(e => e.OrganizationId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Organization>()
                .HasMany(e => e.Programs)
                .WithOne()
                .HasForeignKey(e => e.OrganizationId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Organization>()
                .HasMany(e => e.OrganizationIdentifiers)
                .WithOne()
                .HasForeignKey(e => e.OrganizationId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Organization>()
                .HasMany(e => e.Attributes)
                .WithOne()
                .HasForeignKey(e => e.LinkId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Organization>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            //  Organization Identifier Table

            modelBuilder.Entity<DEDS_Temp.OrganizationIdentifier>()
                .HasMany(e => e.Attributes)
                .WithOne()
                .HasForeignKey(e => e.LinkId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.OrganizationIdentifier>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            //  Phone Table

            modelBuilder.Entity<DEDS_Temp.Phone>()
                .HasMany(e => e.Languages)
                .WithOne()
                .HasForeignKey(e => e.PhoneId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Phone>()
                .HasMany(e => e.Attributes)
                .WithOne()
                .HasForeignKey(e => e.LinkId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Phone>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            //  Program Table

            modelBuilder.Entity<DEDS_Temp.Program>()
                .HasMany(e => e.Attributes)
                .WithOne()
                .HasForeignKey(e => e.LinkId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Program>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            //  Required Document Table

            modelBuilder.Entity<DEDS_Temp.RequiredDocument>()
                .HasMany(e => e.Attributes)
                .WithOne()
                .HasForeignKey(e => e.LinkId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.RequiredDocument>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            //  Schedule Table

            modelBuilder.Entity<DEDS_Temp.Schedule>()
                .HasMany(e => e.Attributes)
                .WithOne()
                .HasForeignKey(e => e.LinkId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Schedule>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            //  Service Table

            modelBuilder.Entity<DEDS_Temp.Service>()
                .HasMany(e => e.Phones)
                .WithOne()
                .HasForeignKey(e => e.ServiceId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Service>()
                .HasMany(e => e.Schedules)
                .WithOne()
                .HasForeignKey(e => e.ServiceId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Service>()
                .HasMany(e => e.ServiceAreas)
                .WithOne()
                .HasForeignKey(e => e.ServiceId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Service>()
                .HasMany(e => e.ServiceAtLocations)
                .WithOne()
                .HasForeignKey(e => e.ServiceId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Service>()
                .HasMany(e => e.Languages)
                .WithOne()
                .HasForeignKey(e => e.ServiceId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Service>()
                .HasOne(e => e.Organization)
                .WithMany()
                .HasForeignKey(e => e.Id)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Service>()
                .HasMany(e => e.Funding)
                .WithOne()
                .HasForeignKey(e => e.ServiceId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Service>()
                .HasMany(e => e.CostOptions)
                .WithOne()
                .HasForeignKey(e => e.ServiceId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Service>()
                .HasOne(e => e.Program)
                .WithMany()
                .HasForeignKey(e => e.Id)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Service>()
                .HasMany(e => e.RequiredDocuments)
                .WithOne()
                .HasForeignKey(e => e.ServiceId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Service>()
                .HasMany(e => e.Contacts)
                .WithOne()
                .HasForeignKey(e => e.ServiceId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Service>()
                .HasMany(e => e.Attributes)
                .WithOne()
                .HasForeignKey(e => e.LinkId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.Service>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            // Service Area Table

            modelBuilder.Entity<DEDS_Temp.ServiceArea>()
                .HasMany(e => e.Attributes)
                .WithOne()
                .HasForeignKey(e => e.LinkId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.ServiceArea>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            // Service At Location Table

            modelBuilder.Entity<DEDS_Temp.ServiceAtLocation>()
                .HasMany(e => e.Contacts)
                .WithOne()
                .HasForeignKey(e => e.ServiceAtLocationId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.ServiceAtLocation>()
                .HasMany(e => e.Phones)
                .WithOne()
                .HasForeignKey(e => e.ServiceAtLocationId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.ServiceAtLocation>()
                .HasMany(e => e.Schedules)
                .WithOne()
                .HasForeignKey(e => e.ServiceAtLocationId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.ServiceAtLocation>()
                .HasOne(e => e.Location)
                .WithMany()
                .HasForeignKey(e => e.Id)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.ServiceAtLocation>()
                .HasMany(e => e.Attributes)
                .WithOne()
                .HasForeignKey(e => e.LinkId)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.ServiceAtLocation>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            //  Taxonomy Table

            modelBuilder.Entity<DEDS_Temp.Taxonomy>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);

            //  Taxonomy Term Table

            modelBuilder.Entity<DEDS_Temp.TaxonomyTerm>()
                .HasOne(e => e.TaxonomyDetail)
                .WithMany()
                .HasForeignKey(e => e.Id)
                .IsRequired(false);

            modelBuilder.Entity<DEDS_Temp.TaxonomyTerm>()
                .HasMany(e => e.Metadata)
                .WithOne()
                .HasForeignKey(e => e.ResourceId)
                .IsRequired(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Service>()
                .HasIndex(e => new { e.OrganisationId, e.Id })
                .IsUnique(false)
                .HasDatabaseName("IX_Services_OrganisationId_Id")
                .IncludeProperties(e => new { e.ServiceType, e.Status });

            modelBuilder.Entity<Contact>()
                .HasIndex(e => e.ServiceId)
                .IsUnique(false)
                .HasDatabaseName("IX_Contacts_ServiceId_Id")
                .IncludeProperties(e => new { e.Id, e.Title, e.Name, e.Telephone, e.TextPhone, e.Url, e.Email });

            modelBuilder.Entity<Service>()
                .HasIndex(e => new { e.ServiceType, e.Id, e.OrganisationId, e.Status })
                .IsUnique(false)
                .HasDatabaseName("IX_ServiceType_OrganisationId_Status");

            modelBuilder.Entity<ServiceDelivery>(entity =>
            {
                entity.HasIndex(e => new { e.Name, e.ServiceId, e.Id })
                    .HasDatabaseName("IX_ServiceDeliveryNonClustered")
                    .IsUnique(false)
                    .IsClustered(false);
            });

            modelBuilder.Entity<ServiceSearch>()
                .ToTable("ServiceSearches")
                .HasKey(e => e.Id);

            modelBuilder.Entity<ServiceSearch>()
                .Property(e => e.SearchTriggerEventId)
                .HasConversion<short>();
            
            modelBuilder.Entity<ServiceSearch>()
                .Property(e => e.ServiceSearchTypeId)
                .HasConversion<byte>();

            modelBuilder.Entity<ServiceSearch>()
                .Property(e => e.SearchPostcode)
                .HasMaxLength(10);
            
            modelBuilder.Entity<ServiceSearch>()
                .HasOne(e => e.SearchTriggerEvent)
                .WithMany(e => e.ServiceSearches)
                .HasForeignKey(e => e.SearchTriggerEventId)
                .IsRequired(false);
            
            modelBuilder.Entity<ServiceSearch>()
                .HasOne(e => e.ServiceSearchType)
                .WithMany(e => e.ServiceSearches)
                .HasForeignKey(e => e.ServiceSearchTypeId)
                .IsRequired(true);
            
            modelBuilder.Entity<ServiceSearch>()
                .Property(e => e.CorrelationId)
                .HasMaxLength(50);

            modelBuilder.Entity<ServiceSearchResult>()
                .ToTable("ServiceSearchResults")
                .HasKey(e => e.Id);

            modelBuilder.Entity<ServiceSearchResult>()
                .HasOne(e => e.Service)
                .WithMany(e => e.ServiceSearchResults)
                .HasForeignKey(e => e.ServiceId)
                // Do not delete metrics if service deleted
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Event>()
                .ToTable("Events")
                .HasKey(e => e.Id);

            modelBuilder.Entity<Event>()
                .Property(e => e.Id)
                .HasConversion<short>();
            
            modelBuilder.Entity<Event>()
                .Property(e => e.Name)
                .HasMaxLength(100);
            
            modelBuilder.Entity<Event>()
                .Property(e => e.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<Event>()
                .HasData(
                    new()
                    {
                        Id = Enums.ServiceDirectorySearchEventType.ServiceDirectoryInitialSearch,
                        Name = nameof(Enums.ServiceDirectorySearchEventType.ServiceDirectoryInitialSearch),
                        Description = "Describes an initial, unfiltered search by a user."
                    },
                    new()
                    {
                        Id = Enums.ServiceDirectorySearchEventType.ServiceDirectorySearchFilter,
                        Name = nameof(Enums.ServiceDirectorySearchEventType.ServiceDirectorySearchFilter),
                        Description = "Describes a filtered search by a user."
                    }
                );

            modelBuilder.Entity<ServiceType>()
                .ToTable("ServiceTypes")
                .HasKey(e => e. Id);

            modelBuilder.Entity<ServiceType>()
                .Property(e => e.Id)
                .HasConversion<byte>();
            
            modelBuilder.Entity<ServiceType>()
                .Property(e => e.Name)
                .HasMaxLength(50);
            
            modelBuilder.Entity<ServiceType>()
                .Property(e => e.Description)
                .HasMaxLength(255);

            modelBuilder.Entity<ServiceType>()
                .HasData(
                    new ServiceType
                    {
                        Id = Enums.ServiceType.FamilyExperience,
                        Name = nameof(Enums.ServiceType.FamilyExperience),
                        Description = "Find"
                    },
                    new ServiceType 
                    {
                        Id = Enums.ServiceType.InformationSharing,
                        Name = nameof(Enums.ServiceType.InformationSharing),
                        Description = "Connect"
                    }
                );

            // TODO: Remove this and update migration
            modelBuilder.Entity<ServicesTemp>()
                .ToTable("services_temp", "staging")
                .HasKey(e => e.Id);

            OnModelCreatingTemp(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (System.Diagnostics.Debugger.IsAttached) optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        public DbSet<AccessibilityForDisabilities> AccessibilityForDisabilities => Set<AccessibilityForDisabilities>();
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<CostOption> CostOptions => Set<CostOption>();
        public DbSet<Eligibility> Eligibilities => Set<Eligibility>();
        public DbSet<Funding> Fundings => Set<Funding>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Organisation> Organisations => Set<Organisation>();
        public DbSet<Schedule> Schedules => Set<Schedule>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<ServiceAtLocation> ServiceAtLocations => Set<ServiceAtLocation>();
        public DbSet<ServiceArea> ServiceAreas => Set<ServiceArea>();
        public DbSet<ServiceDelivery> ServiceDeliveries => Set<ServiceDelivery>();
        public DbSet<Taxonomy> Taxonomies => Set<Taxonomy>();
        public DbSet<ServiceSearch> ServiceSearches => Set<ServiceSearch>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<ServiceSearchResult> ServiceSearchResult => Set<ServiceSearchResult>();
        public DbSet<ServiceType> ServiceTypes => Set<ServiceType>();
    }
}
