namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetStaffWithParametersQueryHandler : IStreamRequestHandler<GetStaffWithParametersQuery, StaffReader>
{
    private readonly IStaffAccessor _staff;

    private readonly IMediator _mediator;

    private readonly ILogger<GetStaffWithParametersQueryHandler> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetStaffWithParametersQueryHandler(IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory, IStaffAccessor staff, IMediator mediator, ILogger<GetStaffWithParametersQueryHandler> logger)
    {
        _dbContextFactory = dbContextFactory;
        _staff = staff;
        _mediator = mediator;
        _logger = logger;
    }

    private static IAsyncEnumerable<StaffReader> Filter(StaffQueryStringParameters parameters, IAsyncEnumerable<StaffReader> staffList)
    {
        return staffList
            .Where(s => s.IsBlackShirt == parameters.IsBlackShirt)
            .Where(s => s.IsRaveApproved == parameters.IsRaveApproved)
            .Where(s => s.NeedsCrashSpace == parameters.NeedsCrashSpace)
            .Where(s => s.IsOnBreak == parameters.IsOnBreak)
            .Where(s => s.Role?.Id == parameters.RoleId)
            .Where(s => s.StaffType?.Id == parameters.TypeId);
    }

    private Task RaiseStaffListQueriedEvent(StaffQueryStringParameters parameters, CancellationToken cancellationToken)
    {
        var e = new StaffListQueriedEvent(parameters)
        {
            Aggregate = nameof(StaffQueryStringParameters),
            MajorVersion = 1,
            MinorVersion = 1,
            Name = nameof(StaffListQueriedEvent)
        };

        return _mediator.Publish(e, cancellationToken);
    }

    public IAsyncEnumerable<StaffReader> Handle(GetStaffWithParametersQuery request, CancellationToken cancellationToken)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var staff = _staff.GetAllAsync(context, cancellationToken);

        staff = Filter(request.Parameters, staff);

        RaiseStaffListQueriedEvent(request.Parameters, cancellationToken);

        return staff;
    }
}