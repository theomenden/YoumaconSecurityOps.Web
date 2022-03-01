namespace YoumaconSecurityOps.Core.Mediatr.Commands;

/// <summary>
/// The basis for all commands in the application
/// </summary>
public interface ICommand: IRequest
{
    /// <value>The unique identifier for each command that we source an event from</value>
    public Guid Id { get; }
}

/// <summary>
/// The basis for all commands in the application that return a <see cref="IRequest{T}"/>
/// </summary>
/// <typeparam name="T">The type we return</typeparam>
public interface ICommandWithReturn<out T> : IRequest<T>
{
    /// <value>The unique identifier for each command that we source an event from</value>
    public Guid Id { get; }
}