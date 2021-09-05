using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.EventStore.Events;

namespace YoumaconSecurityOps.Core.EventStore.Storage
{
    /// <summary>
    /// <inheritdoc cref="IEventStoreRepository"/>
    /// </summary>
    internal sealed class EventStoreRepository : IEventStoreRepository
    {
        private readonly EventStoreDbContext _dbContext;

        private readonly ILogger<EventStoreRepository> _logger;

        public EventStoreRepository(EventStoreDbContext dbContext,ILogger<EventStoreRepository> logger)
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
            var events = _dbContext.Events
                .AsAsyncEnumerable()
                .OrderBy(e => e.Name)
                .ThenBy(e => e.MajorVersion)
                .ThenBy(e => e.MinorVersion);

            return events;
        }

        public async Task<IEnumerable<EventReader>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var eventsAsIEnumerable = await _dbContext.Events.ToListAsync(cancellationToken);

            return eventsAsIEnumerable;
        }

        public IAsyncEnumerable<EventReader> GetAllByAggregateId(Guid aggregateId, CancellationToken cancellationToken = default)
        {
            var events = _dbContext.Events
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

            await _dbContext.Events.AddRangeAsync(listOfEvents, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveAsync(EventReader initialEvent, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("SaveAsync(EventReader initialEvent, CancellationToken cancellationToken = default): Attempting to add: {@initialEvent}", initialEvent);
            await _dbContext.Events.AddAsync(initialEvent, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Aggregate for {initialEvent.Id} added", initialEvent);
        }
    }
}
