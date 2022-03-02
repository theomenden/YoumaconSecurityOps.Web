namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetStaffTypesQueryHandler : IStreamRequestHandler<GetStaffTypesQuery, StaffType>
{
    private readonly IStaffTypeAccessor _staffTypes;

    private readonly IEventStoreRepository _eventStore;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    private readonly ILogger<GetStaffTypesQueryHandler> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetStaffTypesQueryHandler(IStaffTypeAccessor staffTypes, IEventStoreRepository eventStore, IMapper mapper, IMediator mediator, ILogger<GetStaffTypesQueryHandler> logger, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory)
    {
        _staffTypes = staffTypes;
        _eventStore = eventStore;
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
        _dbContextFactory = dbContextFactory;
    }

    public async IAsyncEnumerable<StaffType> Handle(GetStaffTypesQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var staff = _staffTypes.GetAll(context ,cancellationToken);
        
        await foreach (var member in staff.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            yield return member;
        }
    }
}