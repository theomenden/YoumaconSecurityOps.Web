namespace YoumaconSecurityOps.Web.Client.IndexedDb.Repositories;

public interface IIndexedDbRepository
{
    /// <summary>
    /// Removes all records from a particular <see cref="Store"/>
    /// </summary>
    /// <param name="cancellationToken"></param>
    Task ClearAsync(CancellationToken cancellationToken = default);
}

public interface IIndexedDbRepository<T>: IIndexedDbRepository
{
    /// <summary>
    /// Retrieves all the <typeparamref name="T"/>s from the <see cref="IndexedDb"/>
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{T}"/>: <see cref="List{T}"/>: where T: <typeparamref name="T"/></returns>
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds or updates the provided <paramref name="entity"/> to the <see cref="IndexedDb"/>
    /// </summary>
    /// <param name="entity">The item to add</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns the added entity, <typeparamref name="T"/></returns>
    Task<T> CreateOrUpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds the respective <typeparamref name="T" />s to the <see cref="IndexedDb"/> as a single Transaction.
    /// </summary>
    /// <typeparam name="T">The type of entities to add, determining the <see cref="Store"/></typeparam>
    /// <param name="entities">The supplied entities</param>
    /// <param name="cancellationToken"></param>
    Task CreateOrUpdateMultipleAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if the underlying store contains any data
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns><c>True</c> if there is any value in the store</returns>
    Task<bool> IsEmptyAsync(CancellationToken cancellationToken = default);
}
