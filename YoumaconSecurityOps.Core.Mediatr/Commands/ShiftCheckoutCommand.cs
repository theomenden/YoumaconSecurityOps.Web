using System;

namespace YoumaconSecurityOps.Core.Mediatr.Commands
{
    public record ShiftCheckoutCommand(Guid ShiftId) : ICommand<Guid>
    {
        public Guid Id => Guid.NewGuid();
    }
}
