using System;
using System.Collections.Generic;

namespace YSecOps.Data.EfCore.Models
{
    public partial class Location
    {
        public Location()
        {
            Incidents = new HashSet<Incident>();
            RadioSchedules = new HashSet<RadioSchedule>();
            ShiftCurrentLocations = new HashSet<Shift>();
            ShiftStartingLocations = new HashSet<Shift>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsHotel { get; set; }

        public virtual ICollection<Incident> Incidents { get; set; }
        public virtual ICollection<RadioSchedule> RadioSchedules { get; set; }
        public virtual ICollection<Shift> ShiftCurrentLocations { get; set; }
        public virtual ICollection<Shift> ShiftStartingLocations { get; set; }
    }
}
