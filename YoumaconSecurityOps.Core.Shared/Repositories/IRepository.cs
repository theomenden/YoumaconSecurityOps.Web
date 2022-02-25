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
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Boolean"/>: True if operation succeeds: False if anything else happens</returns>
    Task<bool> AddAsync(YoumaconSecurityDbContext dbContext, T entity, CancellationToken cancellationToken = default);
}