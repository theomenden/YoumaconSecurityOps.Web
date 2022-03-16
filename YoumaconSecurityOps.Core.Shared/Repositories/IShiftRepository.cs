namespace YoumaconSecurityOps.Core.Shared.Repositories;

/// <summary>
/// For methods referring to the storage and updating of <see cref="ShiftReader"/> in the database
/// </summary>
/// <remarks>Implements <seealso cref="IRepository{T}"/></remarks>
public interface IShiftRepository: IRepository<ShiftReader>
{
    /// <summary>
    /// Updates a particular shift's Check In date, and notes it in the database.
    /// </summary>
    /// <param name="shiftId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The updated <see cref="ShiftReader"/></returns>
    Task<ShiftReader> CheckIn(YoumaconSecurityDbContext dbContext, Guid shiftId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a particular shift's Check Out date, and notes it in the database
    /// </summary>
    /// <param name="shiftId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The updated <see cref="ShiftReader"/></returns>
    Task<ShiftReader> CheckOut(YoumaconSecurityDbContext dbContext, Guid shiftId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a particular shift's <paramref name="currentLocationId"/> that the shift is reporting from.
    /// </summary>
    /// <param name="shiftId"></param>
    /// <param name="currentLocationId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The updated <see cref="ShiftReader"/></returns>
    Task<ShiftReader> ReportIn(YoumaconSecurityDbContext dbContext, Guid shiftId, Guid currentLocationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a shift's current location
    /// </summary>
    /// <param name="dbContext">The caller provided <see cref="DbContext"/></param>
    /// <param name="shiftId">The <see cref="ShiftReader"/> id to modify</param>
    /// <param name="locationId">The <see cref="LocationReader"/> id to set current location to</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Updated <see cref="ShiftReader"/></returns>
    Task<ShiftReader> UpdateCurrentLocation(YoumaconSecurityDbContext dbContext, Guid shiftId, Guid locationId, CancellationToken cancellationToken = default);
}