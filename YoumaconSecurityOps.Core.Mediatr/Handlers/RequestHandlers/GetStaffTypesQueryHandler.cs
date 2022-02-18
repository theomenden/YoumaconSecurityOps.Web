namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class GetStaffTypesQueryHandler : IStreamRequestHandler<GetStaffTypesQuery, StaffType>
{
    private readonly IStaffTypeAccessor _staffTypes;

    private readonly IEventStoreRepository _eventStore;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly ILogger<GetStaffTypesQueryHandler> _logger;

    public GetStaffTypesQueryHandler(IStaffTypeAccessor staffTypes, IMediator mediator, ILogger<GetStaffTypesQueryHandler> logger, IEventStoreRepository eventStore, IMapper mapper)
    {
        _staffTypes = staffTypes;
        _mediator = mediator;
        _logger = logger;
        _eventStore = eventStore;
        _mapper = mapper;
    }

    public async IAsyncEnumerable<StaffType> Handle(GetStaffTypesQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var staff = _staffTypes.GetAll(cancellationToken);

        await RaiseStaffTypesQueriedEvent(request, cancellationToken);

        await foreach (var member in staff.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            yield return member;
        }
    }

    private async Task RaiseStaffTypesQueriedEvent(GetStaffTypesQuery staffRoleQueryRequest, CancellationToken cancellationToken)
    {
        var e = new StaffTypesQueriedEvent(null)
        {
            Aggregate = nameof(GetStaffTypesQuery),
            MajorVersion = 1,
            Name = nameof(staffRoleQueryRequest)
        };

        _logger.LogInformation("Logged event of type {StaffTypesQueriedEvent} {e}, {request}, {RaiseStaffTypesQueriedEvent}", typeof(StaffTypesQueriedEvent), e, staffRoleQueryRequest, nameof(RaiseStaffTypesQueriedEvent));

        await _eventStore.SaveAsync(_mapper.Map<EventReader>(e), cancellationToken);

        await _mediator.Publish(e, cancellationToken);
    }
}