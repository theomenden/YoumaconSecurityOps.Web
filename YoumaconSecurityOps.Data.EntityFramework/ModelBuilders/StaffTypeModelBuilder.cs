using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YoumaconSecurityOps.Core.Shared.Models.Readers;

namespace YoumaconSecurityOps.Data.EntityFramework.ModelBuilders
{
    class StaffTypeModelBuilder
    {
        public static void BuildModel(EntityTypeBuilder<StaffType> entity)
        {
            entity.ToTable("StaffTypes");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Description)
                .IsRequired();

            entity.Property(e => e.Title)
                .IsRequired();
        }
    }
}
