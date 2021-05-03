using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Data.EntityFramework.ModelBuilders
{
    class StaffRoleModelBuilder
    {
        public static void BuildModel(EntityTypeBuilder<StaffRole> entity)
        {
            entity.ToTable("StaffRoles");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Role)
                .IsRequired();
        }
    }
}
