namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetStaffRolesQueryHandler : IStreamRequestHandler<GetStaffRolesQuery, StaffRole>
{
    private readonly IStaffRoleAccessor _staffRoles;

    private readonly IEventStoreRepository _eventStore;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly ILogger<GetStaffRolesQueryHandler> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetStaffRolesQueryHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory,IStaffRoleAccessor staffRoles, IMediator mediator, ILogger<GetStaffRolesQueryHandler> logger, IEventStoreRepository eventStore, IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _staffRoles = staffRoles;
        _mediator = mediator;
        _logger = logger;
        _eventStore = eventStore;
        _mapper = mapper;
    }

    public async IAsyncEnumerable<StaffRole> Handle(GetStaffRolesQuery request, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken)
            .ConfigureAwait(false);

        await RaiseStaffRolesQueriedEvent(request, cancellationToken)
            .ConfigureAwait(false);

        await foreach (var role in _staffRoles.GetAll(context, cancellationToken).ConfigureAwait(false))
        {
            yield return role;
        }
    }
    private async Task RaiseStaffRolesQueriedEvent(GetStaffRolesQuery staffRoleQueryRequest, CancellationToken cancellationToken)
    {
        var e = new StaffRolesQueriedEvent(null)
        {
            Aggregate = nameof(GetStaffRolesQuery),
            MajorVersion = 1,
            Name = nameof(staffRoleQueryRequest)
        };

        _logger.LogInformation("Logged event of type {StaffRolesQueriedEvent} {e}, {request}, {StaffRolesQueriedEvent}", typeof(StaffRolesQueriedEvent), e, staffRoleQueryRequest, nameof(RaiseStaffRolesQueriedEvent));

        await _eventStore.SaveAsync(_mapper.Map<EventReader>(e), cancellationToken);

        await _mediator.Publish(e, cancellationToken);
    }
}