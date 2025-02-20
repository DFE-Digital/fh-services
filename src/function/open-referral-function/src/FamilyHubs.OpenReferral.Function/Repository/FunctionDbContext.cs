using FamilyHubs.SharedKernel.OpenReferral.Entities;
using FamilyHubs.SharedKernel.OpenReferral.PrototypeEntities;
using FamilyHubs.SharedKernel.OpenReferral.Repository;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.OpenReferral.Function.Repository;

public class FunctionDbContext(DbContextOptions<FunctionDbContext> options) : DbContext(options), IFunctionDbContext
{
    public DbSet<Service> ServicesDbSet { get; init; } = null!;
    
    public DbSet<ThirdParty> ThirdParties { get; init; } = null!;
    public DbSet<ThirdPartyService> ThirdPartyServices { get; init; } = null!;
    public DbSet<StandardVersion> StandardVersions { get; init; } = null!;
    

    public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OpenReferralDbContextExtension.OnModelCreating(modelBuilder);
        OpenReferralDbContextExtensionPrototype.OnModelCreating(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}