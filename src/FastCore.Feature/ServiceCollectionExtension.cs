using FastCore.Auditing;
using FastCore.EFCore.SqlServer;
using FastCore.Hangfire;
using FastCore.HealthCheck;
using FastCore.Redis;
using FastCore.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastCore.Feature
{
    public static class ServiceCollectionExtension
    {
        public static void AddFastCoreCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(StringConstants.CorsPolicy, builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void AddSqlServer(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<FastCoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("FastCoreConnection"),
                options => options.EnableRetryOnFailure()));
        }

        public static void AddFastCoreFeatureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddAuditLog(configuration);
            services.AddRedisService(configuration);
            services.AddSqlServer(configuration);

            services.AddFastCoreCors();
            services.AddJwt(configuration);

            //healthcheck
            services.AddHealthCheckService(configuration);

            //services.AddHealthChecksUI()
            //        .AddSqlServerStorage(configuration.GetConnectionString("HealthCheckConnection"));

            services.AddHealthChecksUI()
                  .AddInMemoryStorage();

            //hangfire
            services.ConfigureHangfire(configuration);

        }


    }
}
