using System;
using System.Collections.Generic;

namespace YSecOps.Data.EfCore.Models
{
    public partial class Role
    {
        public Role()
        {
            StaffTypesRoles = new HashSet<StaffTypesRole>();
        }

        public int Id { get; set; }
        public string Role1 { get; set; }

        public virtual ICollection<StaffTypesRole> StaffTypesRoles { get; set; }
    }
}
