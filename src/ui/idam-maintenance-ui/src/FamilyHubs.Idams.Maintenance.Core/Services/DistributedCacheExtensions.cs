﻿using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;


namespace FamilyHubs.Idams.Maintenance.Core.Services;

public static class DistributedCacheExtensions
{
    public static async Task<T?> GetAsync<T>(
        this IDistributedCache cache,
        string key,
        CancellationToken token = default)
    {
        var json = await cache.GetStringAsync(key, token);
        return json == null ? default : JsonSerializer.Deserialize<T>(json);
    }

    public static async Task SetAsync<T>(
        this IDistributedCache cache,
        string key,
        T value,
        DistributedCacheEntryOptions options,
        CancellationToken token = default)
    {
        var json = JsonSerializer.Serialize(value);
        await cache.SetStringAsync(key, json, options, token);
    }
}