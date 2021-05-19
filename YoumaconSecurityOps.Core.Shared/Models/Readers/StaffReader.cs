using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    public class StaffReader: BaseReader
    {
        #region BaseProperties

        public Guid ContactId { get; set; }
        
        public int RoleId { get; set; }
        
        public int StaffTypeId { get; set; }
        
        public bool NeedsCrashSpace { get; set; }

        public bool IsBlackShirt { get; set; }

        [NotMapped]
        public bool IsOnBreak  => BreakStartAt.HasValue && !BreakEndAt.HasValue;

        public DateTime? BreakStartAt { get; set; }

        public DateTime? BreakEndAt { get; set; }

        [NotMapped]
        public TimeSpan Duration => BreakEndAt.GetValueOrDefault(DateTime.Now)  - BreakStartAt.GetValueOrDefault(DateTime.Now);

        public bool IsRaveApproved { get; set; }

        public string ShirtSize { get; set; }
        #endregion

        #region NavigationProperties

        public virtual ContactReader ContactInformation { get; set; }

        public virtual StaffRole Role { get; set; }

        public virtual StaffType StaffType { get; set; }

        public virtual IEnumerable<IncidentReader> IncidentsRecordedBy { get; set; }

        public virtual IEnumerable<IncidentReader> IncidentsReportedBy { get; set; }

        public virtual IEnumerable<RadioScheduleReader> RadioSchedule { get; set; }

        public virtual IEnumerable<RoomScheduleReader> RoomSchedule { get; set; }

        public virtual IEnumerable<ShiftReader> Shifts { get; set; }

        public virtual IEnumerable<StaffTypesRoles> StaffTypesRoles { get; set; }

        #endregion
    }
}
