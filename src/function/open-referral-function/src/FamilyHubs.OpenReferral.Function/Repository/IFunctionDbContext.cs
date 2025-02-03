using FamilyHubs.SharedKernel.OpenReferral.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.OpenReferral.Function.Repository;

public interface IFunctionDbContext
{
    DbSet<Service> ServicesDbSet { get; }
    public Task<int> SaveChangesAsync();
}