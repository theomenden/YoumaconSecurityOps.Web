using System;
using System.Collections.Generic;

namespace YSecOps.Data.EfCore.Models
{
    public partial class RadioSchedule
    {
        public Guid Id { get; set; }
        public Guid LastStaffToHave_Id { get; set; }
        public Guid Location_Id { get; set; }
        public string RadioNumber { get; set; }
        public DateTime? CheckedOutAt { get; set; }
        public DateTime? CheckedInAt { get; set; }
        public bool? IsCharging { get; set; }

        public virtual Staff LastStaffToHave { get; set; }
        public virtual Location Location { get; set; }
    }
}
