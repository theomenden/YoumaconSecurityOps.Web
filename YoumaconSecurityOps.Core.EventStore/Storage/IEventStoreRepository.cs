using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.EventStore.Events;

namespace YoumaconSecurityOps.Core.EventStore.Storage
{

    public interface IEventStoreRepository: IAsyncEnumerable<EventReader>
    {
        IAsyncEnumerable<EventReader> GetAll(CancellationToken cancellationToken = default);
        
        Task<IEnumerable<EventReader>> GetAllAsync(CancellationToken cancellationToken = default);

        IAsyncEnumerable<EventReader> GetAllByAggregateId(Guid aggregateId, CancellationToken cancellationToken = default);

        Task<IEnumerable<EventReader>> GetAllByAggregateIdAsync(Guid aggregateId, CancellationToken cancellationToken = default);

        Task SaveAsync(Guid aggregateId, int originatingVersion, IReadOnlyCollection<EventReader> events,
            string aggregateName = "Aggregate Name", CancellationToken cancellationToken = default);

        Task SaveAsync(EventReader initalEvent, CancellationToken cancellationToken = default);
    }
}
