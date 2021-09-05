using System;

namespace YoumaconSecurityOps.Core.Mediatr.Commands
{
    public record AddShiftCommand(DateTime StartAt, DateTime EndAt, Guid StaffMemberId, String StaffMemberName,
        Guid StartingLocationId) : ICommand<Guid>
    {
        public Guid Id => Guid.NewGuid();
    }
}
