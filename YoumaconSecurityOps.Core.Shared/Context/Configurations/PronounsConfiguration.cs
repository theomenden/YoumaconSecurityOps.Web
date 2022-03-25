using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Core.Shared.Context.Configurations;
public partial class PronounsConfiguration : IEntityTypeConfiguration<Pronouns>
{
    public void Configure(EntityTypeBuilder<Pronouns> entity)
    {
        entity.ToTable("Pronouns");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .UseIdentityColumn(1);

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Pronouns> entity);
}

