using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastCore.EFCore.SqlServer
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSqlServer(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<FastCoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("FastCoreConnection"),
                options => options.EnableRetryOnFailure()));
        }
    }
}
