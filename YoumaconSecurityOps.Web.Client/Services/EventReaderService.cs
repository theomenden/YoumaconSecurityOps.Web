using System.Threading;
using Microsoft.Extensions.Logging;
using YoumaconSecurityOps.Core.EventStore.Events;

namespace YoumaconSecurityOps.Web.Client.Services;

public class EventReaderService: IEventReaderService
{
    private readonly IMediator _mediator;

    private readonly ILogger<EventReaderService> _logger;

    public EventReaderService(IMediator mediator, ILogger<EventReaderService> logger)
    {
        _mediator = mediator;
        _logger = logger;   
    }

    public async Task<List<EventReader>> GetAllEventsAsync(GetEventListQuery query, CancellationToken cancellationToken = default)
    {
        return await _mediator.CreateStream(query, cancellationToken).ToListAsync(cancellationToken);
    }
}