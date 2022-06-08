namespace YoumaconSecurityOps.Core.EventStore.Storage;

/// <summary>
/// <inheritdoc cref="IEventStoreRepository"/>
/// </summary>
internal sealed class EventStoreRepository : IEventStoreRepository
{
    private readonly IDbContextFactory<EventStoreDbContext> _dbContext;

    private readonly ILogger<EventStoreRepository> _logger;

    public EventStoreRepository(IDbContextFactory<EventStoreDbContext> dbContext,ILogger<EventStoreRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public IAsyncEnumerator<EventReader> GetAsyncEnumerator(CancellationToken cancellationToken = new())
    {
        using var context = _dbContext.CreateDbContext();

        var eventStoreAsyncEnumerator = GetAll(context, cancellationToken).GetAsyncEnumerator(cancellationToken);

        return eventStoreAsyncEnumerator;
    }

    public IAsyncEnumerable<EventReader> GetAll(EventStoreDbContext dbContext, CancellationToken cancellationToken = default)
    {
        using var context = _dbContext.CreateDbContext();

        var events = context.Events
            .OrderBy(e => e.Name)
            .ThenBy(e => e.MajorVersion)
            .ThenBy(e => e.MinorVersion)
            .AsAsyncEnumerable();

        return events;
    }

    public async Task<IEnumerable<EventReader>> GetAllAsync(EventStoreDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var eventsAsIEnumerable = await dbContext.Events.ToListAsync(cancellationToken);

        return eventsAsIEnumerable;
    }

    public IAsyncEnumerable<EventReader> GetAllByAggregateId(EventStoreDbContext dbContext, Guid aggregateId, CancellationToken cancellationToken = default)
    {
        var events = dbContext.Events
            .Where(e => e.AggregateId == aggregateId)
            .AsAsyncEnumerable();

        return events;
    }

    public async Task<IEnumerable<EventReader>> GetAllByAggregateIdAsync(EventStoreDbContext dbContext, Guid aggregateId, CancellationToken cancellationToken = default)
    {
        var eventsWithMatchedAggregateId = await dbContext.Events
            .Where(e => e.AggregateId == aggregateId)
            .AsQueryable()
            .ToListAsync(cancellationToken);

        return eventsWithMatchedAggregateId;
    }
    
    public async Task SaveAsync(EventStoreDbContext dbContext, Guid aggregateId, int originatingVersion, string callerName, IReadOnlyCollection<EventReader> events, string aggregateName = "Aggregate Name", CancellationToken cancellationToken = default)
    {
        if (!events.Any())
        {
            return;
        }
            
        var listOfEvents = events.Select(ev => new EventReader
        { 
            Id = Guid.NewGuid(),
            AggregateId = aggregateId,
            Aggregate = aggregateName,
            Data = JsonSerializer.Serialize(ev.Data),
            Name = callerName,
            MinorVersion = ++originatingVersion,
            MajorVersion = ev.MajorVersion
        });

        try
        {
            dbContext.Events.AddRange(listOfEvents);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception while trying to store event: {@ex}", ex);
            throw;
        }
    }
    public async Task ApplyInitialEventAsync(EventStoreDbContext dbContext, EventReader initialEvent, CancellationToken cancellationToken = default)
    {
        dbContext.Events.Add(initialEvent);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task ApplyNextEventAsync(EventStoreDbContext dbContext, EventReader nextEvent, CancellationToken cancellationToken = default)
    {
        var previousEvents = await dbContext.Events
            .Where(e => e.AggregateId == nextEvent.AggregateId)
            .AsQueryable()
            .ToListAsync(cancellationToken);
        
        if (!previousEvents.Any())
        {
            await ApplyInitialEventAsync(dbContext, nextEvent, cancellationToken).ConfigureAwait(false);
            return;
        }

        await SaveAsync(dbContext, 
            nextEvent.AggregateId,
            previousEvents.MaxBy(ev => ev?.MajorVersion)?.MinorVersion ?? 1,
            nextEvent.Name,
            previousEvents.AsReadOnly(),
            nextEvent.Aggregate,
            cancellationToken).ConfigureAwait(false);
    }

}