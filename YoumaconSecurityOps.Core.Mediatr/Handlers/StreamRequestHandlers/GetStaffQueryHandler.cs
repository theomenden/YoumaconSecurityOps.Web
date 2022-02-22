namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetStaffQueryHandler: IStreamRequestHandler<GetStaffQuery, StaffReader>
{
    private readonly IStaffAccessor _staff;

    private readonly IMediator _mediator;

    private readonly ILogger<GetStaffQueryHandler> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetStaffQueryHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, IStaffAccessor staff, IMediator mediator, ILogger<GetStaffQueryHandler> logger)
    {
        _dbContextFactory = dbContextFactory;
        _staff = staff;
        _mediator = mediator;
        _logger = logger;
    }

    public IAsyncEnumerable<StaffReader> Handle(GetStaffQuery request, CancellationToken cancellationToken)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var staff = _staff.GetAllAsync(context,cancellationToken);

        RaiseStaffListQueriedEvent(request, cancellationToken);

        return staff;
    }

    private Task RaiseStaffListQueriedEvent(GetStaffQuery query, CancellationToken cancellationToken)
    {
        var e = new StaffListQueriedEvent(null)
        {
            Aggregate = nameof(GetStaffQuery),
            MajorVersion = 1,
            Name = nameof(StaffListQueriedEvent)
        };

        return _mediator.Publish(e, cancellationToken);
    }
}