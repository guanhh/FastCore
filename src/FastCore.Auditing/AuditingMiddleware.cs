using FastCore.Abstract;
using FastCore.EFCore.SqlServer;
using FastCore.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FastCore.Auditing
{
    public class AuditingMiddleware : IMiddleware
    {
        private readonly AuditingOptions _options;
        private readonly ILogger<AuditingMiddleware> _logger;
        private readonly FastCoreContext _dbContext;
        private readonly ICurrentUser _currentUser;

        public AuditingMiddleware(ILogger<AuditingMiddleware> logger, IOptions<AuditingOptions> options, FastCoreContext context, ICurrentUser currentUser)
        {
            _logger = logger;
            _options = options.Value;
            _dbContext = context;
            _currentUser = currentUser;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();

            var auditLog = CreateAuditLog(context);

            var body = await GetRequestBody(context);
            auditLog.RequestContent = body;

            bool hasError = false;

            var originalResponseStream = context.Response.Body;
            try
            {
                using (var ms = new MemoryStream())
                {
                    context.Response.Body = ms;

                    await next(context);

                    ms.Position = 0;
                    var responseReader = new StreamReader(ms);

                    auditLog.ResponseContent = await responseReader.ReadToEndAsync();

                    ms.Position = 0;

                    await ms.CopyToAsync(originalResponseStream);
                    context.Response.Body = originalResponseStream;
                }
            }
            catch (Exception ex)
            {
                auditLog.Exceptions = ex.Message;
                hasError = true;
            }
            finally
            {
                auditLog.HttpStatusCode = context.Response.StatusCode;

                if (_currentUser.IsAuthenticated)
                {
                    auditLog.UserId = _currentUser.UserId;
                    auditLog.UserName = _currentUser.UserName;

                    _logger.LogInformation(string.Format("[{0}][{1}]", auditLog.Url, auditLog.UserName));
                }

                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (exceptionHandlerFeature != null)
                {
                    auditLog.Exceptions = string.Format("{0}---{1}", exceptionHandlerFeature.Error.Message, exceptionHandlerFeature.Error.StackTrace);
                    hasError = true;
                }


                stopwatch.Stop();
                auditLog.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);

                if (ShouldWriteAuditLog(context, hasError))
                {
                    await _dbContext.AddAsync(auditLog);
                    await _dbContext.SaveChangesAsync();
                }

            }

        }

        private async Task<string> GetRequestBody(HttpContext context)
        {
            context.Request.EnableBuffering();

            // Leave the body open so the next middleware can read it.
            using (var reader = new StreamReader(
                context.Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: 1,
                leaveOpen: true))
            {
                var body = await reader.ReadToEndAsync();
                // Do some processing with body…
                // Reset the request body stream position so the next middleware can read it
                context.Request.Body.Position = 0;

                return body;
            }
        }

        private AuditLog CreateAuditLog(HttpContext context)
        {
            return new AuditLog
            {
                ApplicationName = _options.ApplicationName,
                ExecutionTime = DateTime.Now,
                HttpMethod = context.Request.Method,
                Url = BuildUrl(context),
                ClientIpAddress = context.Connection.RemoteIpAddress.ToString(),
                ClientId = context.Connection.Id,
                BrowserInfo = context.Request.Headers["User-Agent"]
            };
        }

        private bool ShouldWriteAuditLog(HttpContext httpContext, bool hasError = false)
        {
            var endpoint = httpContext.Features.Get<IEndpointFeature>()?.Endpoint;
            if (endpoint == null)
                return false;

            var disAuditingAttribute = endpoint.Metadata.GetMetadata<DisableAuditingAttribute>();
            if (disAuditingAttribute != null)
                return false;

            var auditedAttribute = endpoint.Metadata.GetMetadata<AuditedAttribute>();
            if (auditedAttribute != null)
                return true;

            if (!_options.IsEnabled)
            {
                return false;
            }

            if (_options.AlwaysLogOnException && hasError)
            {
                return true;
            }

            if (!_options.IsEnabledForAnonymousUsers && !httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            if (!_options.IsEnabledForGetRequests &&
                string.Equals(httpContext.Request.Method, HttpMethods.Get, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        private string BuildUrl(HttpContext httpContext)
        {
            //TODO: Add options to include/exclude query, schema and host

            var uriBuilder = new UriBuilder();

            uriBuilder.Scheme = httpContext.Request.Scheme;
            uriBuilder.Host = httpContext.Request.Host.Host;
            uriBuilder.Path = httpContext.Request.Path.ToString();
            uriBuilder.Query = httpContext.Request.QueryString.ToString();

            return uriBuilder.Uri.PathAndQuery;
        }


    }
}
