using Microsoft.AspNetCore.Builder;

namespace FastCore.Auditing
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseAuditLog(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuditingMiddleware>();
        }
    }
}
