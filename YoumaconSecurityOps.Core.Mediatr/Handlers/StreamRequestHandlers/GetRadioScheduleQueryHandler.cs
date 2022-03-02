namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class GetRadioScheduleQueryHandler : IStreamRequestHandler<GetRadioSchedule, RadioScheduleReader>
{
    private readonly IRadioScheduleAccessor _radios;
    
    private readonly ILogger<GetRadioScheduleQueryHandler> _logger;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public GetRadioScheduleQueryHandler(IRadioScheduleAccessor radios, ILogger<GetRadioScheduleQueryHandler> logger, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory)
    {
        _radios = radios;
        _logger = logger;
        _dbContextFactory = dbContextFactory;   
    }

    public IAsyncEnumerable<RadioScheduleReader> Handle(GetRadioSchedule request, CancellationToken cancellationToken)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var radios = _radios.GetAllAsync(context, cancellationToken);
        
        return radios;
    }
}