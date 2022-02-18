namespace YoumaconSecurityOps.Core.Mediatr.Handlers.FailureHandlers;

internal sealed class FailedToQueryEventHandler: INotificationHandler<FailedToQueryEvent>
{
    public Task Handle(FailedToQueryEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}