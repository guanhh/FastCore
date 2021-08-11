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
            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseHealthChecks("/healthz", new HealthCheckOptions
            //{
            //    Predicate = _ => true,
            //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            //});

            app.UseCors(StringConstants.CorsPolicy);

            app.UseAuditLog();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfire();

            app.UseHealthChecksCore();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //healthcheck
                //endpoints.MapHealthChecksUI();
            });
        }


    }
}
