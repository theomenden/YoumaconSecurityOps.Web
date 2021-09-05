using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Models.Readers
{
    public partial class StaffTypesRoles
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid StaffId { get; set; }
        
        public int StaffTypeId { get; set; }

        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(StaffRole.StaffTypeRoleMap))]
        public virtual StaffRole Role { get; set; } = default!;

        [ForeignKey(nameof(StaffId))]
        [InverseProperty(nameof(StaffReader.StaffTypeRoleMaps))]
        public virtual StaffReader Staff { get; set; } = default!;

        [ForeignKey(nameof(StaffTypeId))]
        [InverseProperty(nameof(StaffType.StaffTypeRoleMaps))]
        public virtual StaffType StaffTypeNavigation { get; set; } = default!;

        //
    }
}
