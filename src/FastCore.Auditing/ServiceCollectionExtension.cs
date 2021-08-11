using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastCore.Auditing
{
    public static class ServiceCollectionExtension
    {
        public static void AddAuditLog(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuditingOptions>(configuration.GetSection("AuditingOptions"));

            services.AddTransient<AuditingMiddleware>();
        }
    }
}
