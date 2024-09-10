using FamilyHubs.OpenReferral.Function.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.OpenReferral.Function.Repository;

public class FunctionDbContext(DbContextOptions<FunctionDbContext> options) : DbContext(options), IFunctionDbContext
{
    public DbSet<Service> Services { get; init; } = null!;

    public void AddService(Service service)
    {
        Services.Add(service);
    }

    // TODO: Update for new schema -> TruncateDEDSSchemaAsync
    // Just a dummy statement for now since the [staging].[services_temp] no longer exists :)
    public Task TruncateServicesTempAsync() => Database.ExecuteSqlRawAsync("SELECT TOP (1) [Id] FROM [deds].[Service]");

    public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OpenReferralDbContextExtension openReferralDbContextExtension = new();
        openReferralDbContextExtension.OnModelCreating(modelBuilder);

        // Service


        // Language
        // modelBuilder.Entity<Service>()
        //     .HasMany<Language>(e => e.Languages)
        //     .WithOne(e => e.Service)
        //     .HasForeignKey(e => e.ServiceOrId)
        //     .HasPrincipalKey(e => e.OrId);
        //
        // // Contact
        // modelBuilder.Entity<Service>()
        //     .HasMany<Contact>(e => e.Contacts)
        //     .WithOne(e => e.Service)
        //     .HasForeignKey(e => e.ServiceOrId)
        //     .HasPrincipalKey(e => e.OrId);
        //
        // modelBuilder.Entity<Organization>()
        //     .HasMany<Contact>(e => e.Contacts)
        //     .WithOne(e => e.Organization)
        //     .HasForeignKey(e => e.OrganizationOrId)
        //     .HasPrincipalKey(e => e.OrId);
        //
        // modelBuilder.Entity<Location>()
        //     .HasMany<Contact>(e => e.Contacts)
        //     .WithOne(e => e.Location)
        //     .HasForeignKey(e => e.LocationOrId)
        //     .HasPrincipalKey(e => e.OrId);
        //
        // modelBuilder.Entity<ServiceAtLocation>()
        //     .HasMany<Contact>(e => e.Contacts)
        //     .WithOne(e => e.ServiceAtLocation)
        //     .HasForeignKey(e => e.ServiceAtLocationOrId)
        //     .HasPrincipalKey(e => e.OrId);

        base.OnModelCreating(modelBuilder);
    }
}