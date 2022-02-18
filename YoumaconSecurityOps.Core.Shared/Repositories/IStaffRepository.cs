namespace YoumaconSecurityOps.Core.Shared.Repositories;

/// <summary>
/// Defines methods relating to the Storage and Updating of <see cref="StaffReader"/> entries in the database
/// </summary>
/// <remarks>Inherits <see cref="IRepository{T}"/></remarks>
public interface IStaffRepository: IRepository<StaffReader>
{
    /// <summary>
    /// Updates the StaffMember provided by the <paramref name="staffId"/> to be put on a break
    /// </summary>
    /// <param name="staffId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The Staff Object on success, otherwise <c>null</c> on failure</returns>
    Task<StaffReader> SendOnBreak(Guid staffId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the StaffMember provided by the <paramref name="staffId"/> to be returned from their break
    /// </summary>
    /// <param name="staffId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The Staff Object on success, otherwise <c>null</c> on failure</returns>
    Task<StaffReader> ReturnFromBreak(Guid staffId, CancellationToken cancellationToken = default);
}