﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
namespace YSecOps.Data.EfCore.Contexts.Configurations;

public partial class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> entity)
    {
        entity.HasIndex(e => new { e.LastName, e.FirstName }, "IX_Contacts_LastName_FirstName");

        entity.HasIndex(e => e.Pronoun_Id, "IX_Contacts_Pronoun_Id");

        entity.HasIndex(e => e.Staff_Id, "IX_Contacts_StaffId");

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

        entity.Property(e => e.Pronoun_Id).HasDefaultValueSql("((14))");

        entity.HasOne(d => d.Pronoun)
            .WithMany(p => p.Contacts)
            .HasForeignKey(d => d.Pronoun_Id)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Contacts_Pronouns_Id");

        entity.HasOne(d => d.Staff)
            .WithMany(p => p.Contacts)
            .HasForeignKey(d => d.Staff_Id)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Contacts_Staff_Id");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Contact> entity);
}