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
    internal sealed class EventStoreRepository: IEventStoreRepository
    {
        private readonly EventStoreDbContext _dbContext;

        //private readonly ILogger<EventStoreRepository> _logger;

        public EventStoreRepository(EventStoreDbContext dbContext)//,ILogger<EventStoreRepository> logger)
        {
            _dbContext = dbContext;
            //_logger = logger;
        }

        public IAsyncEnumerator<EventReader> GetAsyncEnumerator(CancellationToken cancellationToken = new ())
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

        public IAsyncEnumerable<EventReader> GetAllByAggregateId(String aggregateId, CancellationToken cancellationToken = default)
        {
            var events = _dbContext.Events
                .AsAsyncEnumerable()
                .Where(e => e.AggregateId.Equals(aggregateId));

            return events;
        }

        public async Task<IEnumerable<EventReader>> GetAllByAggregateIdAsync(string aggregateId, CancellationToken cancellationToken = default)
        {
            var eventsWithMatchedAggregateId = (await GetAllAsync(cancellationToken))
                .Where(e => e.AggregateId.Equals(aggregateId));

            return eventsWithMatchedAggregateId;
        }


        public async Task SaveAsync(String aggregateId, int originatingVersion, IReadOnlyCollection<EventReader> events, string aggregateName = "Aggregate Name", CancellationToken cancellationToken = default)
        {
            if (events.Count == 0)
            {
                return;
            }
            

            var listOfEvents = events.Select(ev => new EventReader 
            {
                Aggregate = aggregateName,
                CreatedAt = ev.CreatedAt,
                Data = JsonSerializer.Serialize(ev),
                Id = Guid.NewGuid(),
                Name = ev.GetType().Name,
                AggregateId = aggregateId,
                MinorVersion = ++originatingVersion,
                MajorVersion = ev.MajorVersion
            });

            await _dbContext.Events.AddRangeAsync(listOfEvents, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
