namespace YoumaconSecurityOps.Core.Shared.Responses;

/// <summary>
/// A simplified response that is returned to the client
/// </summary>
public class ClientErrorPayload
{
    public string Message { get; set; }
    
    public Boolean IsError { get; set; }

    public Boolean IsValidationFailure { get; set; }

    public String Detail { get; set; }
}

/// <summary>
/// Generic wrapper for <see cref="ClientErrorPayload"/>
/// </summary>
/// <typeparam name="T">Type of data contained by the payload</typeparam>
public class ClientErrorPayload<T>: ClientErrorPayload
{
    public T Data { get; set; }
}

