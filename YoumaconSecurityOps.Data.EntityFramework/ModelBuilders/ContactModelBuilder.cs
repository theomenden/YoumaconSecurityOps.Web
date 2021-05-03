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

            entity.Property(e => e.FirstName)
                .IsRequired();

            entity.Property(e => e.LastName)
                .IsRequired();

            entity.Property(e => e.PreferredName)
                .IsRequired();

            entity.Property(e => e.FacebookName)
                .IsRequired();

            entity.Property(e => e.CreatedOn)
                .IsRequired();

            entity.Property(e => e.StaffId)
                .HasColumnName("Staff_Id")
                .IsRequired();
        }
    }
}
