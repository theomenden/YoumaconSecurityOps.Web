using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Core.Mediatr.Commands
{
    public record ShiftReportInCommand(DateTime ReportInAt, LocationReader CurrentLocation) : ICommand
    {
        public Guid Id => Guid.NewGuid();
    }
}
