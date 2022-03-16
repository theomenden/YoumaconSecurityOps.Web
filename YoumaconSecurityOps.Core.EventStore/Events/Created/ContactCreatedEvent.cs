namespace YoumaconSecurityOps.Core.EventStore.Events.Created;

public class ContactCreatedEvent : EventBase
{
    public ContactCreatedEvent(ContactWriter contactWriter)
        :base()
    {
        ContactWriter = contactWriter;
        DataAsJson = contactWriter.ToJson();
        Aggregate = $"{contactWriter.Id}-{contactWriter.LastName}";
        AggregateId = contactWriter.Id;
        MajorVersion = 1;
        MinorVersion = 1;
    }

    public ContactWriter ContactWriter { get; }
}