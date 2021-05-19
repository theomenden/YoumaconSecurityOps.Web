using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    public class IncidentReader: BaseReader
    {
            public Guid RecordedById { get; set; }
            public Guid ReportedById { get; set; }
            public Guid ShiftId { get; set; }
            public Guid LocationId { get; set; }
            public int Severity { get; set; }
            public DateTime RecordedOn { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? ResolvedAt { get; set; }

            public virtual LocationReader Location { get; set; }
            public virtual StaffReader RecordedBy { get; set; }
            public virtual StaffReader ReportedBy { get; set; }
            public virtual ShiftReader Shift { get; set; }
    }
}
