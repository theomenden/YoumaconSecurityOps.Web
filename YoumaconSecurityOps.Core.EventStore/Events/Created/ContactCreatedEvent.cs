namespace YoumaconSecurityOps.Core.EventStore.Events.Created;

public class ContactCreatedEvent : EventBase
{
    public ContactCreatedEvent(ContactWriter contactWriter)
        :base()
    {
        ContactWriter = contactWriter;
    }

    public ContactWriter ContactWriter { get; }
}