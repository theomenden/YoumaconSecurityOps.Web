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
    class StaffModelBuilder
    {
        public static void BuildModel(EntityTypeBuilder<StaffReader> entity)
        {
            entity.ToTable("Staff");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.ContactId)
                .HasColumnName("Contact_Id")
                .IsRequired();

            entity.Property(e => e.IsBlackShirt)
                .IsRequired();

            entity.Property(e => e.RoleId)
                .HasColumnName("Role_Id")
                .IsRequired();

            entity.Property(e => e.StaffTypeId)
                .HasColumnName("StaffType_Id")
                .IsRequired();

            entity.Property(e => e.ShirtSize)
                .IsRequired();

            entity.Property(e => e.BreakEndTime)
                .IsRequired(false);

            entity.Property(e => e.BreakStartTime)
                .IsRequired(false);

            entity.Property(e => e.IsRaveApproved)
                .IsRequired();

            entity.Property(e => e.IsOnBreak)
                .IsRequired();

            entity.HasOne(e => e.ContactInformation)
                .WithOne();

            entity.HasOne(e => e.Role)
                .WithOne();

            entity.HasOne(e => e.StaffType)
                .WithOne();
        }
    }
}
