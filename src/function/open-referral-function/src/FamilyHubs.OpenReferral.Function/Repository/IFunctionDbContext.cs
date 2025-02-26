using FamilyHubs.SharedKernel.OpenReferral.PrototypeEntities;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.OpenReferral.Function.Repository;

public interface IFunctionDbContext
{
    DbSet<ThirdParty> ThirdParties {  get; }
    DbSet<ThirdPartyService> ThirdPartyServices {  get; }
    DbSet<StandardVersion> StandardVersions {  get; }

    public Task<int> SaveChangesAsync();
}