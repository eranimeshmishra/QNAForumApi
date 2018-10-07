using System;
using System.Linq;

namespace QNAForum.Caching
{
    public abstract class CacheManager : ICacheManager
    {
        public abstract T Get<T>(string cacheKey) where T : class;

        public abstract void Set<T>(string cacheKey, T result) where T : class;

        public abstract bool IsSet(string key);

        public abstract bool RemoveAllKeys();

        public abstract bool RemoveKey(string key);

        public abstract bool RemoveKeyByPattern(string pattern);


        public CacheConfiguration _configuration;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public virtual T GetOrSet<T>(string cacheKey, Func<T> fetchData) where T : class
        {
            if (IsKeyDisabled(cacheKey))
            {
                return fetchData();
            }

            return GetOrSet(cacheKey, fetchData, _configuration.CacheExpiration);
        }

        /// <summary>
        /// Check if a certain key is disabled for caching
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        private bool IsKeyDisabled(string cacheKey)
        {
            return _configuration.DisabledGroups.Any(k => cacheKey.StartsWith(k));
        }

        public T GetOrSet<T>(string cacheKey, Func<T> fetchData, double cacheExpiration) where T : class
        {
            if (IsSet(cacheKey))
            {
                return Get<T>(cacheKey);
            }

            var result = fetchData();
            if (cacheExpiration > 0)
            {
                Set<T>(cacheKey, result);
            }

            return result;
        }
    }
}