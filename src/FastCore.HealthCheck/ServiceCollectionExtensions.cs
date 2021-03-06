using FastCore.EFCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastCore.HealthCheck
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHealthCheckService(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddHealthChecks()
                    .AddDbContextCheck<FastCoreContext>()
                    .AddRedis(Configuration["Redis:ConnectionString"])
                    .AddPingHealthCheck(option =>
                    {
                        var ips = Configuration["PingIp"];
                        if (!string.IsNullOrWhiteSpace(ips))
                        {
                            var ipArr = ips.Split(',');
                            foreach (var ip in ipArr)
                            {
                                option.AddHost(ip, 3000);
                            }
                        }
                    })
                    .AddHangfire(option =>
                    {
                        option.MaximumJobsFailed = 3;
                        option.MinimumAvailableServers = 1;
                    });


            services.AddHealthChecksUI()
            //      .AddSqlServerStorage(configuration.GetConnectionString("HealthCheckConnection"));
                    .AddInMemoryStorage();
        }
    }
}
