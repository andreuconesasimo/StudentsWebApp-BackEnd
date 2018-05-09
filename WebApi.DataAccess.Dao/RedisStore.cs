using StackExchange.Redis;
using System;
using System.Configuration;
using WebApi.Common.Logic.Properties;

namespace WebApi.DataAccess.Dao
{
    public class RedisStore
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection;

        static RedisStore()
        {            
            LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings[ConfigStrings.RedisConn].ToString()));
        }

        public static ConnectionMultiplexer Connection => LazyConnection.Value;

        public static IDatabase RedisCache => Connection.GetDatabase();
    }
}
