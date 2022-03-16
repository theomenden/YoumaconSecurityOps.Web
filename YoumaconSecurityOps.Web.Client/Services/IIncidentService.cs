namespace YoumaconSecurityOps.Web.Client.Services;

public interface IIncidentService
{
    #region Query Methods
    Task<List<IncidentReader>> GetIncidentsAsync(GetIncidentsQuery incidentListQuery, CancellationToken cancellationToken = default);

    Task<List<IncidentReader>> GetIncidentsAsync(GetIncidentsWithParametersQuery incidentListQuery, CancellationToken cancellationToken = default);

    Task<IncidentReader> GetIncidentAsync(Guid incidentId, CancellationToken cancellationToken = default);
    #endregion
    #region Add Methods
    Task<ApiResponse<Guid>> AddIncidentAsync(AddIncidentCommandWithReturn addIncidentCommandWithReturn, CancellationToken cancellationToken = default);
    #endregion
    #region Mutation Methods
    Task<ApiResponse<Guid>> ResolveIncidentAsync(ResolveIncidentCommandWithReturn commandWithReturn, CancellationToken cancellationToken = default);

    Task<ApiResponse<Guid>> AdjustIncidentSeverityAsync(AdjustIncidentSeverityCommandWithReturn commandWithReturn, CancellationToken cancellationToken = default);
    #endregion
}