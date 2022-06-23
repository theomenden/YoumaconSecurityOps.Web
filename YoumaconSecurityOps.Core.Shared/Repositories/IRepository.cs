namespace YoumaconSecurityOps.Core.Shared.Repositories;

/// <summary>
/// <para>The base repository methods.</para>
/// <inheritdoc cref="IAsyncEnumerable{T}"/>
/// </summary>
/// <typeparam name="T"></typeparam>
/// <remarks>Implementations must define methods relevant to <see cref="IAsyncEnumerable{T}"/></remarks>
public interface IRepository<T>: IAsyncEnumerable<T>
{
    /// <summary>
    /// Adds a given <paramref name="entity"/> to the database asynchronously
    /// </summary>
    /// <param name="dbContext">The caller supplied <see cref="YoumaconSecurityDbContext"/></param>
    /// <param name="entity">The <typeparamref name="T" /> entity we are trying to add to the respective table</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">Type of entity to add</typeparam>
    /// <returns><see cref="Boolean"/>: True if operation succeeds: False if anything else happens</returns>
    Task<bool> AddAsync(T entity, CancellationToken cancellationToken = default);
}