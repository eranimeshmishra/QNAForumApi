using StackExchange.Redis;

namespace QNAForum.Caching
{
    public class RedisCacheManager:CacheManager
    {
        private ICacheConnectionManager _cacheConnectionManager;
        private IDatabase _redisDatabase;
        public RedisCacheManager(ICacheConnectionManager cacheConnectionManager, CacheConfiguration cacheConfiguration)
        {
            _cacheConnectionManager = cacheConnectionManager;
            //_redisDatabase = _cacheConnectionManager.Connect();
        }

        public override T Get<T>(string cacheKey)
        {
            throw new System.NotImplementedException();
        }

        public override void Set<T>(string cacheKey, T result)
        {
            throw new System.NotImplementedException();
        }

        public override bool IsSet(string key)
        {
            throw new System.NotImplementedException();
        }

        public override bool RemoveKey(string key)
        {
            throw new System.NotImplementedException();
        }

        public override bool RemoveKeyByPattern(string pattern)
        {
            throw new System.NotImplementedException();
        }

        public override bool RemoveAllKeys()
        {
            throw new System.NotImplementedException();
        }
    }
}