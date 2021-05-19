using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    public class StaffTypesRoles
    {
            public Guid Id { get; set; }
            public Guid StaffId { get; set; }
            public int StaffType { get; set; }
            public int RoleId { get; set; }

            public virtual StaffRole Role { get; set; }
            public virtual StaffReader Staff { get; set; }
            public virtual StaffType StaffTypeNavigation { get; set; }
    }
}
