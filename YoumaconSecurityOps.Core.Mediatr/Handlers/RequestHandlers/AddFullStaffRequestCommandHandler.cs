namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal sealed class AddFullStaffRequestCommandHandler : IRequestHandler<AddFullStaffEntryCommandWithReturn, Guid>
{
    private readonly IMediator _mediator;

    private readonly ILogger<AddFullStaffRequestCommandHandler> _logger;

    private readonly IDbContextFactory<EventStoreDbContext> _dbContextFactory;

    private readonly IEventStoreRepository _eventStore;

    private readonly IMapper _mapper;

    public AddFullStaffRequestCommandHandler(IMediator mediator, ILogger<AddFullStaffRequestCommandHandler> logger, IDbContextFactory<EventStoreDbContext> dbContextFactory, IEventStoreRepository eventStore, IMapper mapper)
    {
        _mediator = mediator;
        _logger = logger;
        _dbContextFactory = dbContextFactory;
        _eventStore = eventStore;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(AddFullStaffEntryCommandWithReturn request, CancellationToken cancellationToken)
    {
        try
        {
            await RaiseStaffCreatedEvent(request.FullStaffWriter, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("Could not handle request {@request}: exception {@ex}", request, ex);
            throw;
        }

        return request.FullStaffWriter.Id;
    }

    private Task RaiseStaffCreatedEvent(FullStaffWriter fullStaffWriter, CancellationToken cancellationToken)
    {
        var e = new StaffCreatedEvent(fullStaffWriter)
        {
            Name = nameof(AddFullStaffRequestCommandHandler)
        };

        using var context = _dbContextFactory.CreateDbContext();

        _eventStore.ApplyInitialEventAsync(context, _mapper.Map<EventReader>(e), cancellationToken);

        _mediator.Publish(e, cancellationToken);

        return Task.CompletedTask;
    }
}