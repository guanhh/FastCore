﻿using FastCore.Auditing;
using FastCore.EFCore.SqlServer;
using FastCore.HealthCheck;
using FastCore.Redis;
using FastCore.Security;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SRM.HttpApi.Extensions
{
    public static class ServiceExtensions
    {
        private const string CorsPolicy = "_FastCoreCorsPolicy";
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy, builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureSqlServer(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<FastCoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("FastCoreConnection"),
                options => options.EnableRetryOnFailure()));
        }

        public static void ConfigureFastCoreService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.ConfigureAuditLog(configuration);
            services.ConfigureRedis(configuration);
            services.ConfigureSqlServer(configuration);

            services.ConfigureCors();
            services.ConfigureJwt(configuration);

            services.ConfigureHealthChecks(configuration);

            services.AddHealthChecksUI()
                    .AddSqlServerStorage(configuration.GetConnectionString("HealthCheckConnection"));
        }

        public static void UseFastCore(this IApplicationBuilder app)
        {
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseCors(CorsPolicy);

            app.UseAuditLog();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecksUI();
            });
        }



    }
}
