namespace YoumaconSecurityOps.Core.EventStore.Events.Updated;

public class ContactListUpdatedEvent : EventBase
{
    public ContactListUpdatedEvent(ContactReader contactReader)
    {
        ContactReader = contactReader;
    }

    public ContactReader ContactReader { get; }
}