namespace YoumaconSecurityOps.Core.Shared.Accessors;

/// <summary>
/// Defines methods for retrieving entities of  Type <typeparamref name="T"/> from the database
/// </summary>
/// <typeparam name="T"></typeparam>
/// <remarks>Only defines READ methods</remarks>
public interface IAccessor<T>
{
    /// <summary>
    /// Retrieves all entities types of Type <c>T</c> from their respective tables in the database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>An asynchronous stream of the entities (<see cref="IAsyncEnumerable{T}"/>)</returns>
    IAsyncEnumerable<T> GetAll(CancellationToken cancellationToken = new ());

    /// <summary>
    /// Returns a single entity of Type <c>T</c> from it's respective table in the database
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A <see cref="Task{T}"/></returns>
    Task<T> WithId(Guid entityId, CancellationToken cancellationToken = new());
}