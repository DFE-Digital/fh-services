using FamilyHubs.SharedKernel.OpenReferral.Entities;

namespace FamilyHubs.OpenReferral.Function.Repository;

public interface IFunctionDbContext
{
    public void AddService(Service service);
    public IQueryable<Service> Services { get; }

    // TODO: Update for new schema -> TruncateDEDSSchemaAsync
    public Task TruncateServicesTempAsync();

    public Task<int> SaveChangesAsync();
}