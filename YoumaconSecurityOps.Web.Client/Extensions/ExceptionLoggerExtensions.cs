namespace YoumaconSecurityOps.Web.Client.Extensions;

public static class ExceptionLoggerExtensions
{
    /// <summary>
    /// Sets the application up to use the custom <see cref="ExceptionLogger"/> middleware
    /// </summary>
    /// <param name="builder"></param>
    /// <returns>The <see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder)
    {
        var options = new ApiExceptionOptions();

        return builder.UseMiddleware<ExceptionLogger>(options);
    }

    /// <summary>
    /// Sets the application up to use the custom <see cref="ExceptionLogger"/> middleware, and customize it's behavior with <seealso cref="ApiExceptionOptions"/>
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configureOptions">Options to define the behavior of the logging middleware</param>
    /// <returns>The <see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder,
        Action<ApiExceptionOptions> configureOptions)
    {
        var options = new ApiExceptionOptions();
        configureOptions(options);
        
        return builder.UseMiddleware<ExceptionLogger>(options);
    }
}

