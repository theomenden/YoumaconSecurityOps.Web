using System;
using System.Collections.Generic;

namespace YSecOps.Data.EfCore.Models
{
    public partial class Staff
    {
        public Staff()
        {
            Contacts = new HashSet<Contact>();
            IncidentOpsManagers = new HashSet<Incident>();
            IncidentRecordedBies = new HashSet<Incident>();
            RadioSchedules = new HashSet<RadioSchedule>();
            Shifts = new HashSet<Shift>();
            StaffTypesRoles = new HashSet<StaffTypesRole>();
        }

        public Guid Id { get; set; }
        public bool IsOnBreak { get; set; }
        public bool NeedsCrashSpace { get; set; }
        public bool IsBlackShirt { get; set; }
        public bool IsRaveApproved { get; set; }
        public DateTime? BreakStartAt { get; set; }
        public DateTime? BreakEndAt { get; set; }
        public string ShirtSize { get; set; }
        public Guid? IncidentId { get; set; }
        public Guid? RoomId { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Incident> IncidentOpsManagers { get; set; }
        public virtual ICollection<Incident> IncidentRecordedBies { get; set; }
        public virtual ICollection<RadioSchedule> RadioSchedules { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
        public virtual ICollection<StaffTypesRole> StaffTypesRoles { get; set; }
    }
}
