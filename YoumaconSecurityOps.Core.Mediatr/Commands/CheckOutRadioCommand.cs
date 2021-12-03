using System;

namespace YoumaconSecurityOps.Core.Mediatr.Commands
{
    public record CheckOutRadioCommand(Guid RadioId, Guid StaffCheckedOutToId, Guid LocationCheckedOutFromId) : ICommand<Guid>
    {
        public Guid Id => Guid.NewGuid();
    }
}
