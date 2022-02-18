namespace YoumaconSecurityOps.Core.EventStore.Events.Added;

public class IncidentAddedEvent : EventBase
{
    public IncidentAddedEvent(IncidentReader incident)
    {
        Incident = incident ?? throw new ArgumentNullException(nameof(incident));
    }

    public IncidentReader Incident { get; }
}