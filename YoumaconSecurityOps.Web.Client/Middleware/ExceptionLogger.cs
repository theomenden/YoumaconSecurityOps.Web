using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace YoumaconSecurityOps.Web.Client.Middleware
{
    public class ExceptionLogger
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<Program> _logger;

        public ExceptionLogger(RequestDelegate next, ILogger<Program> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
            finally
            {
                _logger.LogDebug("Request Logged {@context}", context);
            }
        }

        private Task HandleExceptionAsync(Exception ex)
        {
            _logger.LogError("The Exception: {@ex}",ex);

            return Task.CompletedTask;
        }
    }
}
