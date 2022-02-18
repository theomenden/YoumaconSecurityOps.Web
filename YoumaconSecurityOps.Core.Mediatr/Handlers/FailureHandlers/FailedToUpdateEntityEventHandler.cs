namespace YoumaconSecurityOps.Core.Mediatr.Handlers.FailureHandlers;

internal sealed class FailedToUpdateEntityEventHandler: INotificationHandler<FailedToUpdateEntityEvent>
{
    public Task Handle(FailedToUpdateEntityEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}