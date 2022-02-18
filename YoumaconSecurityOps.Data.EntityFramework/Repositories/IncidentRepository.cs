namespace YoumaconSecurityOps.Data.EntityFramework.Repositories;

internal sealed class IncidentRepository: IIncidentAccessor, IIncidentRepository
{
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContext;

    private readonly ILogger<IncidentRepository> _logger;

    public IncidentRepository(IDbContextFactory<YoumaconSecurityDbContext> dbContext, ILogger<IncidentRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public IAsyncEnumerable<IncidentReader> GetAll(CancellationToken cancellationToken = new ())
    {
        using var context = _dbContext.CreateDbContext();

        var incidents = context.Incidents.AsAsyncEnumerable();

        return incidents;
    }

    public async Task<IncidentReader> WithId(Guid entityId, CancellationToken cancellationToken = new ())
    {
        await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var incident = await context.Incidents.AsQueryable()
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
            await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

            context.Incidents.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

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
            await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

            var incident = await context.Incidents.AsQueryable().SingleOrDefaultAsync(i => i.Id == incidentToUpdate, cancellationToken);

            incident.Severity = (int)updatedSeverity;

            await context.SaveChangesAsync(cancellationToken);

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
            await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);


            var incident = await context.Incidents.AsQueryable().SingleOrDefaultAsync(i => i.Id == incidentToResolve, cancellationToken);

            incident.Severity = (int)Severity.Resolved;

            await context.SaveChangesAsync(cancellationToken);

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