
namespace YoumaconSecurityOps.Core.Shared.Contexts.Configurations;

public partial class PronounConfiguration : IEntityTypeConfiguration<Pronoun>
{
    public void Configure(EntityTypeBuilder<Pronoun> entity)
    {
        entity.Ignore(e => e.Contacts);

        entity.Ignore(e => e.NonStaffPeoples);

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Pronoun> entity);
}