using System.IO.Hashing;
using System.Text;
using System.Text.Json;
using FamilyHubs.OpenReferral.Function.Repository;
using FamilyHubs.SharedKernel.Factories;
using FamilyHubs.SharedKernel.OpenReferral.PrototypeEntities;
using FamilyHubs.SharedKernel.Services.Sanitizers;
using FamilyHubs.SharedKernel.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyHubs.OpenReferral.Function.Services;

public interface IDedsPrototypeService
{
    Task<int> UpsertService(
        int thirdPartyId, 
        string serviceId, 
        string document, 
        StandardDocumentVersions standardVersion); 
}

public class DedsPrototypeService(ILogger<DedsPrototypeService> logger, IFunctionDbContext context) : IDedsPrototypeService
{
    private static readonly JsonSerializerOptions JsonDocumentOptions = new()
    {
        WriteIndented = false,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // TODO: remove after tag stripping is implemented
    };
    private readonly IStringSanitizer _textSanitizer = SanitizerFactory.CreateDedsTextSanitizer();
    
    public async Task<int> UpsertService(
        int thirdPartyId, 
        string serviceId, 
        string document, 
        StandardDocumentVersions standardVersion)
    {

        if (ProfanityChecker.HasProfanity(document))
        {
            logger.LogWarning("Document from service {ServiceId} contains profanity", serviceId);
            return 0;
        }
        
        var documentChecksum = HashingAlgorithms.ComputeXxHashToLong64(document);
        
        var existingService = await context.ThirdPartyServices
            .FirstOrDefaultAsync(x => x.ThirdPartyId == thirdPartyId && x.ServiceId == serviceId);

        if (existingService is not null)
        {
            if (existingService.Checksum == documentChecksum)
            {
                logger.LogInformation("Service with ID {serviceId} for third party with ID {thirdPartyId} is already up to date", serviceId, thirdPartyId);
                return existingService.Id;
            }

            var sanitizedDocument = _textSanitizer.Sanitize(document);
            
            existingService.Document = MinifyJson(sanitizedDocument);
            existingService.Checksum = documentChecksum;
            existingService.StandardVersionId = (byte)standardVersion;
            existingService.UpdatedAt = DateTimeOffset.Now;
            context.ThirdPartyServices.Update(existingService);
            await context.SaveChangesAsync();
            
            logger.LogInformation("Updated service with ID {serviceId} for third party with ID {thirdPartyId}", serviceId, thirdPartyId);
            return existingService.Id;
        }
        
        var sanitizedDocumentNew = _textSanitizer.Sanitize(document);
        
        var newService = new ThirdPartyService
        {
            ThirdPartyId = thirdPartyId,
            ServiceId = serviceId,
            Document = MinifyJson(sanitizedDocumentNew),
            StandardVersionId = (byte)standardVersion,
            Checksum = documentChecksum,
            CreatedAt = DateTimeOffset.Now
        };
        
        var entity = await context.ThirdPartyServices.AddAsync(newService);
        await context.SaveChangesAsync();
        logger.LogInformation("Added service with ID {serviceId} for third party with ID {thirdPartyId}", serviceId, thirdPartyId);
        return entity.Entity.Id;
    }

    private static string MinifyJson(string jsonString)
    {
        var jsonObject = JsonDocument.Parse(jsonString);

        return JsonSerializer.Serialize(jsonObject, JsonDocumentOptions);
    }
}

public static class HashingAlgorithms
{
    public static long ComputeXxHashToLong64(string value)
    {
        var data = Encoding.UTF8.GetBytes(value);
        return BitConverter.ToInt64(XxHash64.Hash(data));
    }
}