using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    public class RadioScheduleReader: BaseReader
    {
        public DateTime SysStart { get; set; }
        public DateTime SysEnd { get; set; }
        public Guid LastStaffToHaveId { get; set; }
        public Guid LocationId { get; set; }
        public int RadioNumber { get; set; }

        public virtual StaffReader LastStaffToHave { get; set; }
        public virtual LocationReader Location { get; set; }
    }
}
