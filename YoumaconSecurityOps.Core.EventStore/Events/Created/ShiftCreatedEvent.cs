namespace YoumaconSecurityOps.Core.EventStore.Events.Created;

/// <summary>
/// An event that reflects the creation of a new shift
/// </summary>
/// <remarks>Holds an instance of the created <see cref="ShiftWriter"/></remarks>
public class ShiftCreatedEvent: EventBase
{
    public ShiftCreatedEvent(ShiftWriter shiftWriter)
    {
        ShiftWriter = shiftWriter;
        DataAsJson = shiftWriter.ToJson();
        Aggregate = $"{shiftWriter.Id}-{shiftWriter.StaffMemberName}";
        AggregateId = shiftWriter.Id;
        MajorVersion = 1;
        MinorVersion = 1;
    }

    /// <value>
    /// A prototype shift object
    /// </value>
    public ShiftWriter ShiftWriter { get; }

}