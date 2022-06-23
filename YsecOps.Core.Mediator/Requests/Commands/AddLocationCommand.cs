using YSecOps.Events.EfCore.Models;

namespace YsecOps.Core.Mediator.Requests.Commands
{
    public record AddLocationCommand(String LocationName, Boolean IsHotel = false): BaseCommand
    {
        public override Event RaiseCommandEvent()
        {
            return new()
            {
                Aggregate = nameof(AddLocationCommand),
                AggregateId = Id,
                CreatedAt = DateTime.Now,
                Major_Version = 0,
                Minor_Version = 1,
                Name = nameof(AddLocationCommand)
            };
        }
    }
}
