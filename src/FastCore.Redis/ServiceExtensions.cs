using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FastCore.Redis
{
    public static class ServiceExtensions
    {
        public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<RedisCacheOptions>(option =>
            {
                option.DatabaseId = int.Parse(configuration.GetSection("Redis:DatabaseId").Value);
                option.ConnectionString = configuration.GetSection("Redis:ConnectionString").Value;
            });

            services.AddSingleton<IRedisCacheDatabaseProvider, RedisCacheDatabaseProvider>();
            services.AddSingleton<IRedisCacheSerializer, DefaultRedisCacheSerializer>();

            services.AddSingleton<ICache, RedisCache>();
        }

    }
}
