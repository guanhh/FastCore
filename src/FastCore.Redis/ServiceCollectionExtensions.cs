using FastCore.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FastCore.Redis
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RedisCacheOptions>(option =>
            {
                option.DatabaseId = int.Parse(configuration.GetSection("Redis:DatabaseId").Value);
                option.ConnectionString = configuration.GetSection("Redis:ConnectionString").Value;
            });

            services.AddSingleton<IRedisCacheDatabaseProvider, RedisCacheDatabaseProvider>();
            services.AddSingleton<IRedisCacheSerializer, DefaultRedisCacheSerializer>();

            return services.AddSingleton<ICache, RedisCache>();
        }

    }
}
