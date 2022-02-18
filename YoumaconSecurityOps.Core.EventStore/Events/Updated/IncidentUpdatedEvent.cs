namespace YoumaconSecurityOps.Core.EventStore.Events.Updated;

public class IncidentUpdatedEvent:EventBase
{
    public IncidentUpdatedEvent(IncidentReader incidentReader)
    {
        IncidentReader = incidentReader;
    }

    public IncidentReader IncidentReader { get; }

}