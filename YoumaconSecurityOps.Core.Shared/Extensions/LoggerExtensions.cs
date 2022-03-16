using Microsoft.Extensions.Logging;

namespace YoumaconSecurityOps.Core.Shared.Extensions;

/// <summary>
/// Extensions on <c>Microsoft.Extensions.Logging.</c><see cref="ILogger"/>
/// </summary>
public static class LoggerExtensions
{
    private static readonly Action<ILogger, Exception> BeforeValidatingMessageTrace;
    private static readonly Action<ILogger, String, String, Exception> InvalidMessageTrace;
    private static readonly Action<ILogger, String, String, String, Exception> ModelBinderUsed;
    private static readonly Action<ILogger, Int64, Exception> ProfileMessageTrace;
    private static readonly Action<ILogger, Exception> ValidMessageTrace;

    private const string PipelineBehaviour = "Pipeline Behaviour: ";

    static LoggerExtensions()
    {
        BeforeValidatingMessageTrace = LoggerMessage.Define(
            LogLevel.Debug,
            new EventId((int)TraceEventIdentifiers.BeforeValidatingMessageTrace, nameof(TraceBeforeValidatingMessage)),
            PipelineBehaviour + " Validating Message"
        );

        ProfileMessageTrace = LoggerMessage.Define<Int64>(
            LogLevel.Information,
            new EventId((int)TraceEventIdentifiers.ProfileMessagingTrace, nameof(TraceMessageProfiling)),
            PipelineBehaviour + " Request took {milliseconds} milliseconds"
        );

        InvalidMessageTrace = LoggerMessage.Define<String, String>(
            LogLevel.Debug,
            new EventId((int)TraceEventIdentifiers.InvalidMessageTrace, nameof(TraceMessageValidationFailed)),
            PipelineBehaviour + " Invalid Message. {message}. User: {user}"
        );

        ModelBinderUsed = LoggerMessage.Define<String, String,String>(
            LogLevel.Debug,
            new EventId((int)TraceEventIdentifiers.ModelBinderUsedTrace, nameof(TraceMessageModelBinderUsed)),
            "Parameter {modelName} of type {type} bound using ModelBinder \"{modelBinder}\""
        );

        ValidMessageTrace = LoggerMessage.Define(
            LogLevel.Debug,
            new EventId((int)TraceEventIdentifiers.ValidMessageTrace, nameof(TraceMessageValidationPassed)),
            PipelineBehaviour + " Message is valid"
        );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="milliseconds"></param>
    public static void TraceMessageProfiling(this ILogger logger, long milliseconds)
    {
        ProfileMessageTrace(logger, milliseconds, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="message"></param>
    /// <param name="user"></param>
    public static void TraceMessageValidationFailed(this ILogger logger, string message, string user)
    {
        InvalidMessageTrace(logger, message, user, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    public static void TraceBeforeValidatingMessage(this ILogger logger)
    {
        BeforeValidatingMessageTrace(logger, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="parameter"></param>
    /// <param name="type"></param>
    /// <param name="modelBinder"></param>
    public static void TraceMessageModelBinderUsed(this ILogger logger, string parameter, string type,
        string modelBinder)
    {
        ModelBinderUsed(logger, parameter, type, modelBinder, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    public static void TraceMessageValidationPassed(this ILogger logger)
    {
        ValidMessageTrace(logger, null);
    }
}

