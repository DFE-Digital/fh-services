using Microsoft.Extensions.Caching.Memory;
using NSubstitute;

namespace FamilyHubs.ServiceDirectory.UnitTests.Services.ServiceDirectory;

public class NotACache(Func<object, (bool, object?)> lookup) : IMemoryCache
{
    public ICacheEntry CreateEntry(object key) => Substitute.For<ICacheEntry>();

    public void Remove(object key) { }

    public bool TryGetValue(object key, out object? retVal)
    {
        (var success, retVal) = lookup(key);
        return success;
    }

    protected virtual void Dispose(bool disposing)
    {
        // Dispose
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
