namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class GetEventListQueryHandler : IStreamRequestHandler<GetEventListQuery, EventReader>
{
    private readonly IEventStoreRepository _eventStore;
        
    private readonly ILogger<GetEventListQueryHandler> _logger;
        
    public GetEventListQueryHandler(IEventStoreRepository eventStore, IEventStoreRepository events, ILogger<GetEventListQueryHandler> logger, IMapper mapper, IMediator mediator)
    {
        _eventStore = eventStore;
        _logger = logger;
    }
        

    public IAsyncEnumerable<EventReader> Handle(GetEventListQuery request, CancellationToken cancellationToken)
    {
        //_logger.LogInformation("User : {userName} Queried the event list", Guid.NewGuid().ToString());

        return _eventStore.GetAll(cancellationToken);
    }
}