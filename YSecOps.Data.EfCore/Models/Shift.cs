using System;
using System.Collections.Generic;

namespace YSecOps.Data.EfCore.Models
{
    public partial class Shift
    {
        public Shift()
        {
            Incidents = new HashSet<Incident>();
        }

        public Guid Id { get; set; }
        public Guid StaffId { get; set; }
        public DateTime EndAt { get; set; }
        public DateTime StartAt { get; set; }
        public Guid StartingLocationId { get; set; }
        public Guid CurrentLocationId { get; set; }
        public DateTime? CheckedInAt { get; set; }
        public DateTime? CheckedOutAt { get; set; }
        public DateTime? LastReportedAt { get; set; }
        public string Notes { get; set; }

        public virtual Location CurrentLocation { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual Location StartingLocation { get; set; }
        public virtual ICollection<Incident> Incidents { get; set; }
    }
}
