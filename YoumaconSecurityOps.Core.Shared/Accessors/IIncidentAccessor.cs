namespace YoumaconSecurityOps.Core.Shared.Accessors;

/// <summary>
/// Contains read methods for <see cref="IncidentReader"/> in the database
/// </summary>
public interface IIncidentAccessor: IAccessor<IncidentReader>
{
    /// <summary>
    /// Retrieves an async stream of incidents that occurred under a particular shift
    /// </summary>
    /// <param name="shiftId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="IAsyncEnumerable{T}"/>: <seealso cref="IncidentReader"/></returns>
    IAsyncEnumerable<IncidentReader> GetByShiftId(Guid shiftId, CancellationToken cancellationToken = new());

    /// <summary>
    /// Retrieves an async stream of incidents of a certain severity
    /// </summary>
    /// <param name="severityToSearch"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="IAsyncEnumerable{T}"/>: <seealso cref="IncidentReader"/></returns>
    IAsyncEnumerable<IncidentReader> GetBySeverity(Severity severityToSearch, CancellationToken cancellationToken = new());

    /// <summary>
    /// Retrieves an async stream incidents by the staff member who initially reported them.
    /// </summary>
    /// <param name="staffId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="IAsyncEnumerable{T}"/>: <seealso cref="IncidentReader"/></returns>
    IAsyncEnumerable<IncidentReader> GetByReportedStaffMember(Guid staffId, CancellationToken cancellationToken = new());

    /// <summary>
    /// Retrieves an async stream incidents by the staff member who recorded them.
    /// </summary>
    /// <param name="staffId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="IAsyncEnumerable{T}"/>: <seealso cref="IncidentReader"/></returns>
    IAsyncEnumerable<IncidentReader> GetByRecordedStaffMember(Guid staffId, CancellationToken cancellationToken = new());

}