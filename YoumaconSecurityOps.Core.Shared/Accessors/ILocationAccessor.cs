namespace YoumaconSecurityOps.Core.Shared.Accessors;

public interface ILocationAccessor : IAccessor<LocationReader>
{
    /// <summary>
    /// Gets all the locations marked as hotels from the database as an asynchronous stream
    /// </summary>
    /// <param name="dbContext">The caller supplied <see cref="DbContext"/></param>
    /// <param name="cancellationToken"></param>
    /// <returns>An <see cref="IAsyncEnumerable{T}"/> where <c>T</c> is <seealso cref="LocationReader"/></returns>
    IAsyncEnumerable<LocationReader> GetHotels(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = new());

    Task<IEnumerable<LocationReader>> GetAllLocationsAsync(YoumaconSecurityDbContext dbContext, CancellationToken cancellationToken = new());
}