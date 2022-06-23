using Nosthy.Blazor.DexieWrapper.Database;

namespace YoumaconSecurityOps.Web.Client.IndexedDb.Context
{
    public class YsecIndexedDbContext : Db
    {
        public YsecIndexedDbContext(IModuleFactory moduleFactory)
            : base("YsecOpsDb", 2, new DbVersion[] { new YsecIndexedDbVersion() }, moduleFactory)
        {
        }
    }
}
