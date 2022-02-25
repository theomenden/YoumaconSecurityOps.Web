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
        var eventStoreAsyncEnumerator = GetAll(cancellationToken).GetAsyncEnumerator(cancellationToken);

        return eventStoreAsyncEnumerator;
    }

    public IAsyncEnumerable<EventReader> GetAll(CancellationToken cancellationToken = default)
    {
        using var context = _dbContext.CreateDbContext();

        var events = context.Events
            .AsAsyncEnumerable()
            .OrderBy(e => e.Name)
            .ThenBy(e => e.MajorVersion)
            .ThenBy(e => e.MinorVersion);

        return events;
    }

    public async Task<IEnumerable<EventReader>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var eventsAsIEnumerable = await context.Events.ToListAsync(cancellationToken);

        return eventsAsIEnumerable;
    }

    public IAsyncEnumerable<EventReader> GetAllByAggregateId(Guid aggregateId, CancellationToken cancellationToken = default)
    {
        using var context = _dbContext.CreateDbContext();


        var events = context.Events
            .AsAsyncEnumerable()
            .Where(e => e.Id == aggregateId);

        return events;
    }

    public async Task<IEnumerable<EventReader>> GetAllByAggregateIdAsync(Guid aggregateId, CancellationToken cancellationToken = default)
    {
        var eventsWithMatchedAggregateId = (await GetAllAsync(cancellationToken))
            .Where(e => e.Id.Equals(aggregateId));

        return eventsWithMatchedAggregateId;
    }


    public async Task SaveAsync(Guid aggregateId, int originatingVersion, IReadOnlyCollection<EventReader> events, string aggregateName = "Aggregate Name", CancellationToken cancellationToken = default)
    {
        if (!events.Any())
        {
            return;
        }
            
        var listOfEvents = events.Select(ev => new EventReader
        {
            Aggregate = aggregateName,
            Data = JsonSerializer.Serialize(ev),
            Name = ev.GetType().Name,
            MinorVersion = ++originatingVersion,
            MajorVersion = ev.MajorVersion
        });

        await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        context.Events.AddRange(listOfEvents);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveAsync(EventReader initialEvent, CancellationToken cancellationToken = default)
    {
        await using var context = await _dbContext.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("SaveAsync(EventReader initialEvent, CancellationToken cancellationToken = default): Attempting to add: {@initialEvent}", initialEvent);
            
        context.Events.Add(initialEvent);

        await context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Aggregate for {initialEvent} added", initialEvent.Id);
    }
}