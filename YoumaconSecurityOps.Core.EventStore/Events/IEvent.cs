namespace YoumaconSecurityOps.Core.EventStore.Events;

/// <summary>
/// Basic interface container for linking <c>Mediatr</c> with our EventStore/Source pattern
/// </summary>
/// <remarks>Implements Mediatr's <see cref="INotification"/></remarks>
public interface IEvent: INotification
{
    Guid Id { get; }
}