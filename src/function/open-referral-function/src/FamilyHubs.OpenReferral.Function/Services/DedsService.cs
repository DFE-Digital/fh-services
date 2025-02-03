using System.ComponentModel;
using FamilyHubs.OpenReferral.Function.Repository;
using FamilyHubs.SharedKernel.Factories;
using FamilyHubs.SharedKernel.OpenReferral.Entities;
using FamilyHubs.SharedKernel.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.OpenReferral.Function.Services;

// TODO: Clean Architecture: Move to application layer
public interface IDedsService
{
    Task<List<Service>> GetServices();
    Task<Service?> GetServicesById(string id);
    Task<Guid> AddService(Service service);
    Task DeleteService(Service service);

    [Description("** WARNING ** This will completely clear the deds tables and is only to be used for local testing. Delete when done!")]
    Task ClearDatabase();
}

// TODO: Clean Architecture: Move to infrastructure layer
public class DedsService(ILogger<DedsService> logger, IFunctionDbContext context) : IDedsService
{
    public Task<List<Service>> GetServices()
    {
        return context.ServicesDbSet.AsNoTracking().AsSplitQuery().ToListAsync();
    }

    public Task<Service?> GetServicesById(string id)
    {
        return context.ServicesDbSet.AsNoTracking().AsSplitQuery().FirstOrDefaultAsync(x => x.Id == new Guid(id));
    }

    public async Task<Guid> AddService(Service service)
    {
        var hasProfanity = ProfanityChecker.HasProfanity(service);
        if (hasProfanity)
        {
            logger.LogWarning("Service with OR ID {OrId} contains profanity and will not be added to the database",
                service.OrId);
            return Guid.Empty;
        }

        var textSanitizer = SanitizerFactory.CreateDedsTextSanitizer();
        logger.LogInformation("Sanitizing service with ID {serviceId}", service.OrId);
        var sanitizedService = textSanitizer.Sanitize(service);

        try
        {
            logger.LogInformation("Adding service with ID {serviceId} to the database", service.OrId);
            var entity = await context.ServicesDbSet.AddAsync(sanitizedService);
            await context.SaveChangesAsync();
            return entity.Entity.Id;
        }
        catch (Exception e)
        {
            logger.LogError("Failed to add service with ID {serviceId} to the database | Exception = {exception}",
                service.OrId, e.Message);
            return Guid.Empty;
        }
    }

    public async Task DeleteService(Service service)
    {
        try
        {
            logger.LogInformation("Removing service with ID {serviceId} from the database", service.OrId);
            context.ServicesDbSet.Remove(service);
            await context.SaveChangesAsync();
        } catch (Exception e)
        {
            logger.LogError("Failed to remove service with ID {serviceId} from the database | Exception = {exception}",
                service.OrId, e.Message);
        }
    }

    public async Task ClearDatabase()
    {
        logger.LogInformation("Removing all services from the database");
        var serviceListFromDb = await GetServices();

        foreach (var service in serviceListFromDb)
        {
            logger.LogInformation("Removing service from the database, Internal ID = {iId} | Open Referral ID = {oId}",
                service.Id, service.OrId);
            await DeleteService(service);
        }
    }
}