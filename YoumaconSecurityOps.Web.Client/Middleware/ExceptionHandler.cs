using System;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace YoumaconSecurityOps.Web.Client.Middleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        
        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            const HttpStatusCode code = HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(new { error = ex.Message });

            context.Response.ContentType = MediaTypeNames.Application.Json;

            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
