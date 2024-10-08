using FamilyHubs.Referral.Core.DistributedCache;
using FamilyHubs.Referral.Core.Models;
using FamilyHubs.SharedKernel.Razor.DistributedCache;
using Microsoft.Extensions.Caching.Distributed;

namespace FamilyHubs.Referral.Infrastructure.DistributedCache;

public class ConnectionRequestDistributedCache(
    IDistributedCache distributedCache,
    DistributedCacheEntryOptions distributedCacheEntryOptions)
    : IConnectionRequestDistributedCache
{
    public async Task<ConnectionRequestModel?> GetAsync(string professionalsEmail)
    {
        return await distributedCache.GetAsync<ConnectionRequestModel>(professionalsEmail);
    }

    public async Task SetAsync(string professionalsEmail, ConnectionRequestModel model)
    {
        await distributedCache.SetAsync(professionalsEmail, model, distributedCacheEntryOptions);
    }

    public async Task RemoveAsync(string professionalsEmail)
    {
        await distributedCache.RemoveAsync(professionalsEmail);
    }
}