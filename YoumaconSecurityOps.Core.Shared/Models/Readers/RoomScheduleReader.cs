using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    public class RoomScheduleReader: BaseReader
    {
        public DateTime SysStart { get; set; }

        public DateTime SysEnd { get; set; }

        public Guid LastStaffInRoomId { get; set; }

        public bool IsCurrentlyOccupied { get; set; }

        public string RoomNumber { get; set; }

        public int NumberOfKeys { get; set; }

        public Guid LocationId { get; set; }


        public virtual StaffReader LastStaffInRoom { get; set; }
    }
}
