namespace YoumaconSecurityOps.Core.Mediatr.Handlers.FailureHandlers;

internal sealed class FailedToAddEntityEventHandler : INotificationHandler<FailedToAddEntityEvent>
{
    public Task Handle(FailedToAddEntityEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}