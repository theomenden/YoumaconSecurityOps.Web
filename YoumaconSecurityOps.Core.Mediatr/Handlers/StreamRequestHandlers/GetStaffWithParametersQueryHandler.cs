using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

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

    public async IAsyncEnumerable<StaffReader> Handle(GetStaffWithParametersQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);
        

        await foreach (var member in _staff.GetAllThatMatchAsync(context, st => StaffFilterBuilder(st, request.Parameters), cancellationToken)
                           .WithCancellation(cancellationToken)
                           .ConfigureAwait(false))
        {
            yield return member;
        }

        await RaiseStaffListQueriedEvent(request.Parameters, cancellationToken);
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

    private Boolean StaffFilterBuilder(StaffReader staff, StaffQueryStringParameters parameters)
    {
        return staff.IsRaveApproved == parameters.IsRaveApproved
               && staff.IsBlackShirt == parameters.IsBlackShirt
               && staff.IsOnBreak == parameters.IsOnBreak
               && staff.NeedsCrashSpace == parameters.NeedsCrashSpace;
    }
}