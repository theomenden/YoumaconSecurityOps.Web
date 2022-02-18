namespace YoumaconSecurityOps.Core.EventStore.Events.Added;

public class ContactAddedEvent : EventBase
{
    public ContactAddedEvent(ContactReader contactReader)
    {
        ContactReader = contactReader;
    }

    public ContactReader ContactReader { get; }
}