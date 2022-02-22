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
    /// <param name="dbContext">The Caller Supplied DbContext</param>
    /// <param name="cancellationToken"></param>
    /// <returns>An asynchronous stream of the entities (<see cref="IAsyncEnumerable{T}"/>) of type <typeparamref name="T"/></returns>
    IAsyncEnumerable<T> GetAllAsync(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = new ());

    /// <summary>
    /// Returns a single entity of Type <c>T</c> from it's respective table in the database
    /// </summary>
    /// <param name="dbContext">The Caller Supplied DbContext</param>
    /// <param name="entityId">The supplied Id for lookup</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A <see cref="Task{T}"/> of <typeparamref name="T"/></returns>
    Task<T> WithIdAsync(YoumaconSecurityDbContext dbContext,Guid entityId, CancellationToken cancellationToken = new());
}