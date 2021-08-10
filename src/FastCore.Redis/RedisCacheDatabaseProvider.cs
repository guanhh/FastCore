using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastCore.Redis
{
    public class RedisCacheDatabaseProvider : IRedisCacheDatabaseProvider
    {

        private readonly RedisCacheOptions _options;
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCacheDatabaseProvider"/> class.
        /// </summary>
        public RedisCacheDatabaseProvider(IOptions<RedisCacheOptions> options)
        {
            _options = options.Value;
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(CreateConnectionMultiplexer);
        }
        /// <summary>
        /// Gets the database connection.
        /// </summary>
        public IDatabase GetDatabase()
        {
            return _connectionMultiplexer.Value.GetDatabase(_options.DatabaseId);
        }

        private ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            try
            {
                return ConnectionMultiplexer.Connect(_options.ConnectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
