using FastCore.Auditing;
using FastCore.Hangfire;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace FastCore.Feature
{
    public static class ApplicationBuilderExtension
    {

        public static void UseFastCoreFeature(this IApplicationBuilder app)
        {
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseHealthChecks("/healthz", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseCors(StringConstants.CorsPolicy);

            app.UseAuditLog();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfire();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //healthcheck
                endpoints.MapHealthChecksUI();
            });
        }


    }
}
