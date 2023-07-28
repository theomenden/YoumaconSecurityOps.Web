
namespace YsecOps.Core.Mediator.Handlers.QueryHandlers;
internal sealed class GetRadioScheduleAggregatesHandler: IRequestHandler<GetTotalRadiosQuery, Int32>,
    IRequestHandler<GetCheckedOutRadiosCountQuery, Int32>,
    IRequestHandler<GetChargingRadiosCountQuery, Int32>
{
    private readonly IDbContextFactory<YoumaconSecurityOpsContext> _dbContextFactory;

    public GetRadioScheduleAggregatesHandler(IDbContextFactory<YoumaconSecurityOpsContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<int> Handle(GetTotalRadiosQuery request, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var totalRadiosCount = await context.RadioSchedules.CountAsync(cancellationToken);

        return totalRadiosCount;
    }

    public async Task<int> Handle(GetCheckedOutRadiosCountQuery request, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        var checkedOutRadioCount = await context.RadioSchedules.CountAsync(radio => radio.CheckedOutAt.HasValue,cancellationToken);

        return checkedOutRadioCount;
    }

    public async Task<int> Handle(GetChargingRadiosCountQuery request, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var chargingRadiosCount = await context.RadioSchedules.CountAsync(radio => radio.IsCharging ?? false,cancellationToken);

        return chargingRadiosCount;
    }
}
