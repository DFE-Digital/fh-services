using FamilyHubs.OpenReferral.Function.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using static FamilyHubs.OpenReferral.Function.Repository.OpenReferralSchemaConstants;
using Attribute = FamilyHubs.OpenReferral.Function.Repository.Entities.Attribute;

namespace FamilyHubs.OpenReferral.Function.Repository;

public class FunctionDbContext(DbContextOptions<FunctionDbContext> options) : DbContext(options), IFunctionDbContext
{
    private DbSet<Service> Services { get; init; } = null!;
    IQueryable<Service> IFunctionDbContext.Services => Services.AsSplitQuery();

    public void AddService(Service service)
    {
        ChangeTracker.TrackGraph(service, node =>
        {
            if (!node.Entry.IsKeySet)
            {
                node.Entry.State = EntityState.Added;
            }
        });
    }

    // TODO: Update for new schema -> TruncateDEDSSchemaAsync
    // Just a dummy statement for now since the [staging].[services_temp] no longer exists :)
    public Task TruncateServicesTempAsync()
    {
        return Task.CompletedTask;
        ////  return Database.ExecuteSqlRawAsync("SELECT TOP (1) [Id] FROM [deds].[Service]");
    }

    public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

    private static void CreateEntityAttributeRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accessibility>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Address>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Contact>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<CostOption>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Funding>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Language>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Location>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<MetaTableDescription>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Organization>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<OrganizationIdentifier>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Phone>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Entities.Program>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<RequiredDocument>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Schedule>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Service>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<ServiceArea>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<ServiceAtLocation>()
            .HasMany<Attribute>(e => e.Attributes)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });
    }

    private static void CreateEntityMetadataRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accessibility>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Address>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Attribute>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Contact>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<CostOption>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Funding>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Language>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Location>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<MetaTableDescription>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Organization>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<OrganizationIdentifier>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Phone>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Entities.Program>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<RequiredDocument>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Schedule>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Service>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<ServiceArea>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<ServiceAtLocation>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<Taxonomy>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });

        modelBuilder.Entity<TaxonomyTerm>()
            .HasMany<Metadata>(e => e.Metadata)
            .WithMany().UsingEntity<Dictionary<string, object>>(
                e => { e.Metadata.SetSchema(DedsMeta); });
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
            .HasMany<Entities.Program>(e => e.Programs)
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
            .WithOne(e => e.Location)
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
        modelBuilder.Entity<Entities.Program>()
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

        modelBuilder.Entity<Entities.Program>().Navigation(e => e.Attributes).AutoInclude();
        modelBuilder.Entity<Entities.Program>().Navigation(e => e.Metadata).AutoInclude();

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OpenReferralDbContextExtension openReferralDbContextExtension = new();
        openReferralDbContextExtension.OnModelCreating(modelBuilder);

        /*
         *
         * -- Core Objects --
         *
         * The schema defines four core objects / relationships: Organization, Service, Location, ServiceAtLocation.
         *
         * These are the "top level" objects in the schema and so have one-to-many relationships with most other
         * objects, e.g., "one Service has many Phones", so most of the information about one of these core objects
         * is held across the different sub-objects, so that's why the split has been done in this way logically.
         *
         * -- Other Objects --
         *
         * Most other objects simply have one-to-many relationships with the core objects, but there are a small number
         * with additional information about them: Program, Phone, Contact. That is because, for example, "one Phone
         * has many Languages", so those additional relationships are mapped afterwards logically.
         *
         * -- Attributes & Metadata --
         *
         * Each of the core and other objects have many attributes and/or metadata. The schema is incomplete on
         * how to represent these relationships, so we have decided to map them out as many-to-many. These have however
         * been normalised by EF Core, and so in a separate schema there will be intermediate one-to-many tables, e.g.,
         * "AttributeService", "MetadataService", "AttributeSchedule", "MetadataSchedule", etc.
         *
         */

        // Core Relationships

        CreateOrganizationRelationships(modelBuilder);
        CreateServiceRelationships(modelBuilder);
        CreateLocationRelationships(modelBuilder);
        CreateServiceAtLocationRelationships(modelBuilder);

        // Other Relationships

        CreatePhoneRelationships(modelBuilder);
        CreateProgramRelationships(modelBuilder);
        CreateContactRelationships(modelBuilder);

        // Attributes & Metadata Relationships

        CreateEntityAttributeRelationships(modelBuilder);
        CreateEntityMetadataRelationships(modelBuilder);

        // Necessary for traversing the full entity graph when reading data out from the Db
        CreateNavigationAutoIncludes(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}