namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class StaffCreatedEventHandler : INotificationHandler<StaffCreatedEvent>
{
    private readonly IStaffRepository _staff;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly ILogger<StaffCreatedEventHandler> _logger;

    public StaffCreatedEventHandler(IStaffRepository staff, IMapper mapper, IMediator mediator, ILogger<StaffCreatedEventHandler> logger)
    {
        _staff = staff;
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(StaffCreatedEvent notification, CancellationToken cancellationToken)
    {
        var staffMember = _mapper.Map<StaffReader>(notification.StaffWriter);

        await _staff.AddAsync(staffMember, cancellationToken);

        await RaiseStaffListUpdatedEvent(staffMember, cancellationToken);
    }

    private async Task RaiseStaffListUpdatedEvent(StaffReader staffReader, CancellationToken cancellationToken)
    {
        var e = new StaffListUpdatedEvent(staffReader);

        await _mediator.Publish(e, cancellationToken);
    }
}