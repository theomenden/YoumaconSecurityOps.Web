namespace YoumaconSecurityOps.Core.Shared.Accessors;

/// <summary>
/// Contains read methods for <see cref="IncidentReader"/> in the database
/// </summary>
public interface IIncidentAccessor: IAccessor<IncidentReader>
{
    /// <summary>
    /// Retrieves an async stream of incidents that occurred under a particular shift
    /// </summary>
    /// <param name="dbContext">The caller supplied <see cref="DbContext"/></param>
    /// <param name="shiftId">The shift that the incidents occurred</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="IAsyncEnumerable{T}"/>: <seealso cref="IncidentReader"/></returns>
    IAsyncEnumerable<IncidentReader> GetByShiftId(YoumaconSecurityDbContext dbContext, Guid shiftId, CancellationToken cancellationToken = new());

    /// <summary>
    /// Retrieves an async stream of incidents of a certain severity
    /// </summary>
    /// <param name="dbContext">The caller supplied <see cref="DbContext"/></param>
    /// <param name="severityToSearch"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="IAsyncEnumerable{T}"/>: <seealso cref="IncidentReader"/></returns>
    IAsyncEnumerable<IncidentReader> GetBySeverity(YoumaconSecurityDbContext dbContext, Severity severityToSearch, CancellationToken cancellationToken = new());

    /// <summary>
    /// Retrieves an async stream incidents by the staff member who initially reported them.
    /// </summary>
    /// <param name="dbContext">The caller supplied <see cref="DbContext"/></param>
    /// <param name="reportingStaffId">The reporting <see cref="StaffReader"/> id</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="IAsyncEnumerable{T}"/>: <seealso cref="IncidentReader"/></returns>
    IAsyncEnumerable<IncidentReader> GetByReportedStaffMember(YoumaconSecurityDbContext dbContext, Guid reportingStaffId, CancellationToken cancellationToken = new());

    /// <summary>
    /// Retrieves an async stream incidents by the staff member who recorded them.
    /// </summary>
    /// <param name="dbContext">The caller supplied <see cref="DbContext"/></param>
    /// <param name="recordingStaffId">The recording <see cref="StaffReader"/> id</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="IAsyncEnumerable{T}"/>: <seealso cref="IncidentReader"/></returns>
    IAsyncEnumerable<IncidentReader> GetByRecordedStaffMember(YoumaconSecurityDbContext dbContext, Guid recordingStaffId, CancellationToken cancellationToken = new());

}