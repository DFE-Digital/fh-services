using FamilyHubs.SharedKernel.OpenReferral.Entities;
using FamilyHubs.SharedKernel.OpenReferral.Repository;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.OpenReferral.Function.Repository;

public class FunctionDbContext(DbContextOptions<FunctionDbContext> options) : DbContext(options), IFunctionDbContext
{
    private DbSet<Service> ServicesDbSet { get; init; } = null!;
    public IQueryable<Service> Services() => ServicesDbSet.AsSplitQuery();

    public void AddService(Service service) => ServicesDbSet.Add(service);

    public void DeleteService(Service service) =>
        ChangeTracker.TrackGraph(service, node => node.Entry.State = EntityState.Deleted);

    public Task<List<T>> ToListAsync<T>(IQueryable<T> queryable) => queryable.ToListAsync();

    public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OpenReferralDbContextExtension.OnModelCreating(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}