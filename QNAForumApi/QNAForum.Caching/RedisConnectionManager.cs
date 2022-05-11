using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;


namespace QNAForum.Caching
{
    /// <summary>
    /// This implementation of Redis is based on such a redis configuration which has a master and a slave.
    /// All requests are handled by master and slave is the exact replica of master in realtime
    /// If master fails for some reason slave is promoted to master.
    /// As soon as master is backup it is designated as slave
    /// </summary>
    public class RedisConnectionManager : ICacheConnectionManager
    {
        private readonly string RedisConnectionString = "RedisConnectionString";
        private ConnectionMultiplexer _redisConnection;
        private ConfigurationOptions _configurationOptions;

        public RedisConnectionStatus RedisConnectionStatus => throw new NotImplementedException();

        public void Connect()
        {
            RedisConnectionStatus redisConnectionStatus = new RedisConnectionStatus();
            _configurationOptions = ConfigurationOptions.Parse(GetConnectionString());
            _configurationOptions.AllowAdmin = true;
            _configurationOptions.AbortOnConnectFail = false;
            lock (_configurationOptions)
            {
                if (_redisConnection == null && _configurationOptions.EndPoints.Count > 0)
                {
                    _redisConnection = ConnectionMultiplexer.Connect(_configurationOptions);
                }
                //Code for master - slave configuration still left
            }
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[RedisConnectionString].ConnectionString;
        }


        public IDatabase GetDatabase()
        {
            return _redisConnection.GetDatabase();
        }

        public EndPoint[] GetEndPoints()
        {
            return _redisConnection.GetEndPoints();
        }

        public IServer GetServer(EndPoint endPoint)
        {
            return _redisConnection.GetServer(endPoint);
        }

        public void Dispose()
        {
            if (_redisConnection != null)
            {
                _redisConnection.Dispose();
            }
        }
    }

    public class RedisConnectionStatus
    {
        Connection.Status Status;
    }

    internal class Connection
    {
        public enum Status
        {
            Disconnected = 0,
            Connected = 1
        }
    }
}
