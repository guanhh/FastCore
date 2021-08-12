using FastCore.Auditing;
using FastCore.EFCore.SqlServer;
using FastCore.Hangfire;
using FastCore.HealthCheck;
using FastCore.MongoDB;
using FastCore.Redis;
using FastCore.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastCore.Feature
{
    public static class ServiceCollectionExtensions
    {

        public static void AddFastCoreFeatureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();

            services.AddAuditLog(configuration);

            services.AddRedisService(configuration);

            services.AddMongoDB(configuration);

            services.AddSqlServer(configuration);

            //cors
            services.AddCors(options =>
            {
                options.AddPolicy(StringConstants.CorsPolicy, builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddJwt(configuration);

            //hangfire
            services.ConfigureHangfire(configuration);

        }


    }
}
