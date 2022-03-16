namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal class GetRoomScheduleQueryHandler: IStreamRequestHandler<GetRoomScheduleQuery, RoomScheduleReader>
{
    private readonly IRoomScheduleAccessor _rooms;

    private readonly ILogger<GetRoomScheduleQueryHandler> _logger;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;
    
    public GetRoomScheduleQueryHandler(IRoomScheduleAccessor rooms, ILogger<GetRoomScheduleQueryHandler> logger, IMapper mapper, IMediator mediator, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory)
    {
        _rooms = rooms;
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
        _dbContextFactory = dbContextFactory;   
    }

    public async IAsyncEnumerable<RoomScheduleReader> Handle(GetRoomScheduleQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        await foreach (var room in _rooms.GetAllAsync(context, cancellationToken).ConfigureAwait(false))
        {
            yield return room;
        }
    }
}

