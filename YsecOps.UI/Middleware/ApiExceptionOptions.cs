namespace YsecOps.UI.Middleware;

public sealed class ApiExceptionOptions
{
    public Action<HttpContext, Exception, OperationOutcome> AddResponseDetails { get; set; }

    public Func<Exception, LogLevel> DetermineLogLevel { get; set; }
}