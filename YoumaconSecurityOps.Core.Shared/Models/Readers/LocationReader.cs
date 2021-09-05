using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    [Table("Locations")]
    public partial class LocationReader: BaseReader
    {
        public LocationReader()
        {
        }

        /// <value>
        /// The Location's Name
        /// </value>
        public string Name { get; set; }

        /// <value>
        /// Flag that determines if the location is also a hotel
        /// </value>
        public bool IsHotel { get; set; }

        #region NavigationProperties
        [InverseProperty(nameof(IncidentReader.Location))]
        public virtual ICollection<IncidentReader> Incidents { get; set; } = new HashSet<IncidentReader>();

        [InverseProperty(nameof(RadioScheduleReader.Location))]
        public virtual ICollection<RadioScheduleReader> RadioSchedule { get; set; } = new HashSet<RadioScheduleReader>();
        
        public virtual ICollection<ShiftReader> ShiftCurrentLocation { get; set; } = new HashSet<ShiftReader>();

        [InverseProperty(nameof(ShiftReader.StartingLocationNavigation))]
        public virtual ICollection<ShiftReader> ShiftStartingLocationNavigation { get; set; } = new HashSet<ShiftReader>();

        #endregion
    }
}
