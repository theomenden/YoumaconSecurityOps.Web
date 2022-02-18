namespace YoumaconSecurityOps.Core.Shared.Repositories;

/// <summary>
/// Contains methods to Update existing, or add new incidents in the database
/// </summary>
/// <remarks><see cref="IncidentReader"/> <seealso cref="IncidentWriter"/></remarks>
public interface IIncidentRepository: IRepository<IncidentReader>
{
    /// <summary>
    /// Adjusts the severity rating given by <paramref name="updatedSeverity"/> for the <paramref name="incidentToUpdate"/>
    /// </summary>
    /// <param name="incidentToUpdate"></param>
    /// <param name="updatedSeverity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><c>True</c> if update succeeds, and <c>False</c> otherwise</returns>
    Task<bool> UpdateIncidentSeverityAsync(Guid incidentToUpdate, Severity updatedSeverity, CancellationToken cancellationToken = new());

    /// <summary>
    /// Resolves the given <paramref name="incidentToResolve"/>
    /// </summary>
    /// <param name="incidentToResolve"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><c>True</c> if update succeeds, and <c>False</c> otherwise</returns>
    Task<bool> ResolveIncidentAsync(Guid incidentToResolve, CancellationToken cancellationToken = new());
}