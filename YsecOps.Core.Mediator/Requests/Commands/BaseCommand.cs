using YSecOps.Events.EfCore.Models;

namespace YsecOps.Core.Mediator.Requests.Commands;
public abstract record BaseCommand : ICommand
{
    public Guid Id { get; } = Guid.NewGuid();

    public DateTime CreatedAt { get; } = DateTime.Now;

    public virtual Event RaiseCommandEvent()
    {
        return new()
        {
            AggregateId = Id,
            CreatedAt = DateTime.Now,
            Major_Version = 0,
            Minor_Version = 1
        };
    }
}
