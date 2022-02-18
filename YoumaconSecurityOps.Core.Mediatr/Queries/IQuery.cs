using MediatR;

namespace YoumaconSecurityOps.Core.Mediatr.Queries;

/// <summary>
/// Base Query Properties for the Mediatr Layer
/// </summary>
public interface IQuery
{
    /// <value>
    /// Unique Id Generated per query
    /// </value>
    Guid Id { get; }

    /// <value>
    /// A signifier for where the query originated
    /// </value>
    String Issuer { get; }

    /// <value>
    /// Date and time the query was issued.
    /// </value>
    DateTime CreatedAt { get; }
}

/// <summary>
/// <inheritdoc cref="IQuery"/> for standard <see cref="IRequest"/>s that return a value <typeparamref name="T"/>
/// <inheritdoc cref="IRequest{T}"/>
/// </summary>
/// <typeparam name="T">Value to be returned </typeparam>
public interface IQuery<out T>: IQuery ,IRequest<T>
{
}

/// <summary>
/// <inheritdoc cref="IQuery"/> for streaming <see cref="IStreamRequest{TResponse}"/> that returns an asynchronous stream of values of type <typeparamref name="T"/>
/// <inheritdoc cref="IStreamRequest{TResponse}"/>
/// </summary>
/// <typeparam name="T">Asynchronous stream of values to be returned</typeparam>
public interface IStreamingQuery<out T> : IQuery, IStreamRequest<T>
{
}