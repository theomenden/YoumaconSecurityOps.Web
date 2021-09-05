using System;
using YoumaconSecurityOps.Core.Shared.Models.Writers;

namespace YoumaconSecurityOps.Core.Mediatr.Commands
{
    public record AddFullStaffEntryCommand(StaffWriter StaffWriter, ContactWriter ContactWriter) : ICommand<Guid>
    {
        public Guid Id => Guid.NewGuid();
    }
}
