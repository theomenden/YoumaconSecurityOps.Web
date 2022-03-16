using System;
using YoumaconSecurityOps.Core.Shared.Enumerations;
using YoumaconSecurityOps.Core.Shared.Responses;

namespace YoumaconSecurityOps.Web.Client.Models;

/// <summary>
/// A container for responses that we get from the backend
/// </summary>
public class ApiResponse
{
    public ApiResponse()
    {
        Outcome = new()
        {
            Errors = Enumerable.Empty<String>(),
            Message = String.Empty,
            OperationResult = OperationResult.Success
        };
    }

    /// <value>
    /// Status code from the API
    /// </value>
    public ResponseCodes ResponseCode { get; set; }

    /// <value>
    /// Human Readable Representation of the Response Code
    /// </value>
    public String ResponseMessage { get; set; }
    
    public OperationOutcome Outcome { get; set; }
}

/// <summary>
/// <para>Generic Wrapper for <see cref="ApiResponse{T}"/></para>
/// <inheritdoc cref="ApiResponse"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public class ApiResponse<T>: ApiResponse
{
    /// <value>
    /// The value that our api returns
    /// </value>
    /// <remarks>On Success should return <typeparamref name="T"/>: On Failure should be <c>Null</c></remarks>
    public T Data { get; set; }
}