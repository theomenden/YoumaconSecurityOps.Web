namespace YoumaconSecurityOps.Core.EventStore.Events;

public abstract record EventRecordBase(Guid AggregateId) : IEvent
{
    protected EventRecordBase()
        :this(Guid.Empty)
    {
        Id = Guid.NewGuid();
    }

    public Guid Id {get;}
}