using System;
using MediatR;

namespace YoumaconSecurityOps.Core.Mediatr.Queries
{
    /// <summary>
    /// <inheritdoc cref="IRequest{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQuery<out T>: IRequest<T>
    {
        Guid Id { get; }
    }
}
