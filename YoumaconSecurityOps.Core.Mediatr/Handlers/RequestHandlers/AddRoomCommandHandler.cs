
namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal class AddRoomCommandHandler : IRequestHandler<AddRoomCommandWithReturn, Guid>
{
    private readonly IDbContextFactory<EventStoreDbContext> _dbContextFactory;
    private readonly IEventStoreRepository _eventStore;
    private readonly IMediator _mediator;

    public AddRoomCommandHandler(IDbContextFactory<EventStoreDbContext> dbContextFactory, IEventStoreRepository eventStore, IMediator mediator)
    {
        _dbContextFactory = dbContextFactory;
        _eventStore = eventStore;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(AddRoomCommandWithReturn request, CancellationToken cancellationToken)
    {
        var roomWriter = new RoomScheduleWriter(request.StaffId, true, request.RoomNumber, 1, request.LocationId);

        await RaiseRoomCreatedEvent(roomWriter, cancellationToken);

        return roomWriter.Id;
    }

    private async Task RaiseRoomCreatedEvent(RoomScheduleWriter writer, CancellationToken cancellationToken = default)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var e = new RoomCreatedEvent(writer);

        await _mediator.Publish(e, cancellationToken);

        var mappedEvent = new EventReader
        {
            AggregateId = writer.Id,
            Aggregate = $"{nameof(RoomScheduleWriter)}-{writer.Id}",
            CreatedAt = DateTime.Now,
            Data = JsonSerializer.Serialize(writer),
            MajorVersion = 1,
            MinorVersion = 1,
            Name = $"{nameof(AddRoomCommandHandler)}"
        };

        await _eventStore.ApplyInitialEventAsync(context, mappedEvent, cancellationToken);
    }
}

