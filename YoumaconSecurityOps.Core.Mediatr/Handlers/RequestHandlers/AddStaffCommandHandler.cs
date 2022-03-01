namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal class AddStaffCommandHandler: IRequestHandler<AddStaffCommand, Guid>
{
    private readonly ILogger<AddStaffCommandHandler> _logger;


    private readonly IMediator _mediator;

    public AddStaffCommandHandler(ILogger<AddStaffCommandHandler> logger,  IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;   
    }

    public async Task<Guid> Handle(AddStaffCommand request, CancellationToken cancellationToken)
    {
        await RaiseStaffCreatedEvent(request.StaffWriter, cancellationToken);

        return request.Id;
    }

    private async Task RaiseStaffCreatedEvent(StaffWriter createdStaffReader, CancellationToken cancellationToken)
    {
        var e = new StaffCreatedEvent(createdStaffReader);

        await _mediator.Publish(e, cancellationToken);
    }
}
