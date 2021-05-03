using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Mediatr.Commands
{
    public record AddLocationCommand(string Name, bool IsHotel) : ICommand
    {
        public Guid Id => Guid.NewGuid();
    }
}
