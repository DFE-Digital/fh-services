using AutoMapper;
using FamilyHubs.OpenReferral.Function.Repository;
using FamilyHubs.SharedKernel.Factories;
using FamilyHubs.SharedKernel.OpenReferral.Entities;
using FamilyHubs.SharedKernel.Services.Sanitizers;
using FamilyHubs.SharedKernel.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.OpenReferral.Function.Services;

// TODO: Clean Architecture: Move to application layer
public interface IDedsService
{
    /// <summary>
    /// Upsert a service to the database
    /// </summary>
    /// <param name="incomingServiceDto">Dto to map to Service</param>
    /// <param name="checksum">Calculated checksum to be added to the entity</param>
    /// <typeparam name="T">Use a Dto that is available in automapper</typeparam>
    /// <returns>Dto that can be mapped to the Service entity</returns>
    Task<Guid> UpsertService<T>(T incomingServiceDto, long checksum) where T : class, IExternalService;
}

// TODO: Clean Architecture: Move to application layer
public class DedsService(ILogger<DedsService> logger, FunctionDbContext context, IMapper mapper) : IDedsService
{
    private readonly IStringSanitizer _textSanitizer = SanitizerFactory.CreateDedsTextSanitizer();
    
    public Task<List<Service>> GetServices()
    {
        return context.ServicesDbSet.AsSplitQuery().AsNoTracking().ToListAsync();
    }

    public Task<Service?> GetServiceByOpenReferralId(Guid id)
    {
        // OrId although unique to each organisation, it is not unique in our database.
        // TODO: We should use mix of identifiers to get the service, the other should be potentially the Id of the LA provider
        // No implemented provider yet. This will be changed soon
        return context.ServicesDbSet.AsSplitQuery().FirstOrDefaultAsync(x => x.OrId == id);
    }

    public async Task<Guid> UpsertService<T>(T incomingServiceDto, long checksum) where T : class, IExternalService
    {
        var hasChecksum = await context.ServicesDbSet.IgnoreAutoIncludes().AnyAsync(x => x.Checksum == checksum);
        if (hasChecksum)
        {
            logger.LogInformation("Service with checksum {Checksum} already exists in the database, no changes to update", checksum);
            return Guid.Empty;
        }
        
        var profanityCheck = ProfanityChecker.HasProfanity(incomingServiceDto);
        if (profanityCheck)
        {
            logger.LogWarning("Service with OR ID {OrId} contains profanity and will not be upserted to the database", incomingServiceDto.ThirdPartyIdentifier);
            return Guid.Empty;
        }
        
        // OrId(ThirdPartyIdentifier) although unique to each organisation, it is not unique in our database.
        // TODO: We should use mix of identifiers to get the service, the other should be potentially the Id of the LA provider
        // No implemented provider yet. This will be changed soon
        var existingService = await context.ServicesDbSet
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.OrId == incomingServiceDto.ThirdPartyIdentifier);
        
        // Update
        if (existingService is not null)
        {
            mapper.Map(incomingServiceDto, existingService);
            _textSanitizer.Sanitize(existingService);
            existingService.Checksum = checksum;
            context.Update(existingService);
            await context.SaveChangesAsync();
            return existingService.Id;
        }
        
        // Create
        var newService = mapper.Map<Service>(incomingServiceDto);
        _textSanitizer.Sanitize(newService);
        newService.Checksum = checksum;
        
        var entity = await context.ServicesDbSet.AddAsync(newService);
        await context.SaveChangesAsync();
        return entity.Entity.Id;
    }
}