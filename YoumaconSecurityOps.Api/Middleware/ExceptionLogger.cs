using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace YoumaconSecurityOps.Api.Middleware
{
    public class ExceptionLogger
    {
        private readonly RequestDelegate _next;

        private readonly ILogger _logger;

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
                throw;
            }
        }

        private Task HandleExceptionAsync(Exception ex)
        {
            _logger.LogError(ex.Message);

            return Task.CompletedTask;
        }
    }
}
