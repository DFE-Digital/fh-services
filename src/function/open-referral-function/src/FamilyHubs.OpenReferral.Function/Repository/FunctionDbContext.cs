using FamilyHubs.SharedKernel.OpenReferral.Entities;
using FamilyHubs.SharedKernel.OpenReferral.Repository;
using Microsoft.EntityFrameworkCore;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OpenReferralDbContextExtension.OnModelCreating(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}