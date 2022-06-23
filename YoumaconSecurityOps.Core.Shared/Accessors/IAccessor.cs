using System.Linq.Expressions;

namespace YoumaconSecurityOps.Core.Shared.Accessors;

/// <summary>
/// Defines methods for retrieving entities of  Type <typeparamref name="T"/> from the database
/// </summary>
/// <typeparam name="T"></typeparam>
/// <remarks>Only defines READ methods</remarks>
public interface IAccessor<T>
{
    /// <summary>
    /// Retrieves all entities of type <typeparam name="T"></typeparam> from their respective tables in the database
    /// </summary>    
    /// <param name="dbContext">The Caller Supplied DbContext</param>
    /// <param name="cancellationToken"></param>
    /// <returns>An asynchronous stream of the entities (<see cref="IAsyncEnumerable{T}"/>) of type <typeparamref name="T"/></returns>
    IAsyncEnumerable<T> GetAllAsync(CancellationToken cancellationToken = new ());

    /// <summary>
    /// Retrieves all entities of type <typeparam name="T"></typeparam> that match the given <param name="predicate"></param>
    /// </summary>
    /// <param name="dbContext">The Caller supplied DbContext</param>
    /// <param name="predicate">A list of conditions to match against</param>
    /// <param name="cancellationToken"></param>
    /// <returns>An asynchronous stream of the entities (<see cref="IAsyncEnumerable{T}"/>) of type <typeparamref name="T"/></returns>
    IAsyncEnumerable<T> GetAllThatMatchAsync(Expression<Func<T,bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a single entity of Type <c>T</c> from it's respective table in the database
    /// </summary>
    /// <param name="dbContext">The Caller Supplied DbContext</param>
    /// <param name="entityId">The supplied Id for lookup</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A <see cref="Task{T}"/> of <typeparamref name="T"/></returns>
    Task<T> WithIdAsync(Guid entityId, CancellationToken cancellationToken = new());
}