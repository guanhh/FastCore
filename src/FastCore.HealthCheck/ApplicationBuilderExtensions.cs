using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCore.HealthCheck
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseHealthChecksCore(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/healthz", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app.UseEndpoints(endpoints =>
            {
                //healthcheck
                endpoints.MapHealthChecksUI();
            });
        }
    }
}
