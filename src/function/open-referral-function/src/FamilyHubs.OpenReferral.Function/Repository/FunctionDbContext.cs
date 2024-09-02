using FamilyHubs.SharedKernel.OpenReferral;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.OpenReferral.Function.Repository;

public class FunctionDbContext(DbContextOptions<FunctionDbContext> options) : DbContext(options), IFunctionDbContext
{
    public DbSet<ServicesTemp> ServicesTemp { get; init; } = null!;

    public void AddServiceTemp(ServicesTemp serviceTemp) => ServicesTemp.Add(serviceTemp);

    public Task TruncateServicesTempAsync() => Database.ExecuteSqlRawAsync("TRUNCATE TABLE [staging].[services_temp]");

    public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ServicesTemp>()
            .ToTable("services_temp", "staging")
            .HasKey(e => e.Id);

        base.OnModelCreating(modelBuilder);
    }
}