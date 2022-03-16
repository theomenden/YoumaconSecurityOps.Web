namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal class AddStaffCommandHandler: IRequestHandler<AddStaffCommand, Guid>
{
    private readonly IDbContextFactory<EventStoreDbContext> _eventStoreDbContextFactory;

    private readonly IEventStoreRepository _eventStore;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    public AddStaffCommandHandler(IDbContextFactory<EventStoreDbContext> eventStoreDbContextFactory, IEventStoreRepository eventStore, IMapper mapper, IMediator mediator)
    {
        _eventStoreDbContextFactory = eventStoreDbContextFactory;
        _eventStore = eventStore;
        _mapper = mapper;
        _mediator = mediator;   
    }

    public async Task<Guid> Handle(AddStaffCommand request, CancellationToken cancellationToken)
    {
        await RaiseStaffCreatedEvent(request.StaffWriter, cancellationToken);

        return request.Id;
    }

    private async Task RaiseStaffCreatedEvent(StaffWriter createdStaffWriter, CancellationToken cancellationToken)
    {
        var e = new StaffCreatedEvent(createdStaffWriter)
        {
            Name = nameof(AddStaffCommand)
        };

        await using var context = await _eventStoreDbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var mappedEvent = _mapper.Map<EventReader>(e);

        await _eventStore.SaveAsync(context, mappedEvent, cancellationToken);

        await _mediator.Publish(e, cancellationToken);
    }
}
