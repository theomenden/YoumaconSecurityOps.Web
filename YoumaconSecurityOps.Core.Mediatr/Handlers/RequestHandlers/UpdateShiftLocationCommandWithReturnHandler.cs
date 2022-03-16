﻿using System.ComponentModel;

namespace YoumaconSecurityOps.Core.Mediatr.Handlers.RequestHandlers;

internal class UpdateShiftLocationCommandWithReturnHandler : IRequestHandler<UpdateShiftLocationCommandWithReturn, ShiftReader>
{
    private readonly IMediator _mediator;

    private readonly IShiftRepository _shifts;

    private readonly IDbContextFactory<YoumaconSecurityDbContext> _dbContextFactory;

    public UpdateShiftLocationCommandWithReturnHandler(IMediator mediator, IShiftRepository shifts, IDbContextFactory<YoumaconSecurityDbContext> dbContextFactory)
    {
        _mediator = mediator;
        _shifts = shifts;
        _dbContextFactory = dbContextFactory;
    }

    public async Task<ShiftReader> Handle(UpdateShiftLocationCommandWithReturn request, CancellationToken cancellationToken)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken).ConfigureAwait(false);

        var updateResult = await _shifts.UpdateCurrentLocation(context, request.ShiftId, request.LocationId, cancellationToken);

        await RaiseShiftUpdatedEvent(updateResult);

        return updateResult;
    }

    private async Task RaiseShiftUpdatedEvent(ShiftReader reader)
    {
        var e = new ShiftUpdatedEvent(reader);

        await _mediator.Publish(e);
    }
}