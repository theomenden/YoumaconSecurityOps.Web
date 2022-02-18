namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class AddShiftCommandHandler: IRequestHandler<AddShiftCommand, Guid> //AsyncRequestHandler<AddShiftCommand>
{
    private readonly IEventStoreRepository _eventStore;

    private readonly ILogger<AddShiftCommandHandler> _logger;
        
    private readonly IMapper _mapper;

    private readonly IMediator _mediator;
        
    public AddShiftCommandHandler(IEventStoreRepository eventStore, ILogger<AddShiftCommandHandler> logger, IMapper mapper,IMediator mediator)
    {
        _eventStore = eventStore;

        _logger = logger;

        _mapper = mapper;

        _mediator = mediator;
    }

    public async Task<Guid> Handle(AddShiftCommand request, CancellationToken cancellationToken)
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
            Name = nameof(AddShiftCommandHandler)
        };
            
        await _eventStore.SaveAsync(_mapper.Map<EventReader>(e), cancellationToken);

        await _mediator.Publish(e, cancellationToken);
    }
}