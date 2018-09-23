using System;

namespace QNAForum.Caching
{
    public interface ICacheManager : IDisposable
    {
        T GetOrSet<T>(string cacheKey, Func<T> fetchData) where T : class;
        T GetOrSet<T>(string cacheKey, Func<T> fetchData, double cacheExpiration) where T : class;

        bool IsSet(string key);

        bool RemoveKey(string key);

        bool RemoveKeyByPattern(string pattern);

        bool RemoveAllKeys();
    }
}