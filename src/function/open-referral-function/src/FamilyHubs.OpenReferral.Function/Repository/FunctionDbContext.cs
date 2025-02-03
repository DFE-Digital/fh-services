using FamilyHubs.SharedKernel.OpenReferral.Entities;
using FamilyHubs.SharedKernel.OpenReferral.Repository;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.OpenReferral.Function.Repository;

public class FunctionDbContext(DbContextOptions<FunctionDbContext> options) : DbContext(options), IFunctionDbContext
{
    public DbSet<Service> ServicesDbSet { get; init; } = null!;

    public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OpenReferralDbContextExtension.OnModelCreating(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}