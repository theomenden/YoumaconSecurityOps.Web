namespace YoumaconSecurityOps.Core.Mediatr.Handlers.NotificationHandlers;

internal sealed class ContactListUpdatedEventHandler: INotificationHandler<ContactListUpdatedEvent>
{
    public Task Handle(ContactListUpdatedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}