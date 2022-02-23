using System.Threading;

namespace YoumaconSecurityOps.Web.Client.Services
{
    public interface IIncidentService
    {
        #region Query Methods
        Task<List<IncidentReader>> GetIncidentsAsync(GetIncidentsQuery incidentListQuery, CancellationToken cancellationToken = default);

        Task<List<IncidentReader>> GetIncidentsAsync(GetIncidentsWithParametersQuery incidentListQuery, CancellationToken cancellationToken = default);

        Task<IncidentReader> GetIncidentAsync(Guid incidentId, CancellationToken cancellationToken = default);
        #endregion
        #region Add Methods
        Task<ApiResponse<Guid>> AddIncidentAsync(AddIncidentCommand addIncidentCommand, CancellationToken cancellationToken = default);
        #endregion
        #region Mutation Methods
        Task<ApiResponse<Guid>> ResolveIncidentAsync(ResolveIncidentCommand command, CancellationToken cancellationToken = default);

        Task<ApiResponse<Guid>> AdjustIncidentSeverityAsync(AdjustIncidentSeverityCommand command, CancellationToken cancellationToken = default);
        #endregion
    }
}
