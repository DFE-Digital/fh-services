using Service = FamilyHubs.OpenReferral.Function.Repository.Entities.Service;

namespace FamilyHubs.OpenReferral.Function.Repository;

public interface IFunctionDbContext
{
    public void AddService(Service service);

    // TODO: Update for new schema -> TruncateDEDSSchemaAsync
    public Task TruncateServicesTempAsync();

    public Task<int> SaveChangesAsync();
}