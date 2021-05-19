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
    class ContactModelBuilder
    {
        public static void BuildModel(EntityTypeBuilder<ContactReader> entity)
        {
            entity.ToTable("Contacts");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.FacebookName).HasMaxLength(100);

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.PreferredName).HasMaxLength(100);

            entity.Property(e => e.StaffId).HasColumnName("Staff_Id");

            entity.HasOne(c => c.StaffInformation)
                .WithOne(s => s.ContactInformation)
                .HasForeignKey<ContactReader>(c => c.StaffId);

            entity.HasIndex(e => new { e.LastName, e.FirstName }, "IX_Contacts_LastName_FirstName");

            entity.HasIndex(e => e.StaffId, "IX_Contacts_StaffId");
        }
    }
}
