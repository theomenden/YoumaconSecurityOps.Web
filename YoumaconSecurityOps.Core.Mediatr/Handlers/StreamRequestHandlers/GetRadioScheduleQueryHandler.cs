namespace YoumaconSecurityOps.Core.Mediatr.Handlers.StreamRequestHandlers;

internal sealed class GetRadioScheduleQueryHandler : IStreamRequestHandler<GetRadioSchedule, RadioScheduleReader>
{
    private readonly IRadioScheduleAccessor _radios;
    
    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetRadioScheduleQueryHandler(IRadioScheduleAccessor radios, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory)
    {
        _radios = radios;
        _dbContextFactory = dbContextFactory;
    }

    public async IAsyncEnumerable<RadioScheduleReader> Handle(GetRadioSchedule request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        await foreach (var radio in _radios.GetAllAsync(context, cancellationToken).ConfigureAwait(false))
        {
            yield return radio;
        }
    }
}