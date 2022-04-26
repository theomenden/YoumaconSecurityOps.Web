namespace YoumaconSecurityOps.Web.Client.Services;

public class EventReaderService: IEventReaderService
{
    private readonly IMediator _mediator;


    public EventReaderService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<EventReader>> GetAllEventsAsync(GetEventListQuery query, CancellationToken cancellationToken = default)
    {
        return await _mediator.CreateStream(query, cancellationToken).ToListAsync(cancellationToken);
    }
}