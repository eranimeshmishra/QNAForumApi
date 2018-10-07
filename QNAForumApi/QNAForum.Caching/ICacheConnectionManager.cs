using System;
using System.Net;
using StackExchange.Redis;

namespace QNAForum.Caching
{
    public interface ICacheConnectionManager:IDisposable
    {
        void Connect();

        IServer GetServer(EndPoint endPoint);

        IDatabase GetDatabase();

        EndPoint[] GetEndPoints();

        RedisConnectionStatus RedisConnectionStatus { get; }
    }
}