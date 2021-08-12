using FastCore.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastCore.MongoDB
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDBOption>(configuration.GetSection("MongoDBOptions"));

            return services.AddSingleton<IMongoDBService, MongoDBService>();
        }
    }
}
