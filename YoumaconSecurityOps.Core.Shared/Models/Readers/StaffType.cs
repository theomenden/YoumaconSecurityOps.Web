using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    public class StaffType
    {
        public StaffType()
        {
        }

        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }

        public virtual IEnumerable<StaffReader> Staff { get; set; }

        public virtual IEnumerable<StaffTypesRoles> StaffTypesRoles { get; set; }
    }
}
