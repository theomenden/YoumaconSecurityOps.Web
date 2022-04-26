using Nosthy.Blazor.DexieWrapper.Database;

namespace YoumaconSecurityOps.Web.Client.IndexedDb.Context
{
    public class YsecIndexedDbContext : Db
    {
        public Store<StaffRole, Int32> Roles { get; set; } = new(nameof(StaffRole.Id));

        public Store<StaffType, Int32> StaffTypes { get; set; } = new(nameof(StaffType.Id));

        public Store<LocationReader, Guid> Locations { get; set; } = new(nameof(LocationReader.Id));

        public Store<Pronoun, Int32> StoredPronouns { get; set; } = new(nameof(Pronoun.Id));

        public YsecIndexedDbContext(IModuleFactory moduleFactory)
            : base("YsecOpsDb", 2, new DbVersion[] { new YsecIndexedDbVersion() }, moduleFactory)
        {
        }
    }
}
