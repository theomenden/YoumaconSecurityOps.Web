namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetStaffTypesQueryHandler : IStreamRequestHandler<GetStaffTypesQuery, StaffType>
{
    private readonly IStaffTypeAccessor _staffTypes;

    private readonly IEventStoreRepository _eventStore;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly ILogger<GetStaffTypesQueryHandler> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetStaffTypesQueryHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, IStaffTypeAccessor staffTypes, IMediator mediator, ILogger<GetStaffTypesQueryHandler> logger, IEventStoreRepository eventStore, IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _staffTypes = staffTypes;
        _mediator = mediator;
        _logger = logger;
        _eventStore = eventStore;
        _mapper = mapper;
    }

    public async IAsyncEnumerable<StaffType> Handle(GetStaffTypesQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var staff = _staffTypes.GetAll(context ,cancellationToken);

        await RaiseStaffTypesQueriedEvent(request, cancellationToken).ConfigureAwait(false);

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

        await _eventStore.SaveAsync(_mapper.Map<EventReader>(e), cancellationToken)
            .ConfigureAwait(false);

        await _mediator.Publish(e, cancellationToken)
            .ConfigureAwait(false);
    }
}