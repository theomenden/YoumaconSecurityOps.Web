using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.Mediatr.Commands
{
    public record AddShiftCommand(DateTime StartAt, DateTime EndAt, StaffReader StaffMember,
        LocationReader StartingLocation) : ICommand
    {
        public Guid Id => Guid.NewGuid();
    }
}
