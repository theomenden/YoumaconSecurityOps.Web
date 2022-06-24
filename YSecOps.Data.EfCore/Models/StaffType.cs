using System;
using System.Collections.Generic;

namespace YSecOps.Data.EfCore.Models
{
    public partial class StaffType
    {
        public StaffType()
        {
            StaffTypesRoles = new HashSet<StaffTypesRole>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<StaffTypesRole> StaffTypesRoles { get; set; }
    }
}
