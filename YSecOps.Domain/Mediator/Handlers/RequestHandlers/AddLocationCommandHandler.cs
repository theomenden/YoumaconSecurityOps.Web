using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using YsecOps.Core.Mediator.Requests.Commands;
using YSecOps.Events.EfCore.Contexts;

namespace YsecOps.Core.Mediator.Handlers.RequestHandlers;
internal sealed class AddLocationCommandHandler : AsyncRequestHandler<AddLocationCommand>
{
    private readonly IDbContextFactory<YSecOpsEventStoreContext> _dbContextFactory;

    public AddLocationCommandHandler(IDbContextFactory<YSecOpsEventStoreContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task Handle(AddLocationCommand request, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var locationAddedEvent = request.RaiseCommandEvent();

        locationAddedEvent.Data = JsonSerializer.Serialize(request);

        await context.Events.AddAsync(locationAddedEvent, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }
}