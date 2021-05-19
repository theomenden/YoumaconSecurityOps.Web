using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    public class ShiftReader: BaseReader
    {
        public ShiftReader()
        :base()
        {
            
        }
        
        public Guid StaffId { get; set; }

        public DateTime SysStart { get; set; }

        public DateTime SysEnd { get; set; }

        public DateTime EndAt { get; set; }

        public DateTime StartAt { get; set; }

        public Guid StartingLocationId { get; set; }

        public Guid CurrentLocationId { get; set; }

        public DateTime? CheckedInAt { get; set; }

        public DateTime? CheckedOutAt { get; set; }

        public DateTime? LastReportedAt { get; set; }

        public string Notes { get; set; }

        #region NavigationProperties
        public virtual LocationReader CurrentLocation { get; set; }

        public virtual StaffReader StaffMember { get; set; }
        
        public virtual LocationReader StartingLocationNavigation { get; set; }

        public virtual IEnumerable<IncidentReader> Incidents { get; set; }
        #endregion

        #region WorkingShiftInformation
        [NotMapped]
        public Boolean IsLate => CheckIfMemberWasLate();

        [NotMapped]
        public TimeSpan WorkedTimeSoFar => WorkedShiftDuration();
        #endregion

        #region PrivateFunctions

        private Boolean CheckIfMemberWasLate()
        {
            var isLate = false;

            if (CheckedInAt.HasValue)
            {
                isLate = CheckedInAt.Value >= StartAt.AddMinutes(5);
            }

            return isLate;
        }

        private TimeSpan TotalShiftDuration()
        {
            return EndAt - StartAt;
        }

        private TimeSpan WorkedShiftDuration()
        {
            var currentTime = DateTime.Now;

            return currentTime - CheckedInAt.GetValueOrDefault(currentTime);
        }

        #endregion
    }
}
