using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    /// <summary>
    /// <para>A class that keeps track of Radios and their owners in the database</para>
    /// <see cref="BaseReader"/>
    /// </summary>
    [Table("RadioSchedules")]
    public partial class RadioScheduleReader: BaseReader
    {
        ///<value>
        /// Time Radio was checked in
        /// </value>
        public DateTime? CheckedInAt { get; set; }

        ///<value>
        /// Time Radio was checked out
        /// </value>
        public DateTime? CheckedOutAt { get; set; }

        ///<value>
        /// Flag indicating if radio is currently charging
        /// </value>
        public Boolean IsCharging { get; set; }

        /// <value>
        /// Tracking information for versioning
        /// </value>
        public DateTime SysStart { get; set; }

        /// <value>
        /// Tracking information for versioning
        /// </value>
        public DateTime SysEnd { get; set; }

        /// <value>
        /// The id of the last staff member to possess the radio
        /// </value>
        public Guid LastStaffToHaveId { get; set; }

        /// <value>
        /// The id of the location the radio was checked out from
        /// </value>
        public Guid LocationId { get; set; }

        /// <value>
        /// The physical ID for the radio that's being checked out
        /// </value>
        public String RadioNumber { get; set; }

        [ForeignKey(nameof(LastStaffToHaveId))]
        [InverseProperty(nameof(StaffReader.RadioSchedules))]
        public virtual StaffReader LastStaffToHave { get; set; } = default!;

        [ForeignKey(nameof(LocationId))]
        [InverseProperty(nameof(LocationReader.RadioSchedule))]
        public virtual LocationReader Location { get; set; } = default!;

        /// <value>
        /// Flag to check if the radio is currently checked in
        /// </value>
        [NotMapped] public Boolean IsCheckedIn => CheckedInAt.HasValue;
    }
}
