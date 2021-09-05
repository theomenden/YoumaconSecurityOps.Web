using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Mediatr.Commands
{
    public record UpdateStaffInfoCommand() : ICommand<Guid>
    {
        public Guid Id => Guid.NewGuid();
    }
}
