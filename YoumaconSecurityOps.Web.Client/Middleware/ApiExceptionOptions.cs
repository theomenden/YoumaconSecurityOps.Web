using Microsoft.AspNetCore.Http;
using YoumaconSecurityOps.Core.Shared.Responses;

namespace YoumaconSecurityOps.Web.Client.Middleware;

public class ApiExceptionOptions
{
    public Action<HttpContext, Exception, OperationOutcome> AddResponseDetails { get; set; }

    public Func<Exception, LogLevel> DetermineLogLevel { get; set; }
}

