using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.EventStore.Events;

namespace YoumaconSecurityOps.Core.EventStore.Storage
{
    /// <summary>
    /// <para>The base CRUD methods for the Security Operations Event Store</para>
    /// <inheritdoc cref="IAsyncEnumerable{T}"/>
    /// </summary>
    /// <remarks>Further Implementations must also define methods relevant to <see cref="IAsyncEnumerable{T}"/></remarks>
    public interface IEventStoreRepository: IAsyncEnumerable<EventReader>
    {
        /// <summary>
        /// Base Return method that gets all events from the <see cref="EventStoreDbContext"/> as an asynchronous stream
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="IAsyncEnumerable{T}"/> : <seealso cref="EventReader"/></returns>
        IAsyncEnumerable<EventReader> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieval method for all events as a single operation rather than a stream from <see cref="EventStoreDbContext"/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Task{T}"/>: <seealso cref="IEnumerable{T}"/> : <see cref="EventReader"/></returns>
        Task<IEnumerable<EventReader>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Return the Events that fall under a particular aggregate Id as an asynchronous stream
        /// </summary>
        /// <param name="aggregateId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="IAsyncEnumerable{T}"/>: <seealso cref="EventReader"/></returns>
        IAsyncEnumerable<EventReader> GetAllByAggregateId(Guid aggregateId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Return the events under a particular Aggregate as a single operation
        /// </summary>
        /// <param name="aggregateId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Task{T}"/>: <seealso cref="IEnumerable{T}"/>: <seealso cref="EventReader"/></returns>
        Task<IEnumerable<EventReader>> GetAllByAggregateIdAsync(Guid aggregateId, CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Saves a particular aggregate asynchronously with an existing <paramref name="aggregateId"/></para>
        /// <para><paramref name="originatingVersion"/> dictates the most recent minor version of the event</para>
        /// <para><paramref name="aggregateName"/> is typically the name of the original event source type</para>
        /// <para><paramref name="events"/> describes the previous events in the sequence they happened</para>
        /// <para>So this method adds this most recent event to the top of the aggregate event's log</para>
        /// </summary>
        /// <param name="aggregateId"></param>
        /// <param name="originatingVersion"></param>
        /// <param name="events"></param>
        /// <param name="aggregateName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Task"/></returns>
        Task SaveAsync(Guid aggregateId, int originatingVersion, IReadOnlyCollection<EventReader> events,
            string aggregateName = "Aggregate Name", CancellationToken cancellationToken = default);

        /// <summary>
        /// <para>Saves the first event as the starting point for other manipulations to take place on that particular aggregate</para>
        /// <para><paramref name="initialEvent"/> reflects the information created for this first event</para>
        /// </summary>
        /// <param name="initialEvent"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Task"/></returns>
        Task SaveAsync(EventReader initialEvent, CancellationToken cancellationToken = default);
    }
}
