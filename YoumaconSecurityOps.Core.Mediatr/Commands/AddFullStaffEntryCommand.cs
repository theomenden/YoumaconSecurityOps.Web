using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Writers;

namespace YoumaconSecurityOps.Core.Mediatr.Commands
{
    public record AddFullStaffEntryCommand(StaffWriter StaffWriter, ContactWriter ContactWriter) : ICommand
    {
        public Guid Id => Guid.NewGuid();
    }
}
