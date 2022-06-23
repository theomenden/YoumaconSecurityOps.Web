using YSecOps.Events.EfCore.Models;

namespace YsecOps.Core.Mediator.Requests.Commands;

public interface ICommand: IRequest
{
    public Guid Id { get;  }

    public DateTime CreatedAt { get;  }

    public Event RaiseCommandEvent();
}

public interface ICommandWithResponse<out T>: IRequest<T>
{
    public Guid Id { get; }

    public DateTime CreatedAt { get; }

    public Event RaiseCommandEvent();
}
