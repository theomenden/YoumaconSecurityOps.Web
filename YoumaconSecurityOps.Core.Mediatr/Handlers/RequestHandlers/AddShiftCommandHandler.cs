namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class AddShiftCommandHandler: IRequestHandler<AddShiftCommandWithReturn, Guid> //AsyncRequestHandler<AddShiftCommandWithReturn>
{

    private readonly IDbContextFactory<EventStoreDbContext> _eventStoreDbContextFactory;

    private readonly IEventStoreRepository _eventStore;
    
    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    public AddShiftCommandHandler(IDbContextFactory<EventStoreDbContext> eventStoreDbContextFactory, IEventStoreRepository eventStore, IMapper mapper, IMediator mediator)
    {
        _eventStoreDbContextFactory = eventStoreDbContextFactory;
        _eventStore = eventStore;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(AddShiftCommandWithReturn request, CancellationToken cancellationToken)
    {
        var shiftWriter = new ShiftWriter(request.StartAt, request.EndAt, request.StaffMemberId, request.StaffMemberName,
            request.StartingLocationId);

        await RaiseShiftCreatedEvent(shiftWriter, cancellationToken);

        return shiftWriter.Id;
    }

    private async Task RaiseShiftCreatedEvent(ShiftWriter shiftWriter, CancellationToken cancellationToken)
    {
        var e = new ShiftCreatedEvent(shiftWriter)
        {
            Name = nameof(AddShiftCommandWithReturn)
        };

        await using var context =
            await _eventStoreDbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var eventReader = _mapper.Map<EventReader>(e);

        await _eventStore.SaveAsync(context, eventReader , cancellationToken).ConfigureAwait(false);
        
        await _mediator.Publish(e, cancellationToken).ConfigureAwait(false);
    }
    
}