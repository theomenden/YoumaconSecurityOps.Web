using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    [Table("RadioSchedules")]
    public partial class RadioScheduleReader: BaseReader
    {
        public DateTime SysStart { get; set; }
        public DateTime SysEnd { get; set; }
        public Guid LastStaffToHave_Id { get; set; }
        public Guid Location_Id { get; set; }
        public int RadioNumber { get; set; }

        [ForeignKey(nameof(LastStaffToHave_Id))]
        [InverseProperty(nameof(StaffReader.RadioSchedules))]
        public virtual StaffReader LastStaffToHave { get; set; } = default!;

        [ForeignKey(nameof(Location_Id))]
        [InverseProperty(nameof(LocationReader.RadioSchedule))]
        public virtual LocationReader Location { get; set; } = default!;
    }
}
