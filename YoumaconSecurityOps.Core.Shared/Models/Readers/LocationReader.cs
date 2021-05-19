using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    public class LocationReader: BaseReader
    {
        public LocationReader()
        {
        }

        public string Name { get; set; }

        public bool IsHotel { get; set; }

        #region NavigationProperties
        public virtual IEnumerable<IncidentReader> Incidents { get; set; }

        public virtual IEnumerable<RadioScheduleReader> RadioSchedule { get; set; }

        public virtual IEnumerable<ShiftReader> ShiftsCurrentLocation { get; set; }

        public virtual IEnumerable<ShiftReader> ShiftsStartingLocationNavigation { get; set; }
        #endregion
    }
}
