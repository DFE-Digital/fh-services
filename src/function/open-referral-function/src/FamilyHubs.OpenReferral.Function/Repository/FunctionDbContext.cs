using FamilyHubs.OpenReferral.Function.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.OpenReferral.Function.Repository;

public class FunctionDbContext(DbContextOptions<FunctionDbContext> options) : DbContext(options), IFunctionDbContext
{
    public DbSet<Service> Services { get; init; } = null!;

    public void AddService(Service service)
    {
        Services.Add(service);

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
    public Task TruncateServicesTempAsync() => Database.ExecuteSqlRawAsync("SELECT TOP (1) [Id] FROM [deds].[Service]");

    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OpenReferralDbContextExtension openReferralDbContextExtension = new();
        openReferralDbContextExtension.OnModelCreating(modelBuilder);

        // Organization

        modelBuilder.Entity<Organization>()
            .HasMany<Service>(e => e.Services)
            .WithOne(e => e.Organization)
            .HasForeignKey(e => e.OrganizationId);

        modelBuilder.Entity<Organization>()
            .HasMany<Location>(e => e.Locations)
            .WithOne(e => e.Organization)
            .HasForeignKey(e => e.OrganizationId);

        modelBuilder.Entity<Organization>()
            .HasMany<Entities.Program>(e => e.Programs)
            .WithOne(e => e.Organization)
            .HasForeignKey(e => e.OrganizationId);


        modelBuilder.Entity<Organization>()
            .HasMany<Funding>(e => e.Funding)
            .WithOne(e => e.Organization)
            .HasForeignKey(e => e.OrganizationId);


        modelBuilder.Entity<Organization>()
            .HasMany<OrganizationIdentifier>(e => e.OrganizationIdentifiers)
            .WithOne(e => e.Organization)
            .HasForeignKey(e => e.OrganizationId);


        modelBuilder.Entity<Organization>()
            .HasMany<Phone>(e => e.Phones)
            .WithOne(e => e.Organization)
            .HasForeignKey(e => e.OrganizationId);


        modelBuilder.Entity<Organization>()
            .HasMany<Contact>(e => e.Contacts)
            .WithOne(e => e.Organization)
            .HasForeignKey(e => e.OrganizationId);


        // modelBuilder.Entity<Organization>()
        //     .HasOne<Organization>(e => e.ParentOrganization)
        //     .WithMany(e => e.ChildOrganizations)
        //     .HasForeignKey(e => e.ParentOrganizationId);
            

        // Service

        modelBuilder.Entity<Service>()
            .HasMany<CostOption>(e => e.CostOptions)
            .WithOne(e => e.Service)
            .HasForeignKey(e => e.ServiceId);


        modelBuilder.Entity<Service>()
            .HasMany<ServiceArea>(e => e.ServiceAreas)
            .WithOne(e => e.Service)
            .HasForeignKey(e => e.ServiceId);


        modelBuilder.Entity<Service>()
            .HasMany<RequiredDocument>(e => e.RequiredDocuments)
            .WithOne(e => e.Service)
            .HasForeignKey(e => e.ServiceId);


        modelBuilder.Entity<Service>()
            .HasMany<Funding>(e => e.Funding)
            .WithOne(e => e.Service)
            .HasForeignKey(e => e.ServiceId);


        modelBuilder.Entity<Service>()
            .HasMany<Schedule>(e => e.Schedules)
            .WithOne(e => e.Service)
            .HasForeignKey(e => e.ServiceId);


        modelBuilder.Entity<Service>()
            .HasMany<ServiceAtLocation>(e => e.ServiceAtLocations)
            .WithOne(e => e.Service)
            .HasForeignKey(e => e.ServiceId);


        modelBuilder.Entity<Service>()
            .HasMany<Contact>(e => e.Contacts)
            .WithOne(e => e.Service)
            .HasForeignKey(e => e.ServiceId);


        modelBuilder.Entity<Service>()
            .HasMany<Phone>(e => e.Phones)
            .WithOne(e => e.Service)
            .HasForeignKey(e => e.ServiceId);


        modelBuilder.Entity<Service>()
            .HasMany<Language>(e => e.Languages)
            .WithOne(e => e.Service)
            .HasForeignKey(e => e.ServiceId);
            

        // Location

        modelBuilder.Entity<Location>()
            .HasMany<Accessibility>(e => e.Accessibilities)
            .WithOne(e => e.Location)
            .HasForeignKey(e => e.LocationId);


        modelBuilder.Entity<Location>()
            .HasMany<Address>(e => e.Addresses)
            .WithOne(e => e.Location)
            .HasForeignKey(e => e.LocationId);


        modelBuilder.Entity<Location>()
            .HasMany<Phone>(e => e.Phones)
            .WithOne(e => e.Location)
            .HasForeignKey(e => e.LocationId);


        modelBuilder.Entity<Location>()
            .HasMany<Language>(e => e.Languages)
            .WithOne(e => e.Location)
            .HasForeignKey(e => e.LocationId);


        modelBuilder.Entity<Location>()
            .HasMany<Schedule>(e => e.Schedules)
            .WithOne(e => e.Location)
            .HasForeignKey(e => e.LocationId);


        modelBuilder.Entity<Location>()
            .HasMany<ServiceAtLocation>(e => e.ServiceAtLocations)
            .WithOne(e => e.Location)
            .HasForeignKey(e => e.LocationId);
            

        // Program

        modelBuilder.Entity<Entities.Program>()
            .HasMany<Service>(e => e.Services)
            .WithOne(e => e.Program)
            .HasForeignKey(e => e.ProgramId);
            

        // Service At Location

        modelBuilder.Entity<ServiceAtLocation>()
            .HasMany<Schedule>(e => e.Schedules)
            .WithOne(e => e.ServiceAtLocation)
            .HasForeignKey(e => e.ServiceAtLocationId);


        modelBuilder.Entity<ServiceAtLocation>()
            .HasMany<Contact>(e => e.Contacts)
            .WithOne(e => e.ServiceAtLocation)
            .HasForeignKey(e => e.ServiceAtLocationId);


        modelBuilder.Entity<ServiceAtLocation>()
            .HasMany<Phone>(e => e.Phones)
            .WithOne(e => e.ServiceAtLocation)
            .HasForeignKey(e => e.ServiceAtLocationId);
            

        // Contact

        modelBuilder.Entity<Contact>()
            .HasMany<Phone>(e => e.Phones)
            .WithOne(e => e.Contact)
            .HasForeignKey(e => e.ContactId);
            

        // Phone

        modelBuilder.Entity<Phone>()
            .HasMany<Language>(e => e.Languages)
            .WithOne(e => e.Phone)
            .HasForeignKey(e => e.PhoneId);
            

        base.OnModelCreating(modelBuilder);
    }
}