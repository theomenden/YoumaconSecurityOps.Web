using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    public class StaffRole
    {
        public StaffRole()
        {
            Staff = new HashSet<StaffReader>(30);

            StaffTypesRoles = new HashSet<StaffTypesRoles>(30);
        }

        public int Id { get; set; }

        public string Role { get; }


        public virtual IEnumerable<StaffReader> Staff { get; set; }
        public virtual IEnumerable<StaffTypesRoles> StaffTypesRoles { get; set; }
    }
}
