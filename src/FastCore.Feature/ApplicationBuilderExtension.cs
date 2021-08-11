using FastCore.Auditing;
using FastCore.Hangfire;
using FastCore.HealthCheck;
using Microsoft.AspNetCore.Builder;

namespace FastCore.Feature
{
    public static class ApplicationBuilderExtension
    {

        public static void UseFastCoreFeature(this IApplicationBuilder app)
        {

            app.UseHangfire();

            app.UseHealthChecksCore();

        }

    }
}
