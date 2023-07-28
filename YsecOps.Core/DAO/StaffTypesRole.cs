using System;
using System.Collections.Generic;

namespace YsecOps.Core.Models.DAO;

public partial class StaffTypesRole
{
    public Guid Id { get; set; }
    public Guid StaffId { get; set; }
    public int StaffTypeId { get; set; }
    public int RoleId { get; set; }

    public virtual Role Role { get; set; }
    public virtual Staff Staff { get; set; }
    public virtual StaffType StaffType { get; set; }
}
