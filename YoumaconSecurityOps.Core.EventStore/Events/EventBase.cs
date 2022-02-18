namespace YoumaconSecurityOps.Core.EventStore.Events;

/// <summary>
/// <para>Provides a basis for all derived events in the Ysec Event Store</para>
/// <inheritdoc cref="IEquatable{T}"/> <inheritdoc cref="IComparable{T}"/>
/// </summary>
public abstract class EventBase: IEvent, IEquatable<EventBase>, IComparable<EventBase>
{
    protected EventBase()
    {
        Id = Guid.NewGuid();

        CreatedAt = DateTime.Now;
    }

    /// <value>
    /// Unique Id for the event
    /// </value>
    public Guid Id { get; }

    /// <value>
    /// Id for the Aggregate that is the basis for this event
    /// </value>
    /// <remarks>Set only during event creation</remarks>
    public Guid AggregateId { get; init; }

    /// <value>
    /// Date and Time this event was created
    /// </value>
    /// <remarks>Read Only Field</remarks>
    public DateTime CreatedAt { get; }

    /// <value>
    /// The major version of this event
    /// </value>
    public Int32 MajorVersion { get; set; }

    /// <value>
    /// The minor version tracks any mutations that occur under this particular event
    /// </value>
    public Int32 MinorVersion { get; set; }

    /// <value>
    /// The name of the aggregate that raised this event
    /// </value>
    public String Aggregate { get; set; }

    /// <value>
    /// The name of the event
    /// </value>
    public String Name { get; set; }

    /// <value>
    /// The data produced as a result of this event being raised
    /// </value>
    public String DataAsJson { get; set; }

    public bool Equals(EventBase other)
    {
        if (other is null)
        {
            return false;
        }

        return Id == other.Id &&
               MajorVersion == other.MajorVersion &&
               MinorVersion == other.MinorVersion;
    }

    public int CompareTo(EventBase other)
    {
        if (other is null)
        {
            return 1;
        }

        if (Equals(other))
        {
            return 0;
        }

        return MajorVersion == other.MajorVersion ? MinorVersion.CompareTo(other.MinorVersion) : MajorVersion.CompareTo(other.MajorVersion);
    }

    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj is EventBase other && Equals(other);
    }

    public override int GetHashCode()
    {
        return 53 * HashCode.Combine(Id, CreatedAt);
    }


}