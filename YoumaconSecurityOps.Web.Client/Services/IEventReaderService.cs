using System.Threading;
using YoumaconSecurityOps.Core.EventStore.Events;

namespace YoumaconSecurityOps.Web.Client.Services;

/// <summary>
/// Service used to display results of our event sourcing implementation to administrative users.
/// </summary>
public interface IEventReaderService
{
    /// <summary>
    /// Retrieves an asynchronous stream of all the events in the application. 
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Task{T}"/>: <seealso cref="List{T}"/>: <seealso cref="EventReader"/></returns>
    Task<List<EventReader>> GetAllEventsAsync(GetEventListQuery query, CancellationToken cancellationToken = default);
}