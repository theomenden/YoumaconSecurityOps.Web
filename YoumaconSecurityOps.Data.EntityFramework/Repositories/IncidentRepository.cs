using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.Shared.Accessors;
using YoumaconSecurityOps.Core.Shared.Enumerations;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Core.Shared.Repositories;
using YoumaconSecurityOps.Data.EntityFramework.Context;

namespace YoumaconSecurityOps.Data.EntityFramework.Repositories
{
    internal sealed class IncidentRepository: IIncidentAccessor, IIncidentRepository
    {
        private readonly YoumaconSecurityDbContext _dbContext;

        private readonly ILogger<IncidentRepository> _logger;

        public IncidentRepository(YoumaconSecurityDbContext dbContext, ILogger<IncidentRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IAsyncEnumerable<IncidentReader> GetAll(CancellationToken cancellationToken = new ())
        {
            var incidents = _dbContext.Incidents.AsAsyncEnumerable();

            return incidents;
        }

        public async Task<IncidentReader> WithId(Guid entityId, CancellationToken cancellationToken = new ())
        {
            var incident = await _dbContext.Incidents.AsQueryable()
                .SingleOrDefaultAsync(i => i.Id == entityId, cancellationToken);

            return incident;
        }

        public IAsyncEnumerable<IncidentReader> GetByShiftId(Guid shiftId, CancellationToken cancellationToken = new())
        {
            var incidentsUnderShift = GetAll(cancellationToken)
                .Where(i => i.ShiftId == shiftId);

            return incidentsUnderShift;
        }

        public IAsyncEnumerable<IncidentReader> GetBySeverity(Severity severityToSearch, CancellationToken cancellationToken = new())
        {
            var incidentsUnderShift = GetAll(cancellationToken)
                .Where(i => Enum.IsDefined(typeof(Severity), i.Severity) && i.Severity ==(int)severityToSearch);

            return incidentsUnderShift;
        }

        public IAsyncEnumerable<IncidentReader> GetByReportedStaffMember(Guid staffId, CancellationToken cancellationToken = new())
        {
            var incidentsUnderShift = GetAll(cancellationToken).Where(i => i.ReportedById == staffId);

            return incidentsUnderShift;
        }

        public IAsyncEnumerable<IncidentReader> GetByRecordedStaffMember(Guid staffId, CancellationToken cancellationToken = new())
        {
            var incidentsUnderShift = GetAll(cancellationToken).Where(i => i.RecordedById == staffId);

            return incidentsUnderShift;
        }

        public IAsyncEnumerator<IncidentReader> GetAsyncEnumerator(CancellationToken cancellationToken = new ())
        {
            var incidentAsyncEnumerator = GetAll(cancellationToken).GetAsyncEnumerator(cancellationToken);

            return incidentAsyncEnumerator;
        }

        public async Task<bool> AddAsync(IncidentReader entity, CancellationToken cancellationToken = default)
        {
            bool addResult;

            try
            {

                await _dbContext.Incidents.AddAsync(entity, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                addResult = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception occurred while trying to add incident to database: {incident}, exception: {ex}", entity.Title, ex?.InnerException?.Message ?? ex.Message);
                addResult = false;
            }

            return addResult;
        }

        public async Task<bool> UpdateIncidentSeverityAsync(Guid incidentToUpdate, Severity updatedSeverity, CancellationToken cancellationToken = new())
        {
            bool updateIncidentResult;

            try
            {
                var incident = await _dbContext.Incidents.AsQueryable().SingleOrDefaultAsync(i => i.Id == incidentToUpdate, cancellationToken);

                incident.Severity = (int)updatedSeverity;

                await _dbContext.SaveChangesAsync(cancellationToken);

                updateIncidentResult = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception occurred while trying to resolve the incident {incidentId}, {ex}", incidentToUpdate, ex?.InnerException?.Message ?? ex.Message);
                updateIncidentResult = false;
            }

            return updateIncidentResult;
        }

        public async Task<bool> ResolveIncidentAsync(Guid incidentToResolve, CancellationToken cancellationToken = new())
        {
            bool updateIncidentResult;

            try
            {
                var incident = await _dbContext.Incidents.AsQueryable().SingleOrDefaultAsync(i => i.Id == incidentToResolve, cancellationToken);

                incident.Severity = (int)Severity.Resolved;

                await _dbContext.SaveChangesAsync(cancellationToken);

                updateIncidentResult = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception occurred while trying to resolve the incident {incidentId}, {ex}", incidentToResolve, ex?.InnerException?.Message ?? ex.Message);
                updateIncidentResult = false;
            }

            return updateIncidentResult;
        }
    }
}
