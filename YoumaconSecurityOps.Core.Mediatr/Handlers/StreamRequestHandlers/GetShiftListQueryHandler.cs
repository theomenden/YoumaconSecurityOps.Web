
namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetShiftListQueryHandler: IStreamRequestHandler<GetShiftListQuery, ShiftReader>,
    IStreamRequestHandler<GetShiftListWithParametersQuery, ShiftReader>
{
    private readonly IShiftAccessor _shifts;
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetShiftListQueryHandler(IShiftAccessor shifts, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory)
    {
        _shifts = shifts;
        _dbContextFactory = dbContextFactory;
    }

    public async IAsyncEnumerable<ShiftReader> Handle(GetShiftListQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        await foreach (var shift in _shifts.GetAllAsync(context, cancellationToken).ConfigureAwait(false))
        {
            yield return shift;
        }
    }

    public async IAsyncEnumerable<ShiftReader> Handle(GetShiftListWithParametersQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        await foreach (var shift in _shifts.GetAllThatMatchAsync(context,
                           sh => request.Parameters.StaffIds.Contains(sh.StaffMember.Id), cancellationToken))
        {
            yield return shift;
        }
    }
}