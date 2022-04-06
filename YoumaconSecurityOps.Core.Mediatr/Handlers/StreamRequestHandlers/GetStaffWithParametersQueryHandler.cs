namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetStaffWithParametersQueryHandler : IStreamRequestHandler<GetStaffWithParametersQuery, StaffReader>
{
    private readonly IStaffAccessor _staff;
    
    private readonly ILogger<GetStaffWithParametersQueryHandler> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetStaffWithParametersQueryHandler(IStaffAccessor staff, ILogger<GetStaffWithParametersQueryHandler> logger, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory)
    {
        _staff = staff;
        _logger = logger;
        _dbContextFactory = dbContextFactory;
    }

    public async IAsyncEnumerable<StaffReader> Handle(GetStaffWithParametersQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);
        

        await foreach (var member in _staff.GetAllThatMatchAsync(context, st => StaffFilterBuilder(st, request.Parameters), cancellationToken)
                           .WithCancellation(cancellationToken)
                           .ConfigureAwait(false))
        {
            yield return member;
        }
    }

    private Boolean StaffFilterBuilder(StaffReader staff, StaffQueryStringParameters parameters)
    {
        return staff.IsRaveApproved == parameters.IsRaveApproved
               && staff.IsBlackShirt == parameters.IsBlackShirt
               && staff.IsOnBreak == parameters.IsOnBreak
               && staff.NeedsCrashSpace == parameters.NeedsCrashSpace;
    }
}