using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    public class StaffReader: BaseReader
    {
        public Guid ContactId { get; set; }

        public virtual ContactReader ContactInformation { get; set; }

        public int RoleId { get; set; }

        public virtual StaffRole Role{ get; set; }

        public int StaffTypeId { get; set; }

        public virtual StaffType StaffType { get; set; }

        public bool NeedsCrashSpace { get; set; }

        public bool IsBlackShirt { get; set; }

        public bool IsOnBreak { get; set; } = false;

        public DateTime? BreakStartAt { get; set; }

        public DateTime? BreakEndAt { get; set; }

        public TimeSpan Duration => BreakEndAt.GetValueOrDefault(DateTime.Now)  - BreakStartAt.GetValueOrDefault(DateTime.Now);

        public bool IsRaveApproved { get; set; }

        public string ShirtSize { get; set; }


    }
}
