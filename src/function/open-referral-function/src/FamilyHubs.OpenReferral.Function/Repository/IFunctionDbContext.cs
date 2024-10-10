using FamilyHubs.SharedKernel.OpenReferral.Entities;

namespace FamilyHubs.OpenReferral.Function.Repository;

public interface IFunctionDbContext
{
    public void AddService(Service service);
    public void DeleteService(Service service);
    public IQueryable<Service> Services();

    public Task<List<T>> ToListAsync<T>(IQueryable<T> queryable);

    public Task<int> SaveChangesAsync();
}