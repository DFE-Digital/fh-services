using FamilyHubs.ServiceDirectory.Data.Entities.Staging;

namespace FamilyHubs.OpenReferral.Function.Repository;

public interface IFunctionDbContext
{
    public void AddServiceTemp(ServicesTemp serviceTemp);

    public Task TruncateServicesTempAsync();

    public Task<int> SaveChangesAsync();
}