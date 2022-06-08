namespace YoumaconSecurityOps.Core.EventStore.Events;

/// <summary>
/// Basic interface container for linking <c>Mediatr</c> with our EventStore/Source pattern
/// </summary>
/// <remarks>Implements Mediatr's <see cref="INotification"/></remarks>
public interface IEvent: INotification
{
    Guid Id { get; }
}

public interface IEvent<out T> : INotification
    where T : class
{
    Guid Id { get; }

    T Data { get; }

    /// <value>
    /// Id for the Aggregate that is the basis for this event
    /// </value>
    /// <remarks>Set only during event creation</remarks>
    public Guid AggregateId { get; }

    /// <value>
    /// Date and Time this event was created
    /// </value>
    /// <remarks>Read Only Field</remarks>
    public DateTime CreatedAt { get; }

    /// <value>
    /// The major version of this event
    /// </value>
    public Int32 MajorVersion { get; }

    /// <value>
    /// The minor version tracks any mutations that occur under this particular event
    /// </value>
    public Int32 MinorVersion { get; }

    /// <value>
    /// The name of the aggregate that raised this event
    /// </value>
    public String Aggregate { get; }

    /// <value>
    /// The name of the event
    /// </value>
    public String Name { get; }
}