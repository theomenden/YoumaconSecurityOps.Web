namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal class GetRoomScheduleQueryHandler: IStreamRequestHandler<GetRoomScheduleQuery, RoomScheduleReader>
{
    private readonly IRoomScheduleAccessor _rooms;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetRoomScheduleQueryHandler(IRoomScheduleAccessor rooms,
        IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory)
    {
        _rooms = rooms;
        _dbContextFactory = dbContextFactory;
    }

    public async IAsyncEnumerable<RoomScheduleReader> Handle(GetRoomScheduleQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        await foreach (var room in _rooms.GetAllAsync(context, cancellationToken))
        {
            yield return room;
        }
    }
}

